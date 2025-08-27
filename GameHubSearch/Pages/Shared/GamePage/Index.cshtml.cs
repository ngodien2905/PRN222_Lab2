using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameHubSearch.Pages.Shared.GamePage
{
    public class IndexModel : PageModel
    {
        private readonly IGameService _gameService;

        public IndexModel(IGameService gameService)
        {
            _gameService = gameService;
        }

        public class GameRow
        {
            public int GameId { get; set; }
            public string Title { get; set; } = "";
            public decimal Price { get; set; }
            public string? ReleaseDate { get; set; }
            public string? Category { get; set; }
            public string? Developer { get; set; }
        }

        public IList<GameRow> Games { get; set; } = new List<GameRow>();

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
                return RedirectToPage("/Shared/Login");

            // Guard: must be Admin
            var role = HttpContext.Session.GetString("Role");
            if (role != DAL.Models.User.Role.Admin.ToString())
                return RedirectToPage("/Shared/Login");

            var allGames = _gameService.GetAllGames();

            Games = allGames.Select(g => new GameRow
            {
                GameId = g.GameId,
                Title = g.Title,
                Price = g.Price,
                ReleaseDate = g.ReleaseDate.HasValue ? g.ReleaseDate.Value.ToString("yyyy-MM-dd") : "",
                Category = g.Category?.CategoryName,
                Developer = g.Developer?.DeveloperName
            }).ToList();

            return Page();
        }
    }
}
