using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CompaniesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Company");
    }
}