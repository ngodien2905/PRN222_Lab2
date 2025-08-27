using BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace GameHubSearch.Pages.Shared.GameHub
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;
        public GameHub(IGameService gameService) => _gameService = gameService;

        public async Task SearchGames(string term)
        {
            var games = _gameService.SearchGames(term);
            var rows = games.Select(g => new
            {
                id = g.GameId,
                title = g.Title,
                price = g.Price.ToString("0.00"),
                releaseDate = g.ReleaseDate.HasValue ? g.ReleaseDate.Value.ToString("yyyy-MM-dd") : "",
                category = g.Category?.CategoryName,
                developer = g.Developer?.DeveloperName
            }).ToList();

            await Clients.Caller.SendAsync("SearchResults", rows);
        }

        public Task BroadcastGameDeleted(int id) =>
            Clients.All.SendAsync("GameDeleted", id);

        public Task BroadcastGameUpdated(object game) =>
            Clients.All.SendAsync("GameUpdated", game);
    }
}

