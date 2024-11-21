using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<UserModel>>("http://localhost:5221/api/users");
                return users ?? new List<UserModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                throw;
            }
        }
    }
}
