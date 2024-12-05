using Blazored.LocalStorage;
using TrackMyMedia.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

        public async Task StoreJwtToken(LoginResponseModel response)
        {
            // Store token locally
            await LocalStorage.SetItemAsync("authToken", response.Token);

            // Set auth header
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", response.Token);
        }

        public async Task StoreUserInfo(LoginResponseModel response)
        {
            await LocalStorage.SetItemAsync("username", response.Username);
            await LocalStorage.SetItemAsync("email", response.Email);
            await LocalStorage.SetItemAsync("firstName", response.FirstName);
        }

        public async Task<LoginResponseModel> LoginUser(string username, string password)
        {
            try
            {
                var loginRequest = new LoginRequestModel
                {
                    Username = username,
                    Password = password
                };

                var response = await HttpClient.PostAsJsonAsync("api/Auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseModel>();
                    if (loginResponse != null)
                    {
                        // Store the token
                        await StoreJwtToken(loginResponse);
                        await StoreUserInfo(loginResponse);
                    }
                    return loginResponse;
                }
                else
                {
                    throw new Exception("Login failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging in: {ex.Message}");
                throw;
            }
        }
        public async Task RegisterUser(RegisterRequestModel registerRequest)
        {
            try
            {
                // Some basic validation
                // TODO improve this
                if (string.IsNullOrWhiteSpace(registerRequest.Username) ||
                    string.IsNullOrWhiteSpace(registerRequest.Email) ||
                    string.IsNullOrWhiteSpace(registerRequest.FirstName) ||
                    string.IsNullOrWhiteSpace(registerRequest.LastName) ||
                    string.IsNullOrWhiteSpace(registerRequest.Password))
                {
                    throw new Exception("All fields are required.");
                }

                var response = await HttpClient.PostAsJsonAsync("api/Auth/register", registerRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Registration failed: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                throw new Exception($"An error occurred during registration: {ex.Message}");
            }
        }

        public async Task LogoutUser()
        {
            await LocalStorage.RemoveItemAsync("authToken");
            HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            try
            {
                var users = await HttpClient.GetFromJsonAsync<List<UserModel>>("api/Auth");
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
