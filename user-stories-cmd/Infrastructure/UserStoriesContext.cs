using Microsoft.EntityFrameworkCore;
using Infrastructure.DataModel;

namespace Infrastructure;

public class UserStoriesContext : DbContext
{
    public virtual DbSet<UserStoryDataModel> UserStories { get; set; }

    public UserStoriesContext(DbContextOptions<UserStoriesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserStoryDataModel>()
            .HasKey(x => x.Id);

        base.OnModelCreating(modelBuilder);
    }
}
