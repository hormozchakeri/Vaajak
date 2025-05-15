using Microsoft.AspNetCore.Identity;
using Share;
using Vaajak.Domain.Entities;

namespace Vaajak.Persistence.Seeders
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            var roles = new[] { "Admin", "User", "Manager" };

            foreach (var role in roles)
            {
                try
                {
                    Console.WriteLine($"Using connection string: {ConnectionStrings.IdentityDatabaseContext}");

                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new Role { Name = role });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Seeding failed: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task SeedAdminUserAsync(UserManager<User> userManager)
        {
            var adminEmail = "hormozchakeri@gmail.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                var newAdmin = new User
                {
                    UserName = "vaajak_admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "050425Hc");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
