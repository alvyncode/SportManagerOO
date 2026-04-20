using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using SportManager.Models;

namespace SportManager.ViewModels;

[QueryProperty(nameof(NomEquipe), "Nom")]
public partial class RecrutementPageViewModel: ObservableObject
{
    private readonly EquipeRepository _equipeRepository;
    private const int ValeurMinCaracteristique = 0;
    private const int ValeurMaxCaracteristique = 100;

    public AsyncRelayCommand ButtonValiderRecrutement { get; }

    [ObservableProperty]
    private string nomEquipe = string.Empty;

    [ObservableProperty]
    private string nom = string.Empty;

    [ObservableProperty]
    private string prenom = string.Empty;

    [ObservableProperty]
    private int vitesse;

    [ObservableProperty]
    private int endurence;

    [ObservableProperty]
    private int technique;

    [ObservableProperty]
    private int force;

    public int ScoreCalcule => (Vitesse + Endurence + Technique + Force) / 4;

    public RecrutementPageViewModel(EquipeRepository equipeRepository)
    {
        _equipeRepository = equipeRepository;
        ButtonValiderRecrutement = new AsyncRelayCommand(RecruterJoueur);
    }

    private bool PeutValiderRecrutement()
        => !string.IsNullOrWhiteSpace(Nom) && !string.IsNullOrWhiteSpace(Prenom);

    partial void OnVitesseChanged(int value)
    {
        if (value < ValeurMinCaracteristique)
        {
            Vitesse = ValeurMinCaracteristique;
            return;
        }

        if (value > ValeurMaxCaracteristique)
        {
            Vitesse = ValeurMaxCaracteristique;
            return;
        }

        OnPropertyChanged(nameof(ScoreCalcule));
    }

    partial void OnEndurenceChanged(int value)
    {
        if (value < ValeurMinCaracteristique)
        {
            Endurence = ValeurMinCaracteristique;
            return;
        }

        if (value > ValeurMaxCaracteristique)
        {
            Endurence = ValeurMaxCaracteristique;
            return;
        }

        OnPropertyChanged(nameof(ScoreCalcule));
    }

    partial void OnTechniqueChanged(int value)
    {
        if (value < ValeurMinCaracteristique)
        {
            Technique = ValeurMinCaracteristique;
            return;
        }

        if (value > ValeurMaxCaracteristique)
        {
            Technique = ValeurMaxCaracteristique;
            return;
        }

        OnPropertyChanged(nameof(ScoreCalcule));
    }

    partial void OnForceChanged(int value)
    {
        if (value < ValeurMinCaracteristique)
        {
            Force = ValeurMinCaracteristique;
            return;
        }

        if (value > ValeurMaxCaracteristique)
        {
            Force = ValeurMaxCaracteristique;
            return;
        }

        OnPropertyChanged(nameof(ScoreCalcule));
    }

    public async Task RecruterJoueur()
    {
        if (!PeutValiderRecrutement())
        {
            await Shell.Current.DisplayAlert("Champs requis", "Le nom et le prénom sont obligatoires.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(NomEquipe))
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        var vitesseValidee = Math.Clamp(Vitesse, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var endurenceValidee = Math.Clamp(Endurence, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var techniqueValidee = Math.Clamp(Technique, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var forceValidee = Math.Clamp(Force, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var scoreCalcule = (vitesseValidee + endurenceValidee + techniqueValidee + forceValidee) / 4;

        var joueur = new Joueur
        {
            Nom = Nom.Trim(),
            Prenom = Prenom.Trim(),
            Poste = Poste.Remplacant,
            Score = scoreCalcule,
            Vitesse = vitesseValidee,
            Endurence = endurenceValidee,
            Force = forceValidee,
            Technique = techniqueValidee,
            Blessure = false
        };

        await _equipeRepository.RecruterJoueur(joueur, NomEquipe);
        var nomEquipeEncode = Uri.EscapeDataString(NomEquipe);
        await Shell.Current.GoToAsync($"VoirEquipeUI?Nom={nomEquipeEncode}");
    }
}
