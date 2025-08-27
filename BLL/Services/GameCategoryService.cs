using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameCategoryService : IGameCategoryService
    {
        private readonly IGameCategoryRepo _gameCategoryRepo;

        public GameCategoryService(IGameCategoryRepo gameCategoryRepo)
        {
            _gameCategoryRepo = gameCategoryRepo;
        }
        public List<GameCategory> GetAllCategories()
        {
            return _gameCategoryRepo.GetAllCategories();
        }

        public GameCategory GetCategoryById(int categoryId)
        {
            return _gameCategoryRepo.GetCategoryById(categoryId);
        }
    }
}
