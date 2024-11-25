using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Server.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
    }
}
