using SportManager.Models;

namespace SportManager.Data;

public class MatchRepository
{
    private readonly List<Equipe> _equipes;
    private readonly List<MatchHistorique> _historique;

    public MatchRepository()
    {
        _equipes = new List<Equipe>
        {
            new Equipe { Id = 1, Nom = "PSG" },
            new Equipe { Id = 2, Nom = "OM" },
            new Equipe { Id = 3, Nom = "Lyon" },
            new Equipe { Id = 4, Nom = "Lille" },
            new Equipe { Id = 5, Nom = "Barça" },
            new Equipe { Id = 6, Nom = "Real" }
        };

        _historique = new List<MatchHistorique>
        {
            new MatchHistorique { Id = 1, Equipe1 = "Équipe 1", Equipe2 = "Équipe 2", Score = "2 - 1" },
            new MatchHistorique { Id = 2, Equipe1 = "PSG", Equipe2 = "OM", Score = "3 - 0" },
            new MatchHistorique { Id = 3, Equipe1 = "Lyon", Equipe2 = "Lille", Score = "1 - 1" },
            new MatchHistorique { Id = 4, Equipe1 = "Barça", Equipe2 = "Real", Score = "2 - 2" }
        };
    }

    public List<Equipe> GetEquipes()
    {
        return _equipes;
    }

    public List<MatchHistorique> GetHistorique()
    {
        return _historique
            .OrderByDescending(m => m.Id)
            .ToList();
    }

    public MatchHistorique JouerMatch(Equipe equipe1, Equipe equipe2)
    {
        Random random = new();

        int score1 = random.Next(0, 6);
        int score2 = random.Next(0, 6);

        var nouveauMatch = new MatchHistorique
        {
            Id = _historique.Count + 1,
            Equipe1 = equipe1.Nom,
            Equipe2 = equipe2.Nom,
            Score = $"{score1} - {score2}"
        };

        _historique.Add(nouveauMatch);
        return nouveauMatch;
    }
}