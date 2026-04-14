using SportManager.ViewModels;

namespace SportManager.Views;

public partial class RecrutementPage : ContentPage
{
	public RecrutementPage()
	{
		InitializeComponent();
		BindingContext = new RecrutementPageViewModel();
	}

	private void OnValiderClicked(object sender, EventArgs e)
	{
		// TODO: logique de recrutement
	}
}