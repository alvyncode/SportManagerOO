using CommunityToolkit.Mvvm.ComponentModel;

namespace SportManager.Models;

public partial class Match : ObservableObject
{
    public int Id { get;set;}
    private Equipe _premiereEquipe;
    private Equipe _deuxiemeEquipe;
    private int _scorePremiereEquipe;
    private int _scoreDeuxiemeEquipe;
}
