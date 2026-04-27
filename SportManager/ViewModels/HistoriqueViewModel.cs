using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SportManager.Data.Repositories;
using SportManager.Models;

namespace SportManager.ViewModels;

public partial class HistoriqueViewModel : ObservableObject
{
    private readonly MatchRepository _repository;

    public ObservableCollection<MatchHistorique> Matchs { get; } = new();

    public HistoriqueViewModel()
    {
        _repository = new MatchRepository();
        ChargerHistorique();
    }

    private void ChargerHistorique()
    {
        Matchs.Clear();

        foreach (var match in _repository.GetHistorique())
        {
            Matchs.Add(match);
        }
    }
}