using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models.Auth;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; } = String.Empty;
    
    public string? LastName { get; set; } = String.Empty;
}