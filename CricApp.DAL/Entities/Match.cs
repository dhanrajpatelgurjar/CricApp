namespace CricApp.DAL.Entities;

public class Match
{
    public int Id { get; set; }
    public string MatchName { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string Venue { get; set; } = string.Empty;
    public int TeamOneId { get; set; }
    public Team? TeamOne { get; set; }
    public int TeamTwoId { get; set; }
    public Team? TeamTwo { get; set; }
    public string Status { get; set; } = "Scheduled";
    public List<Innings> Innings { get; set; } = new();
}
