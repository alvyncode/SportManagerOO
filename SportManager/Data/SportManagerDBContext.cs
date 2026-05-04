using SportManager.Models;
using Microsoft.EntityFrameworkCore;
namespace SportManager.Data;

public class SportManagerDBContext : DbContext
{
    public SportManagerDBContext()
    {
    }
    public SportManagerDBContext(DbContextOptions<SportManagerDBContext> options) 
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "server=localhost;user=root;password=;database=sport_manager_oo_db";
            
            optionsBuilder.UseMySql(
                connectionString, 
                ServerVersion.AutoDetect(connectionString)
            );
        }
    }
    public DbSet<Joueur> Joueurs { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<Match> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Joueur>()
            .Property(j => j.Blessure)
            .HasColumnType("tinyint(1)")
            .HasDefaultValue(false);
    }
}