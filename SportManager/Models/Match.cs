using CommunityToolkit.Mvvm.ComponentModel;

namespace SportManager.Models;

public partial class Match
{
    public int Id { get; set; }

    public int PremiereEquipeId { get; set; }
    public Equipe PremiereEquipe { get; set; } = null!;

    public int DeuxiemeEquipeId { get; set; }
    public Equipe DeuxiemeEquipe { get; set; } = null!;

    public int ScorePremiereEquipe { get; set; }
    public int ScoreDeuxiemeEquipe { get; set; }
}
