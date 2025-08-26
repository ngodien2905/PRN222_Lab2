using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class VwGameRegistrationCount
{
    public int GameId { get; set; }

    public string Title { get; set; } = null!;

    public int? RegistrationCount { get; set; }
}
