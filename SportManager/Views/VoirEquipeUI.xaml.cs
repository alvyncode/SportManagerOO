using SportManager.ViewModels;

namespace SportManager.Views;

public partial class VoirEquipeUI : ContentPage
{
	public VoirEquipeUI()
	{
		BindingContext = new VoirEquipeUIViewModel();
		InitializeComponent();
	}
}