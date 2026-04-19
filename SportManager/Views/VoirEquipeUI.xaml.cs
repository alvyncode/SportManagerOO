using SportManager.Data.Repositories;
using SportManager.ViewModels;

namespace SportManager.Views;

public partial class VoirEquipeUI : ContentPage
{
	public VoirEquipeUI(EquipeRepository equipeRepository)
	{
		InitializeComponent();
		BindingContext = new VoirEquipeUIViewModel(equipeRepository);
	}
}