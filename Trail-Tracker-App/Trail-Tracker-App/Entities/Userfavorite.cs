using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Userfavorite
{
    public int FavoriteId { get; set; }

    public string? UserId { get; set; }

    public int? TrailId { get; set; }

    public virtual Trail? Trail { get; set; }

    public virtual Aspnetuser? User { get; set; }
}
