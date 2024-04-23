using AuthenticationService.Authentication.Domain.Models;
using AuthenticationService.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Patient>().ToTable("Patients");
        builder.Entity<Patient>().HasKey(p => p.Id);
        builder.Entity<Patient>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Patient>().Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Patient>().Property(p => p.Email).IsRequired().HasMaxLength(255);
        builder.Entity<Patient>().Property(p => p.Password).IsRequired().HasMaxLength(255);
        builder.Entity<Patient>().Property(p => p.Age).IsRequired().HasMaxLength(3);
        
        // Apply Snake Case Naming Convention
        builder.UseSnakeCaseNamingConvention();
    }
}