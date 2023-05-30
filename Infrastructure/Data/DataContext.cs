using Infrastructure.Models.Auth;
using Infrastructure.Models.Companies;
using Infrastructure.Models.Projects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        builder.Entity<Project>()
            .HasOne(p => p.Company)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<SubProject>()
            .HasOne(s => s.Project)
            .WithMany(p => p.SubProjects)
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    public DbSet<Company> Companies => Set<Company>();
    
    public DbSet<Project> Projects => Set<Project>();
    
    public DbSet<SubProject> SubProjects => Set<SubProject>();
}