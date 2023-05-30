using Infrastructure.DTOs.Companies;
using Infrastructure.Models.Companies;
using Infrastructure.Services.Companies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CompaniesController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICompanyService _companyService;

    public CompaniesController(IHttpContextAccessor httpContextAccessor, ICompanyService companyService)
    {
        _httpContextAccessor = httpContextAccessor;
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
      
        if(pageSize > 100)
        {
            return BadRequest("Page size cannot be greater than 10");
        }
        
        var companies = await _companyService.GetCompaniesAsync(page, pageSize);
        return Ok(companies);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCompanyDto dto)
    {
        var user = _httpContextAccessor.HttpContext!.User;
        string userId = user.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value ?? String.Empty;
        
        if(String.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        
        var company = new Company
        {
            Name = dto.Name,
            CreatedBy = Guid.Parse(userId)
        };
        
        var result = await _companyService.CreateCompanyAsync(company);
        
        if (!result)
        {
            return BadRequest("Something went wrong");
        }
        
        return Ok(company);
    }
}