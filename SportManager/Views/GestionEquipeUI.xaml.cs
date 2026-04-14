using SportManager.ViewModels;


namespace SportManager.Views;

public partial class GestionEquipeUI : ContentPage
{
	public GestionEquipeUI()
	{
		InitializeComponent();
		BindingContext = new GestionEquipeUIViewModel();
	}
}