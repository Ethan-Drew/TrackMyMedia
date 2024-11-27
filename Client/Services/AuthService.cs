using Blazored.LocalStorage;
using Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TrackMyMedia.Shared.Models;

namespace TrackMyMedia.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient HttpClient;
        private readonly ILocalStorageService LocalStorage;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            HttpClient = httpClient;
            LocalStorage = localStorage;
        }

        // Login functionality
        public async Task LoginUser(LoginResponseModel response)
        {
            // Store token locally
            await LocalStorage.SetItemAsync("authToken", response.Token);

            // Set authorization header
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", response.Token);
        }

        // Logout functionality
        public async Task LogoutUser()
        {
            // Remove the token and clear the header
            await LocalStorage.RemoveItemAsync("authToken");
            HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Fetch users
        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                // Retrieve token
                var token = await LocalStorage.GetItemAsync<string>("authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    HttpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                // Make the request
                var users = await HttpClient.GetFromJsonAsync<List<UserModel>>("http://localhost:5221/api/Auth");
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
