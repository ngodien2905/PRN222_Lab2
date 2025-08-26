using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public Role RoleId { get; set; }
    public enum Role : byte
    {
        Admin = 1,
        Developer = 2,
        Player = 3,
    }

    public virtual Player? Player { get; set; }
}
