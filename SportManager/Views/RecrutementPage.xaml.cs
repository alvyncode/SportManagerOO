using SportManager.ViewModels;

namespace SportManager.Views;

public partial class RecrutementPage : ContentPage
{
	public RecrutementPage()
	{
		BindingContext = new RecrutementPageViewModel();
		InitializeComponent();
	}

	private void OnValiderClicked(object sender, EventArgs e)
	{
		// TODO: logique de recrutement
	}
}