using assecor_assessment_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace assecor_assessment_backend.Global;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    
    public DbSet<Color> Colors { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var personModel = modelBuilder.Entity<Person>();
        
        personModel.HasKey(p => p.Id);
        personModel.HasOne(p => p.Color)
            .WithMany()
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.ColorId)
            .OnDelete(DeleteBehavior.SetNull);
        
        var colorModel = modelBuilder.Entity<Color>();
        
        colorModel.HasKey(c => c.Id);
        
        personModel.Navigation(p => p.Color).AutoInclude();
    }
}