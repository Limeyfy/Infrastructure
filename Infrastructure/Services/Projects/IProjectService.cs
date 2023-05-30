using Infrastructure.DTOs.Projects;
using Infrastructure.Models.Projects;

namespace Infrastructure.Services.Projects;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(Project project); 
}