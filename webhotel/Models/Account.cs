using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Forgotpass> Forgotpasses { get; } = new List<Forgotpass>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
