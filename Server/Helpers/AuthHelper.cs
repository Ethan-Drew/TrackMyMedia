using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Server.Helpers
{
    public class AuthHelper
    {
        private readonly string _jwtSecretKey;
        private const int TokenExpiryMinutes = 60;

        // Constructor to inject the secret key
        public AuthHelper(string jwtSecretKey)
        {
            if (string.IsNullOrEmpty(jwtSecretKey))
                throw new ArgumentNullException(nameof(jwtSecretKey), "JWT Secret Key cannot be null or empty.");

            _jwtSecretKey = jwtSecretKey;
        }

        public string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
