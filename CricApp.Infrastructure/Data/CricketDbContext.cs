using CricApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CricApp.Infrastructure.Data;

public class CricketDbContext : DbContext
{
    public CricketDbContext(DbContextOptions<CricketDbContext> options) : base(options)
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

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.TeamOne)
            .WithMany()
            .HasForeignKey(m => m.TeamOneId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.TeamTwo)
            .WithMany()
            .HasForeignKey(m => m.TeamTwoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Innings>()
            .HasOne(i => i.Match)
            .WithMany(m => m.Innings)
            .HasForeignKey(i => i.MatchId);

        modelBuilder.Entity<Innings>()
            .HasOne(i => i.Team)
            .WithMany()
            .HasForeignKey(i => i.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ball>()
            .HasOne(b => b.Innings)
            .WithMany(i => i.Balls)
            .HasForeignKey(b => b.InningsId);
    }
}
