using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class PlayerGame
{
    public int PlayerId { get; set; }

    public int GameId { get; set; }

    public DateTime RegisteredAt { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
