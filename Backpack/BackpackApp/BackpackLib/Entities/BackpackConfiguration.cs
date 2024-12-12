using System;
using System.Collections.Generic;

namespace BackpackLib.Entities;

public partial class BackpackConfiguration
{
    public int ConfigId { get; set; }

    public int BackpackId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Backpack Backpack { get; set; } = null!;

    public virtual ICollection<ConfigurationItem> ConfigurationItems { get; set; } = new List<ConfigurationItem>();
}
