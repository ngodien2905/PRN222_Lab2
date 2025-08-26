using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;

namespace GameHubSearch.Pages.Shared.GamePage
{
    public class IndexModel : PageModel
    {
        private readonly DAL.Data.GameHubContext _context;

        public IndexModel(DAL.Data.GameHubContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Game = await _context.Games
                .Include(g => g.Category)
                .Include(g => g.Developer).ToListAsync();
        }
    }
}
