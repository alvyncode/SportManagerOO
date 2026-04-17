using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SportManager.Data.Repositories;
using System;

namespace SportManager.ViewModels;

public class GestionEquipeUIViewModel:ObservableObject 
{
    public AsyncRelayCommand ButtonNouvelleEquipe { get; set; }
    public AsyncRelayCommand ButtonVoirEquipe { get; set; }
    public GestionEquipeUIViewModel()
    {
        ButtonNouvelleEquipe = new AsyncRelayCommand(NaviguerVersListeEquipe);
        ButtonVoirEquipe = new AsyncRelayCommand(NaviguerVersVoirEquipe);
    }
    public async Task NaviguerVersListeEquipe()
    {
        await Shell.Current.GoToAsync("NouvelleEquipeConnexionMenu");
    }
    public async Task NaviguerVersVoirEquipe()
    {
        await Shell.Current.GoToAsync("VoirEquipeUI");
    }
}
