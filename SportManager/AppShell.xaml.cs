namespace SportManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Views.MainScreen), typeof(Views.MainScreen));
	}
}
