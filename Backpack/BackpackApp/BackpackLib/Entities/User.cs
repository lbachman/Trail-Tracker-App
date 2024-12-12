using System;
using System.Collections.Generic;

namespace BackpackLib.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public double? Height { get; set; }

    public int? Age { get; set; }

    public string? ExperienceLevel { get; set; }

    public DateTime? CreatedAt { get; set; }
}
