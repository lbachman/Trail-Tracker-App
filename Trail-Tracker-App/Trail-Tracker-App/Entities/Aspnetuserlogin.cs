using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Aspnetuserlogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}
