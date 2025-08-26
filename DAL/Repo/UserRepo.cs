using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly GameHubContext _context;
        private readonly IPasswordHasher<User> _hasher;
        public UserRepo(GameHubContext gameHubContext, IPasswordHasher<User> hasher )
        {
            _context = gameHubContext;
            _hasher = hasher;   
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null) return null;

            var vr = _hasher.VerifyHashedPassword(null, user.PasswordHash, password);
            return vr == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success ? user : null;
        }
    }
}
