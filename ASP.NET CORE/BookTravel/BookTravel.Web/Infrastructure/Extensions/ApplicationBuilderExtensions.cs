using BookTravel.Data;
using BookTravel.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BookTravel.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TravelDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                        var adminName = DataConstants.AdministratorRole;

                        var result = await roleManager.RoleExistsAsync(adminName);
                        if (!result)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = adminName
                            });
                        }

                        var moderatorName = DataConstants.ModeratorRole;

                        result = await roleManager.RoleExistsAsync(moderatorName);
                        if (!result)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = DataConstants.ModeratorRole
                            });
                        }

                        var adminEmail = DataConstants.AdminUsername;
                        var adminUser = await userManager.FindByEmailAsync(adminEmail);
                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = DataConstants.AdminUsername,
                                UserName = DataConstants.AdminUsername,
                            };
                            await userManager.CreateAsync(adminUser, DataConstants.AdminPassword);

                            await userManager.AddToRoleAsync(adminUser, adminName);
                        }
                    }
                    ).Wait();
            }


            return app;
        }
    }
}
