using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.DTOs.Auth;
using Infrastructure.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public IActionResult Register()
    {
        return Ok("Register");
    }
    
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        // create token
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            return BadRequest("Invalid credentials");
        }
        
        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        
        if (!result)
        {
            return BadRequest("Invalid credentials");
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
        };
        
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        
        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return Ok(new { Token = tokenAsString });
    }
    
}