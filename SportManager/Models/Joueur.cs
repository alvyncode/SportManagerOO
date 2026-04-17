using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SportManager.Models;

public partial class Joueur : ObservableObject
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public Poste Poste { get; set; }
    public int Score { get; set; }
    public int Vitesse { get; set; }
    public int Endurence { get; set; }
    public int Force { get; set; }
    public int Technique { get; set; }
    public bool Blessure { get; set; }
    public int EquipeId {get;set;}
}
