using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Companies;

namespace Infrastructure.Models.Projects;

public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    public Guid CreatedBy { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;
    
    public ICollection<SubProject> SubProjects { get; set; } = new List<SubProject>();
}