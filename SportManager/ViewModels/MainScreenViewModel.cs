using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SportManager.ViewModels;

public class MainScreenViewModel:ObservableObject 
{
    public AsyncRelayCommand ButtonGestionEquipe { get; set; }
    public AsyncRelayCommand ButtonHistorique { get; set; }
    public AsyncRelayCommand ButtonJouerMatch { get; set; }
    public MainScreenViewModel()
    {
        ButtonGestionEquipe = new AsyncRelayCommand(NaviguerVersGestionEquipeUI);
        ButtonHistorique = new AsyncRelayCommand(NaviguerVersHistorique);
        ButtonJouerMatch = new AsyncRelayCommand(NaviguerVersGestionMatch);
    }
    public async Task NaviguerVersGestionEquipeUI()
    {
        await Shell.Current.GoToAsync("GestionEquipeUI");
    }
    public async Task NaviguerVersHistorique()
    {
        await Shell.Current.GoToAsync("Historique");
    }
    public async Task NaviguerVersGestionMatch()
    {
        await Shell.Current.GoToAsync("GestionMatch");
    }
}
