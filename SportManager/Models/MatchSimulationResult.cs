using System.Collections.Generic;

namespace SportManager.Models;

public class MatchSimulationResult
{
    public Match Match { get; set; } = null!;
    public List<Joueur> JoueursBlesses { get; set; } = new();
}
