using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Views;

namespace SportManager.ViewModels;

public class NouvelleEquipeConnexionMenuViewModel : ObservableObject
{
    public AsyncRelayCommand ButtonValiderNewTeam { get; set;}
    public NouvelleEquipeConnexionMenuViewModel()
    {
        ButtonValiderNewTeam = new AsyncRelayCommand(NaviguerVersVoirEquipe);
    }
    public async Task NaviguerVersVoirEquipe()
    {
        await Shell.Current.GoToAsync("VoirEquipeUI");
    }

}

