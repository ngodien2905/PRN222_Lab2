using Microsoft.AspNetCore.SignalR;

namespace GameHubSearch.Pages.Shared.GameHub
{
    public class GameHub : Hub
    {
        // Called when a game is deleted
        public async Task BroadcastGameDeleted(int id)
        {
            await Clients.All.SendAsync("GameDeleted", id);
        }

        // Called when a game is updated
        public async Task BroadcastGameUpdated(object game)
        {
            await Clients.All.SendAsync("GameUpdated", game);
        }
    }
}
