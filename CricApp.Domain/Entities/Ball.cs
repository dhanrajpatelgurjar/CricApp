namespace CricApp.Domain.Entities;

public class Ball
{
    public int Id { get; set; }
    public int InningsId { get; set; }
    public Innings? Innings { get; set; }
    public int BallNumber { get; set; }
    public int Runs { get; set; }
    public bool IsWicket { get; set; }
    public string? Commentary { get; set; }
}
