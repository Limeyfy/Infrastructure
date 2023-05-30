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

    public async Task<Project> CreateProjectAsync(Project project)
    {
        await _dataContext.Projects.AddAsync(project);
        
        var created = await _dataContext.SaveChangesAsync();
        
        if (created <= 0)
        {
            throw new Exception("Could not create project");
        }
        
        return project;
    }
}