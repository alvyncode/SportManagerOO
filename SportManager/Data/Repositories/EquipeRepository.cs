using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using SportManager.Models;

namespace SportManager.Data.Repositories;

public class EquipeRepository
{
    private SportManagerDBContext _context;
    private static readonly IReadOnlyDictionary<Poste, (double vitesse, double endurence, double technique, double force)> PoidsParPoste
        = new Dictionary<Poste, (double, double, double, double)>
        {
            [Poste.Remplacant] = (1.0, 1.0, 1.0, 1.0),
            [Poste.Meneur] = (1.4, 1.0, 1.6, 0.8),
            [Poste.Pivot] = (0.8, 1.4, 0.9, 1.8),
            [Poste.AilierDroit] = (1.5, 1.0, 1.3, 0.9),
            [Poste.AilierGauche] = (1.5, 1.0, 1.3, 0.9),
            [Poste.Arriere] = (1.1, 1.2, 1.2, 1.3)
        };

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
            .AsNoTracking()
            .Include(e => e.Joueurs)
            .FirstOrDefaultAsync(j => j.Nom.ToLower() == nomDeLequipe.ToLower());
    }
    public ObservableCollection<Joueur> ListeJoueur(string nomDeLEquipe)
    {
        var Equipe = _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefault(p => p.Nom == nomDeLEquipe);
        if (Equipe != null)
        {
            return Equipe.Joueurs;
        }
        else
        {
            throw new NullReferenceException("Inexistant");
        }
    }
    public async Task RecruterJoueur(Joueur joueur, string nomEquipe)
    {
        var equipe = await _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefaultAsync(e=>e.Nom == nomEquipe);

        if (equipe != null)
        {
            equipe.Joueurs ??= [];
            joueur.EquipeId = equipe.Id;
            joueur.Score = CalculerScoreJoueurSelonPoste(joueur);
            equipe.Joueurs.Add(joueur);
            RecalculerScoreEquipe(equipe);
        }
        else
        {
            throw new NullReferenceException("Inextistant");
        }

        await _context.SaveChangesAsync();
        
    }
    public async Task<Joueur?> RetrouverJoueurParId(int joueurId)
    {
        return await _context.Joueurs
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == joueurId);
    }

    public async Task ModifierJoueur(Joueur joueurModifie)
    {
        var joueurExistant = await _context.Joueurs.FirstOrDefaultAsync(j => j.Id == joueurModifie.Id);
        if (joueurExistant == null)
        {
            throw new NullReferenceException("Joueur introuvable");
        }

        joueurExistant.Nom = joueurModifie.Nom;
        joueurExistant.Prenom = joueurModifie.Prenom;
        joueurExistant.Poste = joueurModifie.Poste;
        joueurExistant.Vitesse = joueurModifie.Vitesse;
        joueurExistant.Endurence = joueurModifie.Endurence;
        joueurExistant.Technique = joueurModifie.Technique;
        joueurExistant.Force = joueurModifie.Force;
        joueurExistant.Score = CalculerScoreJoueurSelonPoste(joueurExistant);

        var equipe = await _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefaultAsync(e => e.Id == joueurExistant.EquipeId);

        if (equipe != null)
        {
            RecalculerScoreEquipe(equipe);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<bool> PosteDejaPrisDansEquipe(string nomEquipe, Poste poste, int joueurIdExclu)
    {
        if (poste == Poste.Remplacant)
        {
            return false;
        }

        var equipe = await _context.Equipes
            .AsNoTracking()
            .Include(e => e.Joueurs)
            .FirstOrDefaultAsync(e => e.Nom.ToLower() == nomEquipe.ToLower());

        if (equipe?.Joueurs == null)
        {
            return false;
        }

        return equipe.Joueurs.Any(j => j.Id != joueurIdExclu && j.Poste == poste);
    }

    public async Task SupprimerJoueur(int joueurId)
    {
        var joueur = await _context.Joueurs
            .AsNoTracking()
            .FirstOrDefaultAsync(j => j.Id == joueurId);

        await _context.Joueurs
            .Where(j => j.Id == joueurId)
            .ExecuteDeleteAsync();

        if (joueur != null)
        {
            var equipe = await _context.Equipes
                .Include(e => e.Joueurs)
                .FirstOrDefaultAsync(e => e.Id == joueur.EquipeId);

            if (equipe != null)
            {
                RecalculerScoreEquipe(equipe);
                await _context.SaveChangesAsync();
            }
        }

        // ExecuteDeleteAsync bypasses tracked entities, so clear the cache to force fresh reloads.
        _context.ChangeTracker.Clear();
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

    private static int CalculerScoreJoueurSelonPoste(Joueur joueur)
    {
        var (poidsVitesse, poidsEndurence, poidsTechnique, poidsForce) =
            PoidsParPoste.TryGetValue(joueur.Poste, out var p)
                ? p
                : (1.0, 1.0, 1.0, 1.0);

        var scorePondere = (joueur.Vitesse * poidsVitesse)
                         + (joueur.Endurence * poidsEndurence)
                         + (joueur.Technique * poidsTechnique)
                         + (joueur.Force * poidsForce);

        // Divide by the number of characteristics so role multipliers can increase score beyond 100.
        var score = scorePondere / 4.0;
        return (int)Math.Round(score, MidpointRounding.AwayFromZero);
    }

    private static void RecalculerScoreEquipe(Equipe equipe)
    {
        if (equipe.Joueurs == null || equipe.Joueurs.Count == 0)
        {
            equipe.Score = 0;
            return;
        }

        equipe.Score = equipe.Joueurs
            .Where(j => j.Poste != Poste.Remplacant)
            .Sum(CalculerScoreJoueurSelonPoste);
    }
}
