using Microsoft.EntityFrameworkCore;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Server.Services
{
    public class UserService : IUserService
    {
        private readonly TrackMyMediaDbContext _context;

        public UserService(TrackMyMediaDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
