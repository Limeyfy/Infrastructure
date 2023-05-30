using Infrastructure.DTOs.Projects;
using Infrastructure.Models.Projects;
using Infrastructure.Services.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody] CreateProjectDto dto)
    {
        // TODO: get userid from session and add it as createdBy 
        
        var project = await _projectService.CreateProjectAsync(new Project
        {
            Name = dto.Name,
            CompanyId = dto.CompanyId
        });
        return Created($"/projects/{project.Id}", project);
    }
}