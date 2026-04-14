using SportManager.ViewModels;

namespace SportManager.Views;

public partial class VoirEquipeUI : ContentPage
{
	public VoirEquipeUI()
	{
		InitializeComponent();
		BindingContext = new VoirEquipeUIViewModel();
	}
}