using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.Data;
using Shared.Models;
using TrackMyMedia.Server.Helpers;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace TrackMyMedia.Server.Services
{
    public class UserService : IUserService
    {
        private readonly TrackMyMediaDbContext Context;
        private readonly AuthHelper authHelper;

        public UserService(TrackMyMediaDbContext context, AuthHelper authHelper)
        {
            Context = context;
            this.authHelper = authHelper;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<bool> RegisterUser(RegisterRequestModel request)
        {
            //Validation checks
            if (await Context.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email))
                throw new InvalidOperationException("Username or email already exists.");

            if (!IsValidEmail(request.Email))
                throw new ArgumentException("Invalid email format.");

            if (!IsValidPassword(request.Password))
                throw new ArgumentException("Password must be at least 8 characters long and include at least one number.");

            // Hash the password
            string passwordHash = HashPassword(request.Password);

            // Create a new user
            var newUser = new UserModel
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                Role = "User"
            };

            // Save user to the database
            Context.Users.Add(newUser);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel request)
        {
            if (string.IsNullOrEmpty(request.UsernameOrEmail) || string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Username or email and password are required.");

            // Fetch user from the database
            var user = await Context.Users
                .FirstOrDefaultAsync(u => u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail);

            if (user == null)
                throw new ArgumentException("Invalid username or email.");

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new ArgumentException("Invalid password.");

            // Generate JWT token
            var token = authHelper.GenerateJwtToken(user);

            return new LoginResponseModel
            {
                Token = token,
                Username = user.Username,
                Email = user.Email
            };
        }


        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
        private bool IsValidPassword(string password)
        {
            var passwordRegex = new Regex(@"^(?=.*[0-9]).{8,}$");
            return passwordRegex.IsMatch(password);
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
