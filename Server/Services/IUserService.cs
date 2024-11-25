using System.Threading.Tasks;
using Shared.Models;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Server.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<bool> RegisterUser(RegisterRequestModel request);
        Task<LoginResponseModel> Login(LoginRequestModel request);
    }
}
