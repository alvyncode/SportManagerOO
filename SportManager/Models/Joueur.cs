using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SportManager.Models;

public partial class Joueur : ObservableObject
{
    public int Id { get; set; }
    private string _nom;
    private string _prenom;
    private Poste _poste;
    private int _score;
    private int _vitesse;
    private int _endurance;
    private int _force;
    private int _technique;
    private bool _blessure;
}
