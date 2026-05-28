using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;
using TaskFlow.Models.Enums;

namespace TaskFlow.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkTask> Tasks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserTask> UserTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserTask>()
            .HasKey(ut => new { ut.UserId, ut.TaskId });

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<WorkTask>()
            .HasQueryFilter(t => !t.IsDeleted);
        
        modelBuilder.Entity<Project>()
            .HasQueryFilter(p => !p.IsDeleted);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<WorkTask>()
            .Property(t => t.Status)
            .HasConversion<string>();

        modelBuilder.Entity<WorkTask>()
            .Property(t => t.Priority)
            .HasConversion<string>();
    }
}