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

    public Match JouerMatch(Equipe equipe1, Equipe equipe2)
{
    var premiereEquipe = _context.Equipes
        .FirstOrDefault(e => e.Id == equipe1.Id);

    var deuxiemeEquipe = _context.Equipes
        .FirstOrDefault(e => e.Id == equipe2.Id);

    if (premiereEquipe == null || deuxiemeEquipe == null)
    {
        throw new Exception("Une ou les deux équipes sont introuvables en base.");
    }

    var match = new Match
    {
        PremiereEquipeId = premiereEquipe.Id,
        DeuxiemeEquipeId = deuxiemeEquipe.Id,
        ScorePremiereEquipe = CalculerScoreMatch(premiereEquipe.Score, deuxiemeEquipe.Score),
        ScoreDeuxiemeEquipe = CalculerScoreMatch(deuxiemeEquipe.Score, premiereEquipe.Score)
    };

    _context.Matches.Add(match);
    _context.SaveChanges();

    match.PremiereEquipe = premiereEquipe;
    match.DeuxiemeEquipe = deuxiemeEquipe;

    return match;
}

    public List<MatchHistorique> GetHistorique()
    {
        return _context.Matches
            .AsNoTracking()
            .Include(m => m.PremiereEquipe)
            .Include(m => m.DeuxiemeEquipe)
            .OrderByDescending(m => m.Id)
            .Select(m => new MatchHistorique
            {
                Id = m.Id,
                Equipe1 = m.PremiereEquipe.Nom,
                Equipe2 = m.DeuxiemeEquipe.Nom,
                Score = $"{m.ScorePremiereEquipe} - {m.ScoreDeuxiemeEquipe}"
            })
            .ToList();
    }

    private static int CalculerScoreMatch(int scoreEquipe, int scoreAdverse)
    {
        var random = new Random();
    
        // Différence de niveau entre les 2 équipes
        int difference = scoreEquipe - scoreAdverse;
    
        // Base aléatoire
        int score = random.Next(0, 3); // 0, 1 ou 2
    
        // Bonus si l'équipe est plus forte
        if (difference > 0)
            score += 1;
    
        // Bonus supplémentaire si elle est beaucoup plus forte
        if (difference >= 20)
            score += 1;
    
        // Malus si elle est plus faible
        if (difference <= -20)
            score -= 1;
    
        // Empêcher les scores négatifs et limiter le max
        return Math.Clamp(score, 0, 5);
    }
}
