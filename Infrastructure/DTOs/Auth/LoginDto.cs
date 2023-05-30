using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DTOs.Auth;

public class LoginDto
{
    [Required]
    public string Email { get; set; } = String.Empty;
    
    [Required]
    public string Password { get; set; } = String.Empty;
}