using Microsoft.IdentityModel.Tokens;
using SmartParking.Application.Abstractions.Persistence;
using SmartParking.Contracts.DriverAuth;
using SmartParking.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartParking.Infrastructure.Persistence.Repositories
{
    public sealed class DriverTokenService : IDriverTokenService
    {
        private const string SecretKey = "THIS_IS_A_DEMO_SECRET_KEY_CHANGE_IT";
        private readonly byte[] _key;

        public DriverTokenService()
        {
            _key = Encoding.UTF8.GetBytes(SecretKey);
        }

        public DriverTokenResult Generate(Driver driver)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddHours(2);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, driver.Id.ToString()),
            new Claim("role", "driver"),
            new Claim("phone", driver.Phone),
            new Claim("name", driver.FullName)
        };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new DriverTokenResult(tokenString, expires);
        }
    }
}
