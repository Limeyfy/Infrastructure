namespace Infrastructure.DTOs.Projects;

public class CreateProjectDto
{
    public Guid CompanyId { get; set; }
    
    public string Name { get; set; } = String.Empty;
}