﻿using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Type
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public string? Des { get; set; }

    public virtual ICollection<Room> Rooms { get; } = new List<Room>();
}
