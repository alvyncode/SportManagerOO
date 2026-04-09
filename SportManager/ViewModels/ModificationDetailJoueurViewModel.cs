using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SportManager.ViewModels;

public class ModificationDetailJoueurViewModel: ObservableObject
{
    public AsyncRelayCommand ButtonValiderModification { get; set;}
    public ModificationDetailJoueurViewModel()
    {
        ButtonValiderModification = new AsyncRelayCommand(NaviguerVersVoirEquipe);
    }
    public async Task NaviguerVersVoirEquipe()
    {
        await Shell.Current.GoToAsync("VoirEquipeUI");
    }

}
