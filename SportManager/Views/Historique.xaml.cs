using System.Collections.ObjectModel;
namespace SportManager.Views;

public partial class Historique : ContentPage
{
    public ObservableCollection<MatchHistorique> Matchs { get; set; }

    public Historique()
    {
        InitializeComponent();

        Matchs = new ObservableCollection<MatchHistorique>
        {
            new MatchHistorique { Id = 1, Equipe1 = "Équipe 1", Equipe2 = "Équipe 2", Score = "2 - 1" },
            new MatchHistorique { Id = 2, Equipe1 = "PSG", Equipe2 = "OM", Score = "3 - 0" },
            new MatchHistorique { Id = 3, Equipe1 = "Lyon", Equipe2 = "Lille", Score = "1 - 1" },
            new MatchHistorique { Id = 4, Equipe1 = "Barça", Equipe2 = "Real", Score = "2 - 2" }
        };

        HistoriqueCollection.ItemsSource = Matchs;
    }
}

public class MatchHistorique
{
    public int Id { get; set; }
    public string Equipe1 { get; set; }
    public string Equipe2 { get; set; }
    public string Score { get; set; }
}