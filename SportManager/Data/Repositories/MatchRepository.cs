using Microsoft.EntityFrameworkCore;
using SportManager.Models;

namespace SportManager.Data.Repositories;

public class MatchRepository
{
    private readonly SportManagerDBContext _context;
    private const double BlessureChanceParEquipe = 1.0; // 1.0 = 100% (mettre 0.25 pour 25%)

    public MatchRepository()
    {
        _context = new SportManagerDBContext();
    }

    public MatchRepository(SportManagerDBContext context)
    {
        _context = context;
    }

    public List<Equipe> GetEquipes()
    {
        var equipes = _context.Equipes
            .AsNoTracking()
            .Include(e => e.Joueurs)
            .OrderBy(e => e.Nom)
            .ToList();

        foreach (var equipe in equipes)
        {
            equipe.Score = CalculerScoreEquipeAvecBlessures(equipe);
        }

        return equipes;
    }

    public MatchSimulationResult JouerMatch(Equipe equipe1, Equipe equipe2)
    {
        var premiereEquipe = _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefault(e => e.Id == equipe1.Id);

        var deuxiemeEquipe = _context.Equipes
            .Include(e => e.Joueurs)
            .FirstOrDefault(e => e.Id == equipe2.Id);

        if (premiereEquipe == null || deuxiemeEquipe == null)
        {
            throw new Exception("Une ou les deux equipes sont introuvables en base.");
        }

        MettreAJourScoreEquipeAvecBlessures(premiereEquipe);
        MettreAJourScoreEquipeAvecBlessures(deuxiemeEquipe);

        var match = new Match
        {
            PremiereEquipeId = premiereEquipe.Id,
            DeuxiemeEquipeId = deuxiemeEquipe.Id,
            ScorePremiereEquipe = CalculerScoreMatch(premiereEquipe.Score, deuxiemeEquipe.Score),
            ScoreDeuxiemeEquipe = CalculerScoreMatch(deuxiemeEquipe.Score, premiereEquipe.Score)
        };

        var random = new Random();
        var joueursBlesses = new List<Joueur>();

        AjouterJoueursActuellementBlesses(premiereEquipe, joueursBlesses);
        AjouterJoueursActuellementBlesses(deuxiemeEquipe, joueursBlesses);

        GererBlessuresEquipe(premiereEquipe.Id, random, joueursBlesses);
        GererBlessuresEquipe(deuxiemeEquipe.Id, random, joueursBlesses);

        MettreAJourScoreEquipeAvecBlessures(premiereEquipe);
        MettreAJourScoreEquipeAvecBlessures(deuxiemeEquipe);

        _context.Matches.Add(match);
        _context.SaveChanges();

        match.PremiereEquipe = premiereEquipe;
        match.DeuxiemeEquipe = deuxiemeEquipe;

        return new MatchSimulationResult
        {
            Match = match,
            JoueursBlesses = joueursBlesses
        };
    }

    public List<Match> GetHistorique()
    {
        return _context.Matches
            .AsNoTracking()
            .Include(m => m.PremiereEquipe)
            .Include(m => m.DeuxiemeEquipe)
            .OrderByDescending(m => m.Id)
            .ToList();
    }

    private void GererBlessuresEquipe(int equipeId, Random random, List<Joueur> joueursBlesses)
    {
        if (random.NextDouble() > BlessureChanceParEquipe)
        {
            return;
        }

        var joueursDisponibles = _context.Joueurs
            .Where(j => j.EquipeId == equipeId && !j.Blessure)
            .ToList();

        if (joueursDisponibles.Count == 0)
        {
            return;
        }

        var joueurBlesse = joueursDisponibles[random.Next(joueursDisponibles.Count)];
        joueurBlesse.Blessure = true;

        if (joueursBlesses.All(j => j.Id != joueurBlesse.Id))
        {
            joueursBlesses.Add(joueurBlesse);
        }
    }

    private static void AjouterJoueursActuellementBlesses(Equipe equipe, List<Joueur> joueursBlesses)
    {
        if (equipe.Joueurs == null)
        {
            return;
        }

        foreach (var joueur in equipe.Joueurs.Where(j => j.Blessure))
        {
            if (joueursBlesses.All(j => j.Id != joueur.Id))
            {
                joueursBlesses.Add(joueur);
            }
        }
    }

    private static void MettreAJourScoreEquipeAvecBlessures(Equipe equipe)
    {
        equipe.Score = CalculerScoreEquipeAvecBlessures(equipe);
    }

    private static int CalculerScoreEquipeAvecBlessures(Equipe equipe)
    {
        if (equipe.Joueurs == null || equipe.Joueurs.Count == 0)
        {
            return 0;
        }

        return equipe.Joueurs
            .Where(j => j.Poste != Poste.Remplacant)
            .Sum(j => j.Blessure ? j.Score / 2 : j.Score);
    }

    private static int CalculerScoreMatch(int scoreEquipe, int scoreAdverse)
    {
        var random = new Random();
    
        // Différence de niveau entre les 2 équipes
        int difference = scoreEquipe - scoreAdverse;
    
        // Base aléatoire
        int score = random.Next(100, 300); // 0, 1 ou 2. J'ai modifier pour que ça correspondent plus à un match de basket
    
        // Bonus si l'équipe est plus forte
        if (difference > 0)
            score += 67;
    
        // Bonus supplémentaire si elle est beaucoup plus forte
        if (difference >= 20)
            score += 67;
    
        // Malus si elle est plus faible
        if (difference <= -20)
            score -= 95;
    
        // Empêcher les scores négatifs et limiter le max
        return Math.Clamp(score, 0, 400);
    }
}
