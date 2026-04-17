using CommunityToolkit.Mvvm.ComponentModel;

namespace SportManager.Models;

public partial class Match : ObservableObject
{
    public int Id { get;set;}
    public Equipe PremiereEquipe { get; set; }
    public Equipe DeuxiemeEquipe { get; set; }
    public int ScorePremiereEquipe { get; set; }
    public int ScoreDeuxiemeEquipe {get; set;}
}
