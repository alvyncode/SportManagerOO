using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SportManager.Models;

public partial class Equipe : ObservableObject
{
    [ObservableProperty]
    private string _nom;
    [ObservableProperty]
    private int _score;
    private ObservableCollection<Joueur> Joueurs;
}
