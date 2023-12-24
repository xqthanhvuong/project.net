using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Roomimg
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public string? Link { get; set; }

    public virtual Roomtype? Type { get; set; }
}
