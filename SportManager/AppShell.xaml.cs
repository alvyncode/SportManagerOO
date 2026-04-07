namespace SportManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Views.RecrutementPage), typeof(Views.RecrutementPage));
		Routing.RegisterRoute(nameof(Views.ModificationDetailJoueurView), typeof(Views.ModificationDetailJoueurView));
	}
}
