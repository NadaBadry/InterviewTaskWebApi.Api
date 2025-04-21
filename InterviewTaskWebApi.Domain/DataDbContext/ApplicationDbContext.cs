using InterviewTaskWebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewTaskWebApi.Domain.DataDbContext;
public class ApplicationDbContext:DbContext
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(p => p.Id);//primary key
            entity.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Description).HasMaxLength(300);
            entity.Property(p => p.StartDate).IsRequired();
            entity.Property(p => p.EndDate).IsRequired();
            entity.Property(p => p.Status).IsRequired();

        });
        modelBuilder.Entity<TaskModel>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasDefaultValueSql("NEWID()");
            entity.Property(t=>t.Title).IsRequired();
            entity.Property(t=>t.Status).IsRequired();
            entity.HasOne(t => t.Project)//task in one project
            .WithMany(p => p.Tasks)//project has many tasks
            .HasForeignKey(t => t.ProjectId);
            


        });
    }
}
