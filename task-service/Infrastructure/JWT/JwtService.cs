using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_service.Application.DTOs;
using task_service.Application.Interfaces;

namespace task_service.Infrastructure.JWT
{
    
    public class JwtService : ITokenService
    {
        private readonly JwtSettings _settings;

        public JwtService(IOptions<JwtSettings> settings) => _settings = settings.Value;

        public string GenerateToken(UserDTO userDTO)
        {
            var claims = new List<Claim>
            {
                new("client_id", userDTO.Id),
                new("client_type", userDTO.type_id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
}
