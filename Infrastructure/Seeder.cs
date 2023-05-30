using Infrastructure.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class Seeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    
    public Seeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            
            await SeedRoles(roleManager);
            await SeedAdmin(userManager, configuration);
        }
        
        await Task.CompletedTask;
    }
    
    public async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
    {
        var roles = new List<ApplicationRole>
        {
            new ApplicationRole { Name = "Admin" },
        };
        
        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role.Name!);
            if (!roleExists)
            {
                await roleManager.CreateAsync(role);
            }
        }   
    }

    public async Task SeedAdmin(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        var user = new ApplicationUser
        {
            UserName = configuration["Seed:Admin:Username"],
            Email = configuration["Seed:Admin:Email"],
        };
        
        var userExists = await userManager.FindByEmailAsync(user.Email!);
        
        var password = configuration["Seed:Admin:Password"];
        
        if(password == null)
            throw new Exception("Password is null");
        
        if (userExists is null)
        {
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
    
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}