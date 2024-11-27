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
        private readonly string JwtSecretKey;
        private const int TokenExpiryMinutes = 300;

        // Constructor to inject the secret key
        public AuthHelper(string jwtSecretKey)
        {
            if (string.IsNullOrEmpty(jwtSecretKey))
                throw new ArgumentNullException(nameof(jwtSecretKey), "JWT Secret Key cannot be null or empty.");

            JwtSecretKey = jwtSecretKey;
        }

        public string GenerateJwtToken(string userId, string email, string role)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, userId),
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Role, role)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TrackMyMedia",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
