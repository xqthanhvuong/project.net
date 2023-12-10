using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Roomimg
{
    public int Id { get; set; }

    public int? RoomId { get; set; }

    public string? Link { get; set; }

    public virtual Room? Room { get; set; }
}
