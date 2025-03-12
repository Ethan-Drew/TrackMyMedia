using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Server.Services
{
    public interface IAuthService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<bool> RegisterUser(RegisterRequestModel request);
        Task<LoginResponseModel> Login(LoginRequestModel request);
    }
}
