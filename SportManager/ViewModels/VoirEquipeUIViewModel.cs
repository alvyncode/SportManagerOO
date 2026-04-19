using System;
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
        set { nomEquipe = value; ChargerEquipe(value); }
    }
    public EquipeRepository EquipeAccess { get; set; }
    public AsyncRelayCommand ButtonRecruter{ get; set; }
    public AsyncRelayCommand ButtonModifier { get; set; }

    [ObservableProperty]
    private Equipe _newEquipe;
    public VoirEquipeUIViewModel(EquipeRepository equipeAccess)
    {
        EquipeAccess = equipeAccess;
        ButtonRecruter = new AsyncRelayCommand(NaviguerVersRecrutementPage);
        ButtonModifier = new AsyncRelayCommand(NaviguerVersModificationDetailJoueur);
    }
    public async Task NaviguerVersRecrutementPage()
    {
        await Shell.Current.GoToAsync("RecrutementPage");
    }
    public async Task NaviguerVersModificationDetailJoueur()
    {
        await Shell.Current.GoToAsync("ModificationDetailJoueur");
    }
    private async Task ChargerEquipe(string nomEquipe)
    {
        var equipeTrouvee = await EquipeAccess.RetrouverEquipe(nomEquipe);

        if (equipeTrouvee != null)
        {
            NewEquipe = equipeTrouvee;
        }
        else
        {
            System.Diagnostics.Debug.WriteLine($"Aucune équipe trouvée avec le nom {nomEquipe}");
            ChargerEquipe(nomEquipe);
        }
    }
}
