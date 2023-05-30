using Infrastructure.Data;
using Infrastructure.DTOs.Projects;
using Infrastructure.Models.Projects;

namespace Infrastructure.Services.Projects;

public class ProjectService : IProjectService
{
    private readonly DataContext _dataContext;

    public ProjectService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Project> CreateProjectAsync(CreateProjectDto dto)
    {
        var project = new Project
        {
            Name = dto.Name,
            CompanyId = dto.CompanyId
        };
        
        await _dataContext.Projects.AddAsync(project);
        
        var created = await _dataContext.SaveChangesAsync();
        
        if (created <= 0)
        {
            throw new Exception("Could not create project");
        }
        
        return project;
    }
}