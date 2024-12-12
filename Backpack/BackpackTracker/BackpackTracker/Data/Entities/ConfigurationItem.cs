using System;
using System.Collections.Generic;

namespace BackpackTracker.Data.Entities;

public partial class ConfigurationItem
{
    public int ConfigItemId { get; set; }

    public int ConfigId { get; set; }

    public int ItemId { get; set; }

    public bool? InBag { get; set; }

    public int? Quantity { get; set; }

    public virtual BackpackConfiguration Config { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
