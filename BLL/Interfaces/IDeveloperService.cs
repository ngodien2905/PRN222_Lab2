using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDeveloperService
    {
        Developer GetDeveloperById(int developerId);
        List<Developer> GetAllDevelopers();
    }
}
