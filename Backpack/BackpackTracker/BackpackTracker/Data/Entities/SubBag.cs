using System;
using System.Collections.Generic;

namespace BackpackTracker.Data.Entities;

public partial class SubBag
{
    public int SubbagId { get; set; }

    public int BackpackId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double? Weight { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Backpack Backpack { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
