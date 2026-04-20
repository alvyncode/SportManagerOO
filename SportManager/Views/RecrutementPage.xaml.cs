using SportManager.ViewModels;
using SportManager.Data.Repositories;

namespace SportManager.Views;

public partial class RecrutementPage : ContentPage
{
	public RecrutementPage(EquipeRepository equipeRepository)
	{
		InitializeComponent();
		BindingContext = new RecrutementPageViewModel(equipeRepository);
	}

	private void OnValiderClicked(object sender, EventArgs e)
	{
		// TODO: logique de recrutement
	}
}