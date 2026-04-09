using SportManager.ViewModels;

namespace SportManager.Views;

public partial class NouvelleEquipeConnexionMenu : ContentPage
{
	public NouvelleEquipeConnexionMenu()
	{
		BindingContext = new NouvelleEquipeConnexionMenuViewModel();
		InitializeComponent();
	}
}