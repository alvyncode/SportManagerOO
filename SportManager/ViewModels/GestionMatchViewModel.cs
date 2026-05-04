using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using SportManager.Models;

namespace SportManager.ViewModels;

public class GestionMatchViewModel : ObservableObject
{
    private readonly MatchRepository _repository;

    private Equipe? _equipe1Selectionnee;
    private Equipe? _equipe2Selectionnee;
    private string _scoreEquipeGaucheAffiche = "-";
    private string _scoreEquipeDroiteAffiche = "-";
    private bool _hasJoueursBlesses;

    public ObservableCollection<Equipe> Equipes { get; } = new();
    public ObservableCollection<string> JoueursBlessesAffiches { get; } = new();

    public Equipe? Equipe1Selectionnee
    {
        get => _equipe1Selectionnee;
        set => SetProperty(ref _equipe1Selectionnee, value);
    }

    public Equipe? Equipe2Selectionnee
    {
        get => _equipe2Selectionnee;
        set => SetProperty(ref _equipe2Selectionnee, value);
    }

    public string ScoreEquipeGaucheAffiche
    {
        get => _scoreEquipeGaucheAffiche;
        set => SetProperty(ref _scoreEquipeGaucheAffiche, value);
    }

    public string ScoreEquipeDroiteAffiche
    {
        get => _scoreEquipeDroiteAffiche;
        set => SetProperty(ref _scoreEquipeDroiteAffiche, value);
    }

    public bool HasJoueursBlesses
    {
        get => _hasJoueursBlesses;
        set => SetProperty(ref _hasJoueursBlesses, value);
    }

    public ICommand JouerMatchCommand { get; }

    public GestionMatchViewModel()
    {
        _repository = new MatchRepository();
        JouerMatchCommand = new AsyncRelayCommand(JouerMatch);
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

        ScoreEquipeGaucheAffiche = match.ScorePremiereEquipe.ToString();
        ScoreEquipeDroiteAffiche = match.ScoreDeuxiemeEquipe.ToString();

        JoueursBlessesAffiches.Clear();
        foreach (var joueur in simulationResult.JoueursBlesses)
        {
            JoueursBlessesAffiches.Add($"{joueur.Prenom} {joueur.Nom}");
        }

        HasJoueursBlesses = JoueursBlessesAffiches.Count > 0;

        await Shell.Current.DisplayAlert(
            "Resultat",
            $"{match.PremiereEquipe.Nom} {match.ScorePremiereEquipe} - {match.ScoreDeuxiemeEquipe} {match.DeuxiemeEquipe.Nom}",
            "OK");
    }
}
