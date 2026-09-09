namespace CricApp.API.Services;

public class MatchService
{
    public Task<string> GetMatchSummaryAsync(string matchId)
    {
        return Task.FromResult($"Summary for match {matchId}");
    }
}
