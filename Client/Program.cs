using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TrackMyMedia.Client.Services;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Client.Services;
using Client;
using Microsoft.AspNetCore.Components.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Load configuration from appsettings.json and set base api url
var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<JsonElement>("appsettings.json");
var apiBaseUrl = config.GetProperty("ApiBaseUrl").GetString();

// Add custom HTTP handler for JWT tokens
var handler = new HttpClientHandler();
builder.Services.AddScoped<AuthHttpClientHandler>();
builder.Services.AddScoped(sp =>
{
    var authHandler = sp.GetRequiredService<AuthHttpClientHandler>();
    authHandler.InnerHandler = handler;
    return new HttpClient(authHandler) { BaseAddress = new Uri(apiBaseUrl) };
});

// Add Blazored Local Storage
builder.Services.AddBlazoredLocalStorage();

// Register Services
builder.Services.AddScoped<AuthService>();

// Add root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
