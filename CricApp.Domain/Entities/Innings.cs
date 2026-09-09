namespace CricApp.Domain.Entities;

public class Innings
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public Match? Match { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public int InningNumber { get; set; }
    public int Runs { get; set; }
    public int Wickets { get; set; }
    public int Overs { get; set; }
    public List<Ball> Balls { get; set; } = new();
}
