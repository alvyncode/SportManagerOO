using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using SportManager.Models;

namespace SportManager.ViewModels;

public partial class GestionMatchViewModel : ObservableObject
{
    private readonly MatchRepository _repository;
    private bool _hasJoueursBlesses;

    public ObservableCollection<Equipe> Equipes { get; } = new();
    public ObservableCollection<string> JoueursBlessesAffiches { get; } = new();

    [ObservableProperty]
    private Equipe? equipe1Selectionnee;

    [ObservableProperty]
    private Equipe? equipe2Selectionnee;

    [ObservableProperty]
    private string scoreAffiche = "-";

    public bool HasJoueursBlesses
    {
        get => _hasJoueursBlesses;
        set => SetProperty(ref _hasJoueursBlesses, value);
    }

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
            await Shell.Current.DisplayAlert("Erreur", "Veuillez choisir deux equipes.", "OK");
            return;
        }

        if (Equipe1Selectionnee.Id == Equipe2Selectionnee.Id)
        {
            await Shell.Current.DisplayAlert("Erreur", "Tu ne peux pas faire jouer la meme equipe contre elle-meme.", "OK");
            return;
        }

        var simulationResult = _repository.JouerMatch(Equipe1Selectionnee, Equipe2Selectionnee);
        var match = simulationResult.Match;

        ScoreAffiche = $"{match.ScorePremiereEquipe} - {match.ScoreDeuxiemeEquipe}";

        JoueursBlessesAffiches.Clear();
        foreach (var joueur in simulationResult.JoueursBlesses)
        {
            var libelle = $"{joueur.Prenom} {joueur.Nom} - score reduit de 50%";
            JoueursBlessesAffiches.Add(libelle);
        }

        HasJoueursBlesses = JoueursBlessesAffiches.Count > 0;

        await Shell.Current.DisplayAlert(
            "Resultat",
            $"{match.PremiereEquipe.Nom} {match.ScorePremiereEquipe} - {match.ScoreDeuxiemeEquipe} {match.DeuxiemeEquipe.Nom}",
            "OK");
    }
}
