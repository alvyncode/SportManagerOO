using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SportManager.Models;

public partial class Match : ObservableObject
{
    [ObservableProperty]
    private Equipe _premiereEquipe;

    [ObservableProperty]
    private Equipe _deuxiemeEquipe;

    [ObservableProperty]
    private int _scorePremiereEquipe;

    [ObservableProperty]
    private int _scoreDeuxiemeEquipe;
}
