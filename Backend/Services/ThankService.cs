using Data_.Entities;
using Data_.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Data_.Dtos;

namespace Services
{
    public class ThankService : IThankService
    {
        private IUnitOfWork UnitOfWork;
        private IConfiguration Config;
        private IMapper Mapper;
        private readonly string Key;

       public  ThankService(IUnitOfWork _UnitOfWork, IConfiguration _Config, IMapper _Mapper)
        {
            UnitOfWork = _UnitOfWork;
            Config = _Config;
            Mapper = _Mapper;
            Key = Config.GetSection("JWTToken")["TokenSecretString"];
        }
        public async Task<ThankReadDTO> GetThank(string Token, CancellationToken Cancel)
        {
                var Claims = HelperService.GetClaimsFromToken(Token, Key);
                var UserId = HelperService.GetIdFromToken(Claims);
                
                if (UserId == null)
                {
                    new ArgumentNullException();
                }

                return Mapper.Map<ThankReadDTO>(await UnitOfWork.ThankRepository.DbSet
                    .Include(Thank => Thank.FromUser)
                    .Where(Item => Item.ToUserId == new Guid(UserId))
                    .OrderByDescending(Item => Item.AddedTime)
                    .FirstOrDefaultAsync(Cancel));
        }

        public async Task SayThank(string Token, string Text, Guid ToUserId, CancellationToken Cancel)
        {
                var Claims = HelperService.GetClaimsFromToken(Token, Key);
                var UserId = HelperService.GetIdFromToken(Claims);
                
                if (UserId == null)
                {
                    throw new ArgumentNullException();
                }

                var ThankBody = new Thank
                {
                    FromUser = await UnitOfWork.UserRepository.GetById(new Guid(UserId), Cancel),
                    Text = Text,
                    ToUserId = ToUserId,
                    Id = new Guid(),
                    AddedTime = DateTime.Now.ToUniversalTime()
                };
                await UnitOfWork.ThankRepository.Create(ThankBody, Cancel);
                await UnitOfWork.SaveChangesAsync(Cancel);
        }
    }
}
