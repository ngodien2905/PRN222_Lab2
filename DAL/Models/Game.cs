using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? DeveloperId { get; set; }

    public int? CategoryId { get; set; }

    public virtual GameCategory? Category { get; set; }

    public virtual Developer? Developer { get; set; }

    public virtual ICollection<PlayerGame> PlayerGames { get; set; } = new List<PlayerGame>();
}
