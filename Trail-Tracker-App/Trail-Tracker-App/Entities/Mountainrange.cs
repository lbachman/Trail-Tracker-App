using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Mountainrange
{
    public int RangeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Mountain> Mountains { get; set; } = new List<Mountain>();
}
