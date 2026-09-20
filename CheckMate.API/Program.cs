using CheckMate.Application.Interfaces.RepositoryInterfaces;
using CheckMate.Application.Interfaces.ServiceInterfaces;
using CheckMate.Application.Services;
using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Persistence;
using CheckMate.Infrastructure.Persistence.SeedData;
using CheckMate.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CheckMate") ??
        throw new InvalidOperationException("Missing connection string for CheckMate");


// Add services to the container.
builder.Services.AddDbContext<CheckMateDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.
    AddIdentityCore<User>()
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
