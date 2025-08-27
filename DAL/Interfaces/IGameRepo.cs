using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGameRepo
    {
        Game GetGameById(int gameId);

        void AddGame(Game game);

        void UpdateGame(Game game);

        void DeleteGame(int gameId);

        List<Game> GetAllGames();

        List<Game> SearchGames(string term);
    }
}
