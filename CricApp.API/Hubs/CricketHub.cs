using Microsoft.AspNetCore.SignalR;

namespace CricApp.API.Hubs;

public class CricketHub : Hub
{
    public async Task SendMatchUpdate(string matchId, string message)
    {
        await Clients.All.SendAsync("ReceiveMatchUpdate", matchId, message);
    }
}
