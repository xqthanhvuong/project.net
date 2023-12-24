using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Room
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual Roomtype? Type { get; set; }
}
