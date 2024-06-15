using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Trail
{
    public int TrailId { get; set; }

    public int? MountainId { get; set; }

    public string Name { get; set; } = null!;

    public string? Difficulty { get; set; }

    public decimal? Length { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Mountain? Mountain { get; set; }

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Userfavorite> Userfavorites { get; set; } = new List<Userfavorite>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
