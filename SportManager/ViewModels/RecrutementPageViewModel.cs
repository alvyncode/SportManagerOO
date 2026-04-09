using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SportManager.ViewModels;

public class RecrutementPageViewModel: ObservableObject
{
    public AsyncRelayCommand ButtonValiderRecrutement { get; set;}
    public RecrutementPageViewModel()
    {
        ButtonValiderRecrutement = new AsyncRelayCommand(NaviguerVersVoirEquipe);
    }
    public async Task NaviguerVersVoirEquipe()
    {
        await Shell.Current.GoToAsync("VoirEquipeUI");
    }

}
