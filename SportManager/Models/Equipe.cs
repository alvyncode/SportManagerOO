using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SportManager.Models;

public partial class Equipe : ObservableObject
{
    public int Id { get; set; }
    public string Nom {get;set;}
    public int Score { get; set; }
    public ObservableCollection<Joueur> Joueurs {get;set;}
}
