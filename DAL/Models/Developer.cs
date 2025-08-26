using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Developer
{
    public int DeveloperId { get; set; }

    public string DeveloperName { get; set; } = null!;

    public string? Website { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
