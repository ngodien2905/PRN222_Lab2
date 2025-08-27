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
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepo _developerRepo;

        public Developer GetDeveloperById(int developerId)
        {
            return _developerRepo.GetDeveloperById(developerId);
        }

        public List<Developer> GetAllDevelopers()
        {
            return _developerRepo.GetAllDevelopers();
        }
    }
}
