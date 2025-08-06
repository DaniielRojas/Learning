using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Learning.Models;
using Learning.Models.Entities;

namespace Learning.Custom
{
    public class Utilities
    {
        private readonly IConfiguration _configuration;
        public Utilities(IConfiguration configuration )
        {
         _configuration = configuration;   
        }

        public string EncryptSHA256(string text)
        {
            using(SHA256 sHA256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i]);
                }
                return builder.ToString();
            }
        }
        public string GenerateJWT(User user)
        {
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.Username!)

            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}
