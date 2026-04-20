using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using SportManager.Models;
namespace SportManager.ViewModels;

[QueryProperty(nameof(NomEquipe), "Nom")]
public partial class VoirEquipeUIViewModel : ObservableObject
{
    private string nomEquipe;
    public string NomEquipe
    {
        get { return nomEquipe; }
        set { nomEquipe = value; _ = ChargerEquipe(value); }
    }
    public EquipeRepository EquipeAccess { get; set; }
    public AsyncRelayCommand ButtonRecruter{ get; set; }
    public AsyncRelayCommand<int> ButtonModifier { get; set; }
    public AsyncRelayCommand<int> ButtonSupprimerJoueur { get; set; }

    [ObservableProperty]
    private Equipe _newEquipe;
    public VoirEquipeUIViewModel(EquipeRepository equipeAccess)
    {
        EquipeAccess = equipeAccess;
        ButtonRecruter = new AsyncRelayCommand(NaviguerVersRecrutementPage);
        ButtonModifier = new AsyncRelayCommand<int>(NaviguerVersModificationDetailJoueur);
        ButtonSupprimerJoueur = new AsyncRelayCommand<int>(SupprimerJoueur);
    }
    public async Task NaviguerVersRecrutementPage()
    {
        if (string.IsNullOrWhiteSpace(NomEquipe))
        {
            await Shell.Current.GoToAsync("RecrutementPage");
            return;
        }

        var nomEquipeEncode = Uri.EscapeDataString(NomEquipe);
        await Shell.Current.GoToAsync($"RecrutementPage?Nom={nomEquipeEncode}");
    }
    public async Task NaviguerVersModificationDetailJoueur(int joueurId)
    {
        if (joueurId <= 0)
        {
            return;
        }

        var nomEquipeEncode = Uri.EscapeDataString(NomEquipe ?? string.Empty);
        await Shell.Current.GoToAsync($"ModificationDetailJoueurView?JoueurId={joueurId}&Nom={nomEquipeEncode}");
    }
    public async Task SupprimerJoueur(int joueurId)
    {
        if (joueurId <= 0)
        {
            return;
        }

        var confirmation = await Shell.Current.DisplayAlert(
            "Suppression",
            "Voulez-vous supprimer ce joueur ?",
            "Oui",
            "Non");

        if (!confirmation)
        {
            return;
        }

        await EquipeAccess.SupprimerJoueur(joueurId);
        await ChargerEquipe(NomEquipe);
    }
    private async Task ChargerEquipe(string nomEquipe)
    {
        var equipeTrouvee = await EquipeAccess.RetrouverEquipe(nomEquipe);

        if (equipeTrouvee != null)
        {
            equipeTrouvee.Joueurs = new ObservableCollection<Joueur>(
                equipeTrouvee.Joueurs
                    .OrderBy(j => j.Poste == Poste.Remplacant)
                    .ThenBy(j => j.Poste)
                    .ThenBy(j => j.Nom)
                    .ThenBy(j => j.Prenom));

            NewEquipe = equipeTrouvee;
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"Aucune équipe trouvée avec le nom {nomEquipe}");
        }
    }
}
