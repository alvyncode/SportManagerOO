using Microcharts;
using SkiaSharp;
using SportManager.ViewModels;
namespace SportManager.Views;

public partial class ModificationDetailJoueurView : ContentPage
{
    private ModificationDetailJoueurViewModel? _viewModel;

	public ModificationDetailJoueurView()
	{
		InitializeComponent();
        _viewModel = new ModificationDetailJoueurViewModel();
        BindingContext = _viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        MettreAJourRadarChart();
    }

    protected override void OnDisappearing()
    {
        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        base.OnDisappearing();
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ModificationDetailJoueurViewModel.Vitesse)
            || e.PropertyName == nameof(ModificationDetailJoueurViewModel.Endurence)
            || e.PropertyName == nameof(ModificationDetailJoueurViewModel.Technique)
            || e.PropertyName == nameof(ModificationDetailJoueurViewModel.Force))
        {
            MettreAJourRadarChart();
        }
    }

    private void MettreAJourRadarChart()
    {
        if (_viewModel == null)
        {
            return;
        }

        var entries = new[]
        {
            new ChartEntry(_viewModel.Vitesse) { Label = "Vitesse",   Color = SKColor.Parse("#f28c57") },
            new ChartEntry(_viewModel.Endurence) { Label = "Endurance", Color = SKColor.Parse("#f28c57") },
            new ChartEntry(_viewModel.Technique) { Label = "Technique", Color = SKColor.Parse("#f28c57") },
            new ChartEntry(_viewModel.Force) { Label = "Force",     Color = SKColor.Parse("#f28c57") },
        };

        radarChart.Chart = new RadarChart
        {
            Entries = entries,
            MinValue = 0,
            MaxValue = 100,
            LineSize = 2,
            PointMode = PointMode.Circle,
            PointSize = 12,
            LabelTextSize = 22,
            LabelColor = SKColor.Parse("#ffffff"),
            BackgroundColor = SKColor.Parse("#3a3a3a"),
            BorderLineSize = 1
        };
    }

	private void OnValiderClicked(object sender, EventArgs e)
	{
		// TODO: logique de validation
	}
}