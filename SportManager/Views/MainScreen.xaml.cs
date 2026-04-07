namespace SportManager.Views;

public partial class MainScreen : ContentPage
{
	public MainScreen()
	{
		InitializeComponent();
		
	}
	
	//mauvais root mais c'est pour voir la page de recrutement
	private async void OnGestionEquipeClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RecrutementPage));
	}
	//mauvais root mais c'est pour voir la page de modification de match
	private async void OnJouerMatchClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(ModificationDetailJoueurView));
	}
	//mauvais root mais tkt
	private async void OnHistoriqueClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RecrutementPage));
	}

}