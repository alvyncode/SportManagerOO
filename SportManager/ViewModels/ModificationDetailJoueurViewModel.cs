using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using SportManager.Models;

namespace SportManager.ViewModels;

[QueryProperty(nameof(NomEquipe), "Nom")]
[QueryProperty(nameof(JoueurId), "JoueurId")]
public partial class ModificationDetailJoueurViewModel: ObservableObject
{
    private readonly EquipeRepository _equipeRepository = new();
    private const int ValeurMinCaracteristique = 0;
    private const int ValeurMaxCaracteristique = 100;
    private static readonly IReadOnlyDictionary<Poste, (double vitesse, double endurence, double technique, double force)> PoidsParPoste
        = new Dictionary<Poste, (double, double, double, double)>
        {
            [Poste.Remplacant] = (1.0, 1.0, 1.0, 1.0),
            [Poste.Meneur] = (1.4, 1.0, 1.6, 0.8),
            [Poste.Pivot] = (0.8, 1.4, 0.9, 1.8),
            [Poste.AilierDroit] = (1.5, 1.0, 1.3, 0.9),
            [Poste.AilierGauche] = (1.5, 1.0, 1.3, 0.9),
            [Poste.Arriere] = (1.1, 1.2, 1.2, 1.3)
        };

    public AsyncRelayCommand ButtonValiderModification { get; }
    public List<string> PostesDisponibles { get; } = Enum.GetNames<Poste>().ToList();

    [ObservableProperty]
    private string nomEquipe = string.Empty;

    [ObservableProperty]
    private int joueurId;

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

    [ObservableProperty]
    private string posteSaisi = nameof(Poste.Remplacant);

    public int ScoreCalcule => CalculerScoreCalculeSelonPoste();

    public ModificationDetailJoueurViewModel()
    {
        ButtonValiderModification = new AsyncRelayCommand(ValiderModification);
    }

    partial void OnJoueurIdChanged(int value)
    {
        if (value > 0)
        {
            _ = ChargerJoueur(value);
        }
    }

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

    partial void OnPosteSaisiChanged(string value) => OnPropertyChanged(nameof(ScoreCalcule));

    private async Task ChargerJoueur(int idJoueur)
    {
        var joueur = await _equipeRepository.RetrouverJoueurParId(idJoueur);
        if (joueur == null)
        {
            return;
        }

        Nom = joueur.Nom;
        Prenom = joueur.Prenom;
        Vitesse = joueur.Vitesse;
        Endurence = joueur.Endurence;
        Technique = joueur.Technique;
        Force = joueur.Force;
        PosteSaisi = joueur.Poste.ToString();
    }

    public async Task ValiderModification()
    {
        if (string.IsNullOrWhiteSpace(Nom) || string.IsNullOrWhiteSpace(Prenom))
        {
            await Shell.Current.DisplayAlertAsync("Champs requis", "Le nom et le prénom sont obligatoires.", "OK");
            return;
        }

        if (!Enum.TryParse<Poste>(PosteSaisi, true, out var poste))
        {
            await Shell.Current.DisplayAlertAsync("Poste invalide", "Veuillez choisir un poste valide.", "OK");
            return;
        }

        var posteDejaPris = await _equipeRepository.PosteDejaPrisDansEquipe(NomEquipe, poste, JoueurId);
        if (posteDejaPris)
        {
            await Shell.Current.DisplayAlertAsync("Poste deja pris", "Ce poste est deja occupe dans l'equipe.", "OK");
            return;
        }

        var vitesseValidee = Math.Clamp(Vitesse, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var endurenceValidee = Math.Clamp(Endurence, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var techniqueValidee = Math.Clamp(Technique, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var forceValidee = Math.Clamp(Force, ValeurMinCaracteristique, ValeurMaxCaracteristique);
        var scoreCalcule = (vitesseValidee + endurenceValidee + techniqueValidee + forceValidee) / 4;

        var joueurModifie = new Joueur
        {
            Id = JoueurId,
            Nom = Nom.Trim(),
            Prenom = Prenom.Trim(),
            Poste = poste,
            Score = scoreCalcule,
            Vitesse = vitesseValidee,
            Endurence = endurenceValidee,
            Technique = techniqueValidee,
            Force = forceValidee
        };

        await _equipeRepository.ModifierJoueur(joueurModifie);

        var nomEquipeEncode = Uri.EscapeDataString(NomEquipe ?? string.Empty);
        await Shell.Current.GoToAsync($"VoirEquipeUI?Nom={nomEquipeEncode}");
    }

    private int CalculerScoreCalculeSelonPoste()
    {
        if (!Enum.TryParse<Poste>(PosteSaisi, true, out var poste))
        {
            poste = Poste.Remplacant;
        }

        var (poidsVitesse, poidsEndurence, poidsTechnique, poidsForce) =
            PoidsParPoste.TryGetValue(poste, out var p)
                ? p
                : (1.0, 1.0, 1.0, 1.0);

        var scorePondere = (Vitesse * poidsVitesse)
                         + (Endurence * poidsEndurence)
                         + (Technique * poidsTechnique)
                         + (Force * poidsForce);

        return (int)Math.Round(scorePondere / 4.0, MidpointRounding.AwayFromZero);
    }

}
