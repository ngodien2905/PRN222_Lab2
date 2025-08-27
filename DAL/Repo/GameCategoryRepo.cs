using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class GameCategoryRepo : IGameCategoryRepo
    {
        private readonly GameHubContext _context;

        public GameCategoryRepo(GameHubContext context)
        {
            _context = context;
        }

        public List<GameCategory> GetAllCategories()
        {
            return _context.GameCategories.ToList();
        }

        public GameCategory GetCategoryById(int categoryId)
        {
            return _context.GameCategories
                .FirstOrDefault(c => c.CategoryId == categoryId);
        }

    }
}
