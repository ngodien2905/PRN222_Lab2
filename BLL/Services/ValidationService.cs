using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidateDate(Game game)
        {
            if (game.ReleaseDate is null)
                throw new InvalidOperationException("Release date is required.");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (game.ReleaseDate.Value >= today)
                throw new InvalidOperationException("Release date must be before today.");
        }
    }
}
