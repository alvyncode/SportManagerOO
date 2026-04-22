namespace SportManager.Models;

public class MatchHistorique
{
    public int Id { get; set; }
    public string Equipe1 { get; set; } = string.Empty;
    public string Equipe2 { get; set; } = string.Empty;
    public string Score { get; set; } = string.Empty;
}