using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Video
{
    public int VideoId { get; set; }

    public int? TrailId { get; set; }

    public string FilePath { get; set; } = null!;

    public string? UploadedBy { get; set; }

    public DateTime? UploadDate { get; set; }

    public virtual Trail? Trail { get; set; }
}
