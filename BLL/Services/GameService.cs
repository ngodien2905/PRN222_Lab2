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
    public class GameService : IGameService
    {
        private readonly IGameRepo _gameRepo;
        public GameService(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public Game GetGameById(int gameId)
        {
            return _gameRepo.GetGameById(gameId);
        }
        public void AddGame(Game game)
        {
            _gameRepo.AddGame(game);
        }
        public void UpdateGame(Game game)
        {
            _gameRepo.UpdateGame(game);
        }
        public void DeleteGame(int gameId)
        {
            _gameRepo.DeleteGame(gameId);
        }
        public List<Game> GetAllGames()
        {
            return _gameRepo.GetAllGames();
        }

        public List<Game> SearchGames(string term)
        {
            return _gameRepo.SearchGames(term);
        }
    }
}
