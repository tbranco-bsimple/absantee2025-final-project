using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;

namespace Infrastructure;

public class AssociationsSprintUserStoryContext : DbContext
{
    public virtual DbSet<AssociationSprintUserStoryDataModel> AssociationsSprintUserStory { get; set; }
    public virtual DbSet<SprintDataModel> Sprints { get; set; }
    public virtual DbSet<UserStoryDataModel> UserStories { get; set; }
    public virtual DbSet<CollaboratorDataModel> Collaborators { get; set; }

    public AssociationsSprintUserStoryContext(DbContextOptions<AssociationsSprintUserStoryContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssociationSprintUserStoryDataModel>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<SprintDataModel>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<SprintDataModel>()
            .OwnsOne(x => x.Period);

        modelBuilder.Entity<UserStoryDataModel>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<CollaboratorDataModel>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<CollaboratorDataModel>()
            .OwnsOne(x => x.Period);


        base.OnModelCreating(modelBuilder);
    }
}
