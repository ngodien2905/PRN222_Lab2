using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepo
    {
        public Task<User?> AuthenticateAsync(string email, string password);
        //public Task<User> RegisterAsync(string email, string password, User.Role role);
        //public Task<User?> GetByIdAsync(int userId);
        //public Task<List<User>> GetAllAsync();
    }
}
