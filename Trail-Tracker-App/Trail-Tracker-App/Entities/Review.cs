using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public string? UserId { get; set; }

    public int? TrailId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Trail? Trail { get; set; }
}
