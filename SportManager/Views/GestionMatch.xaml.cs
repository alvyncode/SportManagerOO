using SportManager.ViewModels;

namespace SportManager.Views;


public partial class GestionMatch : ContentPage
{
    Random random = new Random();

    public GestionMatch()
    {
        InitializeComponent();
        BindingContext = new GestionMatchViewModel();
    }

    private async void OnJouerClicked(object sender, EventArgs e)
    {
        // Exemple simple (tu peux remplacer par tes vrais équipes)
        int score1 = random.Next(0, 6);
        int score2 = random.Next(0, 6);

        ScoreLabel.Text = $"{score1} - {score2}";
    }
}