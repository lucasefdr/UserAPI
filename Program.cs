using System.Text;
using IdentityAPI.Authorization;
using IdentityAPI.Data;
using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:AppConnection"];

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>
    (opts => { opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });

// Identity config
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// AutoMapper config
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Authentication 
builder.Services.AddAuthentication(opts => { opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// Authorization Policy 
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("MinimumAge", policy =>
        policy.AddRequirements(new MinimumAge(18)));
});

// Dependency Injection config
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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