using SportManager.Views;
namespace SportManager;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainScreen), typeof(MainScreen));
		Routing.RegisterRoute(nameof(GestionEquipeUI), typeof(GestionEquipeUI));
		Routing.RegisterRoute(nameof(ModificationDetailJoueurView), typeof(ModificationDetailJoueurView));
		Routing.RegisterRoute(nameof(NouvelleEquipeConnexionMenu), typeof(NouvelleEquipeConnexionMenu));
		Routing.RegisterRoute(nameof(RecrutementPage), typeof(RecrutementPage));
		Routing.RegisterRoute(nameof(VoirEquipeUI), typeof(VoirEquipeUI));
        Routing.RegisterRoute(nameof(GestionMatch), typeof(GestionMatch));
        Routing.RegisterRoute(nameof(Historique), typeof(Historique));
    }
}