using Microcharts;
using SkiaSharp;
using SportManager.ViewModels;
namespace SportManager.Views;

public partial class ModificationDetailJoueurView : ContentPage
{
	public ModificationDetailJoueurView()
	{
        BindingContext = new ModificationDetailJoueurViewModel();
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var entries = new[]
        {
            new ChartEntry(80) { Label = "Vitesse",   Color = SKColor.Parse("#f28c57") },
            new ChartEntry(60) { Label = "Endurance", Color = SKColor.Parse("#f28c57") },
            new ChartEntry(90) { Label = "Technique", Color = SKColor.Parse("#f28c57") },
            new ChartEntry(25) { Label = "Force",     Color = SKColor.Parse("#f28c57") },
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