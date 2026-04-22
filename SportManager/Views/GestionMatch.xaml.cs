using SportManager.ViewModels;

<<<<<<< HEAD
using SportManager.ViewModels;

=======
>>>>>>> 761214401b5c90605a0c752d21f4dc1600332a7e
namespace SportManager.Views;


public partial class GestionMatch : ContentPage
{
    public GestionMatch()
    {
        InitializeComponent();
        BindingContext = new GestionMatchViewModel();
    }
}