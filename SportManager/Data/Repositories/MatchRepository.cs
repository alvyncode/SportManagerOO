using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using SportManager.Models;
namespace SportManager.Data.Repositories;

public class MatchRepository
{
    private readonly SportManagerDBContext _context;
    public MatchRepository(SportManagerDBContext context)
    {
        var ConnexionString = "server=localhost;user=root;password=;database=sport_manager_oo_db";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
        var optionsBuilder = new DbContextOptionsBuilder<SportManagerDBContext>();
        optionsBuilder.UseMySql(ConnexionString,serverVersion);
        _context = new SportManagerDBContext(optionsBuilder.Options);
    }
    public List<Match> AfficherHistoriqueDesMatch()
    {
        var listeDeMatchs = _context.Matches.ToList();
        if (listeDeMatchs == null)
        {
            return null;
        }
        else
        {
            return listeDeMatchs;
        }
    }
}
