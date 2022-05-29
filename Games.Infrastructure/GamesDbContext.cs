using Games.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Infrastructure;

public class GamesDbContext : DbContext
{
    public GamesDbContext(DbContextOptions<GamesDbContext> options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = true;
    }

    public DbSet<Domain.Models.Game> Games { get; set; }
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Models.Game>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Genre>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Genre>()
            .HasOne(p => p.Game)
            .WithMany(t => t.GameGenres)
            .HasForeignKey(p => p.GameId);
    }
}