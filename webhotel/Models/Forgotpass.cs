using System;
using System.Collections.Generic;

namespace webhotel.Models;

public partial class Forgotpass
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string ResetPass { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Account? UsernameNavigation { get; set; }
}
