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
    public class DetailsModel : PageModel
    {
        private readonly IGameService _gameService;

        public DetailsModel(IGameService gameService)
        {
            _gameService = gameService;
        }

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
    }
}
