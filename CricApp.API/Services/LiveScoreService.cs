namespace CricApp.API.Services;

public class LiveScoreService
{
    public Task<string> GetLiveScoreAsync(string matchId)
    {
        return Task.FromResult($"Live score for {matchId}: 120/3 in 12.2 overs");
    }
}
