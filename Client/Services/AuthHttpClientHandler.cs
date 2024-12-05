using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Client.Services
{
    public class AuthHttpClientHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthHttpClientHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Example: Add authorization token from local storage
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Call the inner handler
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
