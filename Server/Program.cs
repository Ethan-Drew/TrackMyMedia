using Microsoft.EntityFrameworkCore;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Server.Services;
using TrackMyMedia.Server.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Use configuration from appsettings.json
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services for user authentication and JWT handling
var jwtSecretKey = configuration["Jwt:SecretKey"]
                   ?? throw new InvalidOperationException("JWT secret key not configured in appsettings.json.");
builder.Services.AddSingleton(new AuthHelper(jwtSecretKey));
builder.Services.AddScoped<IUserService, UserService>();

// Add HttpClient
var baseUrl = configuration["BaseUrl"]
              ?? throw new ArgumentNullException(nameof(configuration), "BaseUrl is not configured.");

builder.Services.AddScoped<HttpClient>(sp =>
    new HttpClient { BaseAddress = new Uri(baseUrl) });

// Add DbContext
builder.Services.AddDbContext<TrackMyMediaDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
