#region Using
using AuditNow.Core.Models;
using AuditNow.Core.Models.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#endregion

namespace AuditNow.Core.Common
{
    public class TokenManager
    {


        public JwtToken GetToken(User user, IConfiguration _config)
        {
            try
            {
                var claims = new[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretKey"]));
                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    claims: claims,
                    signingCredentials: credential,
                    expires: DateTime.Now.AddMinutes(240),
                    issuer: _config["Authentication:Issuer"],
                    audience: _config["Authentication:Audience"]
                );

                JwtToken jwtToken = new JwtToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };

                return jwtToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}