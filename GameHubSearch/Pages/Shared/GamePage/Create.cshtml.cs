using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Data;
using DAL.Models;
using BLL.Interfaces;

namespace GameHubSearch.Pages.Shared.GamePage
{
    public class CreateModel : PageModel
    {
        private readonly IGameService _gameService;
        private readonly IGameCategoryService _categoryService;
        private readonly IDeveloperService _developerService;
        private readonly IValidationService _validationService;

        public CreateModel(
            IGameService gameService,
            IGameCategoryService categoryService,
            IDeveloperService developerService,
            IValidationService validationService)
        {
            _gameService = gameService;
            _categoryService = categoryService;
            _developerService = developerService;
            _validationService = validationService;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public void OnGet()
        {
            PopulateLookups();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                PopulateLookups();
                return Page();
            }

            try
            {
                _validationService.ValidateDate(Game);   // enforce: not in the future
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Game.ReleaseDate", ex.Message);
                PopulateLookups();
                return Page();
            }

            _gameService.AddGame(Game);
            return RedirectToPage("./Index");
        }

        private void PopulateLookups()
        {
            var categories = _categoryService.GetAllCategories();
            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", Game?.CategoryId);

            var developers = _developerService.GetAllDevelopers();
            ViewData["DeveloperId"] = new SelectList(developers, "DeveloperId", "DeveloperName", Game?.DeveloperId);
        }
    }
}