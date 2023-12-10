using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
