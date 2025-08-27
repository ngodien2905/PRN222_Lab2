using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHubSearch.Pages.Shared.GamePage
{
    public class EditModel : PageModel
    {

        private readonly IGameService _gameService;
        private readonly IHubContext<GameHubSearch.Pages.Shared.GameHub.GameHub> _hub;
        private readonly GameHubContext _ctx;
        private readonly IValidationService _validationService;

        public EditModel(IGameService gameService, IHubContext<GameHubSearch.Pages.Shared.GameHub.GameHub> hub, GameHubContext ctx, IValidationService validationService)
        {
            _gameService = gameService;
            _hub = hub;
            _ctx = ctx;
            _validationService = validationService;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        private async Task PopulateLookupsAsync(int? selectedDevId = null, int? selectedCatId = null)
        {
            ViewData["DeveloperId"] = new SelectList(
                await _ctx.Developers.AsNoTracking().ToListAsync(),
                "DeveloperId", "DeveloperName", selectedDevId
            );

            ViewData["CategoryId"] = new SelectList(
                await _ctx.GameCategories.AsNoTracking().ToListAsync(),
                "CategoryId", "CategoryName", selectedCatId
            );
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = _gameService.GetGameById(id);
            if (Game == null) return NotFound();

            await PopulateLookupsAsync(Game.DeveloperId, Game.CategoryId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateLookupsAsync(Game?.DeveloperId, Game?.CategoryId);
                return Page();
            }

            try
            {
                _validationService.ValidateDate(Game);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Game.ReleaseDate", ex.Message);
                await PopulateLookupsAsync(Game.DeveloperId, Game.CategoryId);
                return Page();
            }
            _gameService.UpdateGame(Game);


            var updated = _gameService.GetGameById(Game.GameId);
            if (updated != null)
            {
                var payload = new
                {
                    id = updated.GameId,
                    title = updated.Title,
                    price = updated.Price.ToString("0.00"),
                    releaseDate = updated.ReleaseDate.HasValue ? updated.ReleaseDate.Value.ToString("yyyy-MM-dd") : null,
                    category = updated.Category?.CategoryName,
                    developer = updated.Developer?.DeveloperName
                };

                await _hub.Clients.All.SendAsync("GameUpdated", payload);
            }

            return RedirectToPage("./Index");
        }
    }
}
