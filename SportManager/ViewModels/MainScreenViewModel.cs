using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SportManager.ViewModels;

public class MainScreenViewModel:ObservableObject 
{
    public AsyncRelayCommand ButtonGestionEquipe { get; set; }
    public MainScreenViewModel()
    {
        ButtonGestionEquipe = new AsyncRelayCommand(NaviguerVersListeEquipe);
    }
    public async Task NaviguerVersListeEquipe()
    {
        await Shell.Current.GoToAsync("GestionEquipeUI");
    }
}
