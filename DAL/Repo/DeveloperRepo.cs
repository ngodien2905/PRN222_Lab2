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
    public class DeveloperRepo : IDeveloperRepo
    {
        private readonly GameHubContext _context;
        public DeveloperRepo(GameHubContext context)
        {
            _context = context;
        }

        public List<Developer> GetAllDevelopers()
        {
            return _context.Developers
        .Select(d => new Developer { DeveloperId = d.DeveloperId, DeveloperName = d.DeveloperName })
        .ToList();
        }

        public Developer GetDeveloperById(int developerId)
        {
            return _context.Developers
                .FirstOrDefault(d => d.DeveloperId == developerId) ?? new Developer();
        }
    }
}
