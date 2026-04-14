using SportManager.ViewModels;

namespace SportManager.Views;

public partial class NouvelleEquipeConnexionMenu : ContentPage
{
	public NouvelleEquipeConnexionMenu()
	{
		InitializeComponent();
		BindingContext = new NouvelleEquipeConnexionMenuViewModel();
	}
}