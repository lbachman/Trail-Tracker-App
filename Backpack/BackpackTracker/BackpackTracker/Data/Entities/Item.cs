using System;
using System.Collections.Generic;

namespace BackpackTracker.Data.Entities;

public partial class Item
{
    public int ItemId { get; set; }

    public int? SubbagId { get; set; }

    public int BackpackId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Weight { get; set; }

    public string? PictureUrl { get; set; }

    public int? Quantity { get; set; }

    public bool? InBag { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Backpack Backpack { get; set; } = null!;

    public virtual ICollection<ConfigurationItem> ConfigurationItems { get; set; } = new List<ConfigurationItem>();

    public virtual SubBag? Subbag { get; set; }
}
