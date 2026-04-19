using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
namespace SportManager.ViewModels;

public partial class NouvelleEquipeConnexionMenuViewModel : ObservableObject
{
    public AsyncRelayCommand ButtonValiderNewTeam { get; set;}
    public EquipeRepository EquipeAccess { get; set; } = new();
    
    [ObservableProperty]
    private string _nomDeLequipe;
    public NouvelleEquipeConnexionMenuViewModel()
    {
        ButtonValiderNewTeam = new AsyncRelayCommand(NaviguerVersVoirEquipe);
    }
    public async Task NaviguerVersVoirEquipe()
    {
        EquipeAccess.EnregistrerEtRecupererEquipe(_nomDeLequipe);
        await Shell.Current.GoToAsync($"VoirEquipeUI?Nom={_nomDeLequipe}");
    }
}

