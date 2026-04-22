using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data;
using SportManager.Models;

namespace SportManager.ViewModels;

public partial class GestionMatchViewModel : ObservableObject
{
    private readonly MatchRepository _repository;

    public ObservableCollection<Equipe> Equipes { get; } = new();

    [ObservableProperty]
    private Equipe? equipe1Selectionnee;

    [ObservableProperty]
    private Equipe? equipe2Selectionnee;

    [ObservableProperty]
    private string scoreAffiche = "-";

    public GestionMatchViewModel()
    {
        _repository = new MatchRepository();
        ChargerEquipes();
    }

    private void ChargerEquipes()
    {
        Equipes.Clear();

        foreach (var equipe in _repository.GetEquipes())
        {
            Equipes.Add(equipe);
        }
    }

    [RelayCommand]
    private async Task JouerMatch()
    {
        if (Equipe1Selectionnee is null || Equipe2Selectionnee is null)
        {
            await Shell.Current.DisplayAlert("Erreur", "Veuillez choisir deux équipes.", "OK");
            return;
        }

        if (Equipe1Selectionnee.Id == Equipe2Selectionnee.Id)
        {
            await Shell.Current.DisplayAlert("Erreur", "Tu ne peux pas faire jouer la même équipe contre elle-même.", "OK");
            return;
        }

        var match = _repository.JouerMatch(Equipe1Selectionnee, Equipe2Selectionnee);
        ScoreAffiche = match.Score;

        await Shell.Current.DisplayAlert(
            "Résultat",
            $"{match.Equipe1} {match.Score} {match.Equipe2}",
            "OK");
    }
}