using Infrastructure.Models.Auth;
using Infrastructure.Models.Companies;
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
    }
    
    public DbSet<Company> Companies => Set<Company>();
}