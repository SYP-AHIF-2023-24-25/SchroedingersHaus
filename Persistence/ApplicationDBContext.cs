using Base.Tools;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Parameterless constructor reads the connection string from appsettings.json (at design time)
    /// For migration generation! Note: The constructor must be the first one in order.
    /// </summary>
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Challenge> Challenges => Set<Challenge>();
    public DbSet<Lobby> Lobbies => Set<Lobby>();
    public DbSet<Diary> Diaries => Set<Diary>();
    public DbSet<GameState> GameStates => Set<GameState>();
    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //We need this for migration
            var connectionString = ConfigurationHelper.GetConfiguration().Get("DefaultConnection", "ConnectionStrings");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}