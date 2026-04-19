using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using SportManager.Models;
using System;
using System.Collections.ObjectModel;
using Windows.UI.WebUI;

namespace SportManager.ViewModels;

public partial class GestionEquipeUIViewModel: ObservableObject 
{
    public AsyncRelayCommand ButtonNouvelleEquipe { get;}
    public AsyncRelayCommand<string> ButtonVoirEquipe { get;}
    public AsyncRelayCommand<string> ButtonSupprimerEquipe { get;}
    public EquipeRepository EquipeAccess { get;} = new();

    [ObservableProperty]
    private ObservableCollection<Equipe> _listeDesEquipes = new() ;
    public GestionEquipeUIViewModel()
    {
        ButtonNouvelleEquipe = new AsyncRelayCommand(NaviguerVersListeEquipe);
        ButtonVoirEquipe = new AsyncRelayCommand<string>(NaviguerVersVoirEquipe);
        ButtonSupprimerEquipe = new AsyncRelayCommand<string>(ExecuterSupprimer);
    }
    public async Task NaviguerVersListeEquipe()
    {
        await Shell.Current.GoToAsync("NouvelleEquipeConnexionMenu");
    }
    public async Task NaviguerVersVoirEquipe(string nomDeLequipe)
    {
        await Shell.Current.GoToAsync($"VoirEquipeUI?Nom={nomDeLequipe}");
    }
    public async Task ExecuterSupprimer(string nomDeLEquipe)
    {
        EquipeAccess.SupprimerEquipe(nomDeLEquipe);
        var equipeASupprimer = ListeDesEquipes.FirstOrDefault(e => e.Nom == nomDeLEquipe); // (Adapte la propriété Nom selon ta classe)
        if (equipeASupprimer != null)
        {
            ListeDesEquipes.Remove(equipeASupprimer);
        }
    }
    public async Task ChargerToutesLesEquipesAsync()
    {
        try 
        {
            var equipesDepuisBDD = await EquipeAccess.ListeEquipe();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ListeDesEquipes.Clear();
                foreach (var equipe in equipesDepuisBDD)
                {
                    ListeDesEquipes.Add(equipe);
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur : {ex.Message}");
        }
    }
}
