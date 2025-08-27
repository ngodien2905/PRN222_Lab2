using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace GameHubSearch.Pages.Shared.GamePage
{
    public class DeleteModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly IHubContext<GameHubSearch.Pages.Shared.GameHub.GameHub> _hubContext;

        public DeleteModel(IGameService gameService, IHubContext<GameHubSearch.Pages.Shared.GameHub.GameHub> hubContext)
        {
            _gameService = gameService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public IActionResult OnGet(int id)
        {
            Game = _gameService.GetGameById(id);
            if (Game == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var game = _gameService.GetGameById(id);
            if (game != null)
            {
                _gameService.DeleteGame(id);
                // Broadcast the deletion to all SignalR clients
                await _hubContext.Clients.All.SendAsync("GameDeleted", id);
            }
            return RedirectToPage("./Index");
        }
    }
}
