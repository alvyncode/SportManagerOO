using System;
using Microsoft.EntityFrameworkCore;
using SportManager.Models;

namespace SportManager.Data.Repositories;

public class JoueurRepository
{
    private SportManagerDBContext _context;
    public JoueurRepository()
    {
        var ConnexionString = "server=localhost;user=root;password=;database=sport_manager_oo_db";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
        var optionsBuilder = new DbContextOptionsBuilder<SportManagerDBContext>();
        optionsBuilder.UseMySql(ConnexionString,serverVersion);
        _context = new SportManagerDBContext(optionsBuilder.Options);
    }
}
