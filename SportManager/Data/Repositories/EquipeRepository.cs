using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using SportManager.Models;

namespace SportManager.Data.Repositories;

public class EquipeRepository
{
    private SportManagerDBContext _context;
    public EquipeRepository()
    {
        var ConnexionString = "server=localhost;user=root;password=;database=sport_manager_oo_db";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
        var optionsBuilder = new DbContextOptionsBuilder<SportManagerDBContext>();
        optionsBuilder.UseMySql(ConnexionString,serverVersion);
        _context = new SportManagerDBContext(optionsBuilder.Options);
    }
    public async Task<Equipe> EnregistrerEtRecupererEquipe(string nomDeLequipe)
    {
        var equipeExistante = await _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefaultAsync(j => j.Nom.ToLower() == nomDeLequipe.ToLower());
        if (equipeExistante != null)
        {
            return equipeExistante; 
        }

        Equipe nouvelleEquipe = new() { Nom = nomDeLequipe };
        _context.Equipes.Add(nouvelleEquipe);

        await _context.SaveChangesAsync(); 
        
        return nouvelleEquipe;
    }
    public async Task<Equipe?> RetrouverEquipe(string nomDeLequipe)
    {
         return await _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefaultAsync(j => j.Nom.ToLower() == nomDeLequipe.ToLower());
    }
    public ObservableCollection<Joueur> ListeJoueur(string nomDeLEquipe)
    {
        var Equipe = _context.Equipes.FirstOrDefault(p => p.Nom == nomDeLEquipe);
        if (Equipe != null)
        {
            return Equipe.Joueurs;
        }
        else
        {
            throw new NullReferenceException("Inexistant");
        }
    }
    public void RecruterJoueur(Joueur joueur, string nomEquipe)
    {
        var equipe = _context.Equipes.FirstOrDefault(e=>e.Nom == nomEquipe);
        if (equipe != null)
        {
            equipe.Joueurs.Add(joueur);
        }
        else
        {
            throw new NullReferenceException("Inextistant");
        }
        _context.SaveChanges();
        
    }
    public async Task<List<Equipe>> ListeEquipe()
    {
        var l = await _context.Equipes.ToListAsync();
        return l ;
    }
    public async Task SupprimerEquipe(string nomDeLEquipe)
    {
        await _context.Equipes
                    .Where(e => e.Nom == nomDeLEquipe)
                    .ExecuteDeleteAsync();
    }
}
