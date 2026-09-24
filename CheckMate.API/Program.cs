using CheckMate.Application.Interfaces;
using CheckMate.Application.Services;
using CheckMate.Infrastructure.Configurations;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Persistence.SeedData;
using CheckMate.Infrastructure.Repositories.Implementations;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CheckMate") ??
        throw new InvalidOperationException("Missing connection string for CheckMate");


// Add services to the container.
builder.Services.AddDbContext<CheckMateDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork>(provider =>
    provider.GetRequiredService<CheckMateDbContext>());

builder.Services.
    AddIdentityCore<User>(options =>
    {
        // User
        options.SignIn.RequireConfirmedEmail = false;
        options.User.RequireUniqueEmail = true

        // Password
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredUniqueChars = 1;

        // Lockout
        options.Lockout.AllowedForNewUsers = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddRoles<Role>()
    .AddEntityFrameworkStores<CheckMateDbContext>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

# region reporitories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
#endregion

#region application
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
#endregion

var jwtSection = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<JwtSettings>(jwtSection);

var jwtSettings = jwtSection.Get<JwtSettings>()
    ?? throw new InvalidOperationException("JwtSettings configuration is missing.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.SigningKey)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = JwtRegisteredClaimNames.Name,
            RoleClaimType = ClaimTypes.Role,
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await IdentityDataSeed.SeedAsync(
        services.GetRequiredService<UserManager<User>>(),
        services.GetRequiredService<RoleManager<Role>>(),
        services.GetRequiredService<IConfiguration>());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
