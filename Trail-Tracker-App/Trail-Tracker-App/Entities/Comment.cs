using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? UserId { get; set; }

    public int? TrailId { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual Trail? Trail { get; set; }
}
