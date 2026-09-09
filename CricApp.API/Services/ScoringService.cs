namespace CricApp.API.Services;

public class ScoringService
{
    public Task<int> GetCurrentRunRateAsync(int runs, int overs)
    {
        if (overs == 0)
        {
            return Task.FromResult(0);
        }

        return Task.FromResult((runs * 6) / overs);
    }
}
