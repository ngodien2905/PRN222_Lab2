using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGameService
    {
        Game GetGameById(int gameId);

        void AddGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(int gameId);
        List<Game> GetAllGames();
    }
}
