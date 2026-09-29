using CheckMate.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMate.Infrastructure.Persistence.SeedData
{
    public static class IdentityDataSeed
    {
        public static async Task SeedAsync(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IConfiguration configuration)
        {
            const string adminRole = "admin";

            if(!await roleManager.RoleExistsAsync(adminRole))
            {
                var roleResult = await roleManager.CreateAsync(new Role
                {
                    Name = adminRole,
                    Description = "This is the admin role. He is basically allowed to do anything in the application. He has full control."
                });

                if (!roleResult.Succeeded)
                    throw new Exception("Could not create the admin role...");

            }

            var email = configuration["SeedAdmin:Email"]
                ?? throw new Exception("Email address not found in configuration file.");

            var password = configuration["SeedAdmin:Password"]
                ?? throw new Exception("Password Not found in configuration file");

            User? admin = await userManager.FindByEmailAsync(email);

            if (admin is null)
            {
                admin = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Administrator",
                    Email = email
                };

                var userResult = await userManager.CreateAsync(admin, password);

                if (!userResult.Succeeded)
                {
                    var errors = string.Join(", ",
                        userResult.Errors.Select(e => e.Description));

                    throw new Exception($"Can not create Admin user: {errors}");
                }
            }

            if (!await userManager.IsInRoleAsync(admin ,adminRole))
            {
                var addToRoleResult = await userManager.AddToRoleAsync(admin, adminRole);
                if (!addToRoleResult.Succeeded)
                    throw new Exception($"Could not associate the user: {admin.UserName} to the role: {adminRole}");
            }
        }
    }
}
