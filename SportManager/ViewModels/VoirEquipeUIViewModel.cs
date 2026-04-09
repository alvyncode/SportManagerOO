using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SportManager.ViewModels;

public class VoirEquipeUIViewModel : ObservableObject
{
    public AsyncRelayCommand ButtonRecruter{ get; set; }
    public AsyncRelayCommand ButtonModifier { get; set; }
    public VoirEquipeUIViewModel()
    {
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
}
