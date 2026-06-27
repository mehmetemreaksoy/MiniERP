using Microsoft.AspNetCore.Identity;

namespace MiniERP.Web.Data;

public static class IdentitySeedData
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles =
        {
            "Admin",
            "Manager",
            "SalesUser",
            "WarehouseUser",
            "Viewer"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await CreateUserIfNotExistsAsync(
            userManager,
            userName: "admin",
            email: "admin@minierp.local",
            password: "Admin123!",
            role: "Admin");

        if (!environment.IsDevelopment())
        {
            return;
        }

        await CreateUserIfNotExistsAsync(userManager, "manager", "manager@minierp.local", "Test123!", "Manager");
        await CreateUserIfNotExistsAsync(userManager, "sales", "sales@minierp.local", "Test123!", "SalesUser");
        await CreateUserIfNotExistsAsync(userManager, "warehouse", "warehouse@minierp.local", "Test123!", "WarehouseUser");
        await CreateUserIfNotExistsAsync(userManager, "viewer", "viewer@minierp.local", "Test123!", "Viewer");
    }

    private static async Task CreateUserIfNotExistsAsync(
        UserManager<IdentityUser> userManager,
        string userName,
        string email,
        string password,
        string role)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, password);
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
