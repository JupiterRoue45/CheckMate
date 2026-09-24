using CheckMate.Application.Interfaces;
using CheckMate.Application.Services;
using CheckMate.Infrastructure.Configurations;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Persistence.SeedData;
using CheckMate.Infrastructure.Repositories.Implementations;
using CheckMate.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CheckMate") ??
        throw new InvalidOperationException("Missing connection string for CheckMate");


// Add services to the container.
builder.Services.AddDbContext<CheckMateDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.
    AddIdentityCore<User>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredUniqueChars = 1;
    })
    .AddRoles<Role>()
    .AddEntityFrameworkStores<CheckMateDbContext>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

# region reporitories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
#endregion

#region application
builder.Services.AddScoped<ICountryService, CountryService>();
#endregion

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

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
