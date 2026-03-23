using System;
using CommunityToolkit.Mvvm.ComponentModel;
namespace SportManager.Models;

public partial class Joueur : ObservableObject
{
    [ObservableProperty]
    private string _nom;
    [ObservableProperty]
    private string _prenom;
    [ObservableProperty]
    private Poste _poste;
    [ObservableProperty]
    private int _score;
    [ObservableProperty]
    private int _vitesse;
    [ObservableProperty]
    private int _endurance;
    [ObservableProperty]
    private int _force;

    [ObservableProperty]
    private int _technique;

    [ObservableProperty]
    private bool _blessure;
}
