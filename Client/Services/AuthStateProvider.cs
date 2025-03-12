using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using TrackMyMedia.Client.Services;

namespace TrackMyMedia.Client.Services
{
    public class AuthStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthService _authService;

        private bool _isAuthenticated;

        public AuthStateProvider(ILocalStorageService localStorage, AuthService authService)
        {
            _localStorage = localStorage;
            _authService = authService;
        }

        public event Func<Task>? OnChange;

        public bool IsAuthenticated => _isAuthenticated;

        public async Task InitializeAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _isAuthenticated = !string.IsNullOrEmpty(token) && _authService.IsValidToken(token);
            await NotifyStateChangedAsync();
        }

        public async Task SetAuthenticated(bool isAuthenticated)
        {
            _isAuthenticated = isAuthenticated;
            await NotifyStateChangedAsync();
        }

        public async Task LogoutAsync()
        {
            await _authService.LogoutUser();
            _isAuthenticated = false;
            await NotifyStateChangedAsync();
        }

        private async Task NotifyStateChangedAsync()
        {
            if (OnChange != null)
            {
                await OnChange.Invoke();
            }
        }
    }
}
