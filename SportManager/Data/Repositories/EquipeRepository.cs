using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using SportManager.Models;

namespace SportManager.Data.Repositories;

public class EquipeRepository
{
    private readonly SportManagerDBContext _context;
    public EquipeRepository(SportManagerDBContext context)
    {
        var ConnexionString = "server=localhost;user=root;password=;database=sport_manager_oo_db";
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
        var optionsBuilder = new DbContextOptionsBuilder<SportManagerDBContext>();
        optionsBuilder.UseMySql(ConnexionString,serverVersion);
        _context = new SportManagerDBContext(optionsBuilder.Options);
    }
    public Equipe EnregistrerEtRecupererEquipe(Equipe equipe)
    {
        var equipeExistante = _context.Equipes
            .FirstOrDefault(j => j.Nom.ToLower() == equipe.Nom.ToLower());

        if (equipeExistante != null)
        {
            _context.Equipes.Update(equipeExistante);
            equipe = equipeExistante; 
        }
        else
        {
            _context.Equipes.Add(equipeExistante);
        }
        _context.SaveChanges();
        return equipe;
    }
    public ObservableCollection<Joueur> ListeEquipe(string nomDeLEquipe)
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
}
