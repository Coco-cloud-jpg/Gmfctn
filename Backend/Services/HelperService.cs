using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Services
{
   static  class HelperService
    {
        static public IEnumerable<Claim> GetClaimsFromToken(string Token, string Key)
        {
            var TokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key))
                };
                var Principal = TokenHandler.ValidateToken(Token, validationParameters, out var ValidatedToken);

                if ((ValidatedToken is JwtSecurityToken JwtToken)
                                          && (JwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)))
                    return JwtToken.Claims;
                return null;
            }
            catch
            {
                return null;
            }
        }
        static public string  GetIdFromToken(IEnumerable<Claim> Claims)
        {
            try
            {
                if (Claims == null)
                {
                    return null;
                }

                return Claims.FirstOrDefault(Claim =>
                                                Claim.Type == JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }
        }
        
    }
}
