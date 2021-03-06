using Data_.Interfaces;
using Services.Interfaces;
using Data_.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Security.Cryptography;
using AutoMapper;
using Data_.Dtos;
using Data_;
using System.Linq;
using System.Collections.Generic;
using Data_.Validators;

namespace Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private IUnitOfWork UnitOfWork;
        private IConfiguration Config;
        private IMapper Mapper;
        private readonly string Key;
        private readonly string TimeToLive;

        public JwtAuthenticationService(IUnitOfWork _UnitOfWork, IConfiguration _Config, IMapper _Mapper)
        {
            UnitOfWork = _UnitOfWork;
            Config = _Config;
            Mapper = _Mapper;
            Key = Config.GetSection("JWTToken")["TokenSecretString"];
            TimeToLive = Config.GetSection("JWTToken")["TokenLifetime"];
        }
        public async Task<(string, string, string)> Authenticate(string Login, string Password, CancellationToken Cancel, bool isHashed = false)
        {
            if (!isHashed)
                Password = HashData(Password);

            UserReadDTO User = await GetUserFromDB(Login, Cancel);

            if (User == null || User.Password != Password)
                return (null, null, null);

            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenKey = Encoding.ASCII.GetBytes(Key);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(User)),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(TimeToLive)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey),
                                                     SecurityAlgorithms.HmacSha256)
            };

            var Token = TokenHandler.CreateToken(TokenDescriptor);

            var RefreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                UserId = User.Id,
                CreatedTime = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddYears(1)
            };
            await UnitOfWork.UserRepository.Context.RTokens.AddAsync(RefreshToken, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);

            return (TokenHandler.WriteToken(Token), Token.ValidTo.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"), RefreshToken.Token);
        }
        private Claim[] GetClaims(UserReadDTO User)
        {
            Claim[] Claims = new Claim[1 + User.Roles.Count];
            Claims[0] = new Claim(JwtRegisteredClaimNames.Sub, User.Id.ToString());
            int Count = 1;

            foreach (var Role in User.Roles)
            {
                Claims[Count++] = new Claim(ClaimTypes.Role, Role);
            }

            return Claims;
        }
        private string HashData(string Data)
        {
            using (SHA256 Sha256Hash = SHA256.Create())
            {
                var ByteArray = Sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(Data));

                return Encoding.UTF8.GetString(ByteArray, 0, ByteArray.Length);
            }
        }
        private async Task<UserReadDTO> GetUserFromDB(string Login, CancellationToken Cancel)
        {
            return Mapper.Map<UserReadDTO>(await UnitOfWork.UserRepository.DbSet
                               .Include(User => User.UserRoles)
                                            .ThenInclude(UserRole => UserRole.Role)
                               .FirstOrDefaultAsync(_User => _User.Email == Login || _User.UserName == Login, Cancel));
        }

        public async Task<(string, string, string)> RefreshToken(string Token, string RefreshToken, CancellationToken Cancel)
        {
             var Claims = HelperService.GetClaimsFromToken(Token, Key);

             if (Claims == null) 
             {
                 return (null, null, null);
             }

             var ExpiryDate = long.Parse(Claims.FirstOrDefault(Claim =>
                                             Claim.Type == JwtRegisteredClaimNames.Exp).Value);

             var ExpiryDateTime = new DateTime(1970, 1, 1, 0, 0, 0)
                 .AddSeconds(ExpiryDate);

             if (ExpiryDateTime > DateTime.UtcNow)
             {
                 return (null, null, null);
             }

            var UserId = Claims.FirstOrDefault(Claim =>
                                             Claim.Type == JwtRegisteredClaimNames.Sub).Value;

            await DeleteUnUsedRefreshTokens(RefreshToken, UserId, Cancel);
            var StoredRefreshToken = await UnitOfWork.UserRepository.Context.RTokens.FirstOrDefaultAsync(Token => Token.Token == RefreshToken, Cancel);

            if ( IsNotValidRefreshToken(StoredRefreshToken) || (StoredRefreshToken.UserId.ToString() != Claims.FirstOrDefault(Claim =>
                                             Claim.Type == JwtRegisteredClaimNames.Sub).Value))
                return (null, null, null);

            UnitOfWork.UserRepository.Context.RTokens.Remove(StoredRefreshToken);
             await UnitOfWork.SaveChangesAsync(Cancel);

             var User =await  UnitOfWork.UserRepository.GetById(StoredRefreshToken.UserId, Cancel);

             return await Authenticate(User.UserName, User.Password, Cancel, true);
        }
        private async Task DeleteUnUsedRefreshTokens(string RefreshToken, string UserId, CancellationToken Cancel)
        {
            var Cuantity = await UnitOfWork.UserRepository.Context.RTokens.CountAsync(Token => Token.UserId.ToString() == UserId, Cancel);

            if (Cuantity > 1)
            {
                var AllTokens = await UnitOfWork.UserRepository.Context.RTokens.ToListAsync(Cancel);

                foreach (var RToken in AllTokens)
                {
                    if ((RToken.UserId.ToString() == UserId) && (RToken.Token != RefreshToken ))
                    {
                        UnitOfWork.UserRepository.Context.RTokens.Remove(RToken);
                    }
                }
                await UnitOfWork.SaveChangesAsync(Cancel);
            }
        }
        private bool IsNotValidRefreshToken(RefreshToken Token)
        {
            return (Token == null ||
                Token.ExpiryDate < DateTime.UtcNow
                );
        }

        public string GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];

            using (var Rng = RandomNumberGenerator.Create())
            {
                Rng.GetBytes(RandomNumber);

                return Convert.ToBase64String(RandomNumber);
            }
        }

        public async Task ResetPassword(string NewPassword, string Key, CancellationToken Cancel)
        {
            if (!ModelsValidator.RegExpPassword.IsMatch(NewPassword))
                throw new ArgumentException();

            var Request = await UnitOfWork.PasswordResetRequestRepository.DbSet.FirstOrDefaultAsync(Request => Request.Hash == Key, Cancel);

            if (Request == null)
                throw new ArgumentException();

            var User = await UnitOfWork.UserRepository.DbSet.FirstOrDefaultAsync(User => User.Email == Request.Email, Cancel);

            if (User == null)
                throw new ArgumentException();

            User.Password = HashData(NewPassword);

            await UnitOfWork.PasswordResetRequestRepository.Delete(Request.Id, Cancel);
            await UnitOfWork.SaveChangesAsync(Cancel);
        }
    }
}
