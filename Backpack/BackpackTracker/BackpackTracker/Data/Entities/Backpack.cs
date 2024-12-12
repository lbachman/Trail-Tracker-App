using System;
using System.Collections.Generic;

namespace BackpackTracker.Data.Entities;

public partial class Backpack
{
    public int BackpackId { get; set; }

    public string Name { get; set; } = null!;

    public double? Weight { get; set; }

    public int CapacityLiters { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BackpackConfiguration> BackpackConfigurations { get; set; } = new List<BackpackConfiguration>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<SubBag> SubBags { get; set; } = new List<SubBag>();
}
