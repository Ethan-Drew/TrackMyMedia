using Microsoft.EntityFrameworkCore;
using TrackMyMedia.Server.Data;

var builder = WebApplication.CreateBuilder(args);

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

// Add HttpClient
var baseUrl = builder.Configuration["BaseUrl"]
              ?? throw new ArgumentNullException(nameof(builder.Configuration), "BaseUrl is not configured.");

builder.Services.AddScoped<HttpClient>(sp =>
    new HttpClient { BaseAddress = new Uri(baseUrl) });

builder.Services.AddDbContext<TrackMyMediaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
