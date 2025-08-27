using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubSearch.Pages.Shared.GamePage
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly GameHubContext _ctx;

        public IndexModel(IGameService gameService, GameHubContext ctx)
        {
            _gameService = gameService;
            _ctx = ctx;
        }

        public class GameRow
        {
            public int GameId { get; set; }
            public string Title { get; set; } = "";
            public decimal Price { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Category { get; set; }
            public string? Developer { get; set; }
            public int RegisteredCount { get; set; }
        }

        public IList<GameRow> Games { get; set; } = new List<GameRow>();

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
                return RedirectToPage("/Shared/Login");

            // 2) Guard: must be Admin or Developer
            var role = HttpContext.Session.GetString("Role");
            if (role != DAL.Models.User.Role.Admin.ToString())
                return RedirectToPage("/Shared/Login");

            var allGames = _gameService.GetAllGames();

            // Get counts grouped by GameId
            var counts = _ctx.PlayerGames
                .AsNoTracking()
                .GroupBy(pg => pg.GameId)
                .Select(g => new { g.Key, Cnt = g.Count() })
                .ToDictionary(g => g.Key, g => g.Cnt);

            Games = allGames.Select(g => new GameRow
            {
                GameId = g.GameId,
                Title = g.Title,
                Price = g.Price,
                ReleaseDate = g.ReleaseDate.HasValue ? g.ReleaseDate.Value.ToString("yyyy-MM-dd") : "",
                Category = g.Category?.CategoryName,
                Developer = g.Developer?.DeveloperName,
                RegisteredCount = counts.TryGetValue(g.GameId, out var c) ? c : 0
            }).ToList();

            return Page();
        }
    }
}
