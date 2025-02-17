using AuthenticationAPI.Definitions;
using AuthenticationAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthenticationAPI.Services
{
    public class JWTService : ITokenService
    {
        private readonly IConfiguration _configuration;        

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken()
        {
            var jwtSettings = new JwtSettings();
            _configuration.GetSection("JWTSettings").Bind(jwtSettings);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var toekn = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryTimeInMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(toekn);
        }
    }
}
