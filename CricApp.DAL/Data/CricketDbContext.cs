using CricApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CricApp.DAL.Data;

public class CricketDbContext : DbContext
{
    public CricketDbContext(DbContextOptions<CricketDbContext> options)
        : base(options)
    {
    }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Innings> Innings => Set<Innings>();
    public DbSet<Ball> Balls => Set<Ball>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            entity.Property(t => t.ShortName).HasMaxLength(20);
            entity.HasMany(t => t.Players)
                  .WithOne(p => p.Team)
                  .HasForeignKey(p => p.TeamId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Role).HasMaxLength(100);
            entity.HasOne(p => p.Team)
                  .WithMany(t => t.Players)
                  .HasForeignKey(p => p.TeamId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.MatchName).IsRequired().HasMaxLength(200);
            entity.Property(m => m.Venue).HasMaxLength(200);
            entity.Property(m => m.Status).HasMaxLength(50);

            entity.HasOne(m => m.TeamOne)
                  .WithMany()
                  .HasForeignKey(m => m.TeamOneId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.TeamTwo)
                  .WithMany()
                  .HasForeignKey(m => m.TeamTwoId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(m => m.Innings)
                  .WithOne(i => i.Match)
                  .HasForeignKey(i => i.MatchId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Innings>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.HasOne(i => i.Match)
                  .WithMany(m => m.Innings)
                  .HasForeignKey(i => i.MatchId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.Team)
                  .WithMany()
                  .HasForeignKey(i => i.TeamId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(i => i.Balls)
                  .WithOne(b => b.Innings)
                  .HasForeignKey(b => b.InningsId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Ball>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Commentary).HasMaxLength(500);
            entity.HasOne(b => b.Innings)
                  .WithMany(i => i.Balls)
                  .HasForeignKey(b => b.InningsId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
