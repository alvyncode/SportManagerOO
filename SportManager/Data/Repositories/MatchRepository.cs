using Microsoft.EntityFrameworkCore;
using SportManager.Models;

namespace SportManager.Data.Repositories;

public class MatchRepository
{
    private readonly SportManagerDBContext _context;

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
        return _context.Equipes
            .AsNoTracking()
            .OrderBy(e => e.Nom)
            .ToList();
    }

    public MatchSimulationResult JouerMatch(Equipe equipe1, Equipe equipe2)
    {
        var premiereEquipe = _context.Equipes
            .FirstOrDefault(e => e.Id == equipe1.Id);

        var deuxiemeEquipe = _context.Equipes
            .FirstOrDefault(e => e.Id == equipe2.Id);

        if (premiereEquipe == null || deuxiemeEquipe == null)
        {
            throw new Exception("Une ou les deux equipes sont introuvables en base.");
        }

        var match = new Match
        {
            PremiereEquipeId = premiereEquipe.Id,
            DeuxiemeEquipeId = deuxiemeEquipe.Id,
            ScorePremiereEquipe = CalculerScoreMatch(premiereEquipe.Score, deuxiemeEquipe.Score),
            ScoreDeuxiemeEquipe = CalculerScoreMatch(deuxiemeEquipe.Score, premiereEquipe.Score)
        };

        var random = new Random();
        var joueursBlesses = new List<Joueur>();

        GererBlessuresEquipe(premiereEquipe.Id, random, joueursBlesses);
        GererBlessuresEquipe(deuxiemeEquipe.Id, random, joueursBlesses);

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
        if (random.NextDouble() > 0.25)
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
        joueursBlesses.Add(joueurBlesse);
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
