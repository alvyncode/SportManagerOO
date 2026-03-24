using SportManager.Models;
using Microsoft.EntityFrameworkCore;
namespace SportManager.Data;

public class SportManagerDBContext: DbContext
{
    public SportManagerDBContext()
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
    public DbSet<Equipe> Equipes {get; set; }
    public DbSet<Match> Matches { get; set; }
}