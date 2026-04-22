using SportManager.ViewModels;

namespace SportManager.Views;

public partial class Historique : ContentPage
{
    public Historique()
    {
        InitializeComponent();
        BindingContext = new HistoriqueViewModel();
    }
}