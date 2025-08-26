using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public int? UserId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();

    public virtual User? User { get; set; }
}
