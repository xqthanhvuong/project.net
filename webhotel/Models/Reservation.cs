using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public string? CustomerId { get; set; }

    public DateTime? CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public int? RoomId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Room? Room { get; set; }
}
