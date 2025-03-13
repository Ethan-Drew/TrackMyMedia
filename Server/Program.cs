using Microsoft.EntityFrameworkCore;
using TrackMyMedia.Server.Data;
using TrackMyMedia.Server.Services;
using TrackMyMedia.Server.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Use configuration from appsettings.json
var configuration = builder.Configuration;

// Cors services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer abc123token'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Auth services
var jwtSecretKey = configuration["Jwt:SecretKey"]
    ?? throw new InvalidOperationException("JWT secret key not configured in appsettings.json.");

builder.Services.AddSingleton<AuthHelper>(sp =>
{
    return new AuthHelper(jwtSecretKey);
});

builder.Services.AddAuthentication(options =>
{
    //Set default auth to JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Check token is valid
        ValidateIssuer = true,
        //Check audience is valid - turned off for now
        ValidateAudience = false,
        //Checl hasnt expired
        ValidateLifetime = true,
        //Ensures signing of token is valid
        ValidateIssuerSigningKey = true,
        //Check issuer is valid
        ValidIssuer = configuration["Jwt:Issuer"],
        //Validate JWT integrity
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
        //No allowance for clockskew when checking expiration
        ClockSkew = TimeSpan.Zero
    };
});

// Add HttpClient
var baseUrl = configuration["BaseUrl"]
              ?? throw new ArgumentNullException(nameof(configuration), "BaseUrl is not configured.");

builder.Services.AddScoped<HttpClient>(sp =>
    new HttpClient { BaseAddress = new Uri(baseUrl) });

// General services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IComicService, ComicService>();
builder.Services.AddScoped<IMusicService, MusicService>();
builder.Services.AddScoped<ITVShowService, TVShowService>();
builder.Services.AddScoped<IVideoGameService, VideoGameService>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
