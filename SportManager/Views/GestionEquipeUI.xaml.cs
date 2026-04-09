using SportManager.ViewModels;


namespace SportManager.Views;

public partial class GestionEquipeUI : ContentPage
{
	public GestionEquipeUI()
	{
		BindingContext = new GestionEquipeUIViewModel();
		InitializeComponent();
	}
}