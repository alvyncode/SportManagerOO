using SportManager.ViewModels;


namespace SportManager.Views;

public partial class GestionEquipeUI : ContentPage
{
	private readonly GestionEquipeUIViewModel _viewModel = new ();
	public GestionEquipeUI()
	{
		InitializeComponent();
		BindingContext = _viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.ChargerToutesLesEquipesAsync();
	}
}