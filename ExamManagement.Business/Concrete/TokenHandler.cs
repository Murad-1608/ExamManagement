using ExamManagement.Business.Abstract;
using ExamManagement.Business.DTOs.AuthDTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExamManagement.Business.Concrete
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDTO CreateAccessToken(int second, string role)
        {
            TokenDTO token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddSeconds(second);

            var claims = new[] { new Claim(ClaimTypes.Role, role) };

            JwtSecurityToken securityToken = new(audience: _configuration["Token:Audience"],
                                                  issuer: _configuration["Token:Issuer"],
                                                  expires: token.Expiration,
                                                  notBefore: DateTime.UtcNow,
                                                  signingCredentials: signingCredentials,
                                                  claims: claims);

            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefleshToken = CreateRefleshToken();

            return token;
        }

        public string CreateRefleshToken()
        {
            byte[] number = new byte[32];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);

                return Convert.ToBase64String(number);
            }
        }
    }
}
