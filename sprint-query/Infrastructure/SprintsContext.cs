using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;

namespace Infrastructure;

public class SprintsContext : DbContext
{
    public virtual DbSet<SprintDataModel> Sprints { get; set; }
    public virtual DbSet<ProjectDataModel> Projects { get; set; }

    public SprintsContext(DbContextOptions<SprintsContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SprintDataModel>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<SprintDataModel>()
            .OwnsOne(x => x.Period);

        modelBuilder.Entity<ProjectDataModel>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<ProjectDataModel>()
            .OwnsOne(x => x.Period);


        base.OnModelCreating(modelBuilder);
    }
}
