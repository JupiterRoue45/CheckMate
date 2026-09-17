using CheckMate.Infrastructure.Identity;
using CheckMate.Infrastructure.Persistence;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
