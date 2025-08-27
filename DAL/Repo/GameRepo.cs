using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
   public class GameRepo : IGameRepo
    {
        private readonly GameHubContext _context;
        public GameRepo(GameHubContext context)
        {
            _context = context;
        }
        public Game GetGameById(int gameId)
        {
            return _context.Games
       .Include(g => g.Category)
       .Include(g => g.Developer)
       .FirstOrDefault(g => g.GameId == gameId);
        }

        public List<Game> GetAllGames()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Developer)
                .ToList();
        }

        public void AddGame(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            _context.Games.Update(game);
            _context.SaveChanges();
        }

        public void DeleteGame(int gameId)
        {
            var game = _context.Games.Find(gameId);
            if (game != null)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
            }
        }
    }
}
