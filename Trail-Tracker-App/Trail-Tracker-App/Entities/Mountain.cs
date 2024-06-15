using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Mountain
{
    public int MountainId { get; set; }

    public int? RangeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public int? Height { get; set; }

    public string? Description { get; set; }

    public virtual Mountainrange? Range { get; set; }

    public virtual ICollection<Trail> Trails { get; set; } = new List<Trail>();
}
