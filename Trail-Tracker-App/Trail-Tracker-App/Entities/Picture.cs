using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Picture
{
    public int PictureId { get; set; }

    public int? TrailId { get; set; }

    public string FilePath { get; set; } = null!;

    public string? UploadedBy { get; set; }

    public DateTime? UploadDate { get; set; }

    public virtual Trail? Trail { get; set; }
}
