using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static partial class JWT
    {
        public static int? ValidateJwtToken(IConfiguration _configuration, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtKey"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtIssuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtIssuer"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var UserId = int.Parse(jwtToken.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                // if validation is successful then return UserId from JWT token 
                return UserId;
            }
            catch(Exception ex)
            {
                // if validation fails then return null
                return null;
            }
        }
    }
}
