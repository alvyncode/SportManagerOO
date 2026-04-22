using SportManager.ViewModels;

namespace SportManager.Views;

public partial class GestionMatch : ContentPage
{
    public GestionMatch()
    {
        InitializeComponent();
        BindingContext = new GestionMatchViewModel();
    }
}