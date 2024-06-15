using System;
using System.Collections.Generic;

namespace Trail_Tracker_App.Entities;

public partial class Userprofile
{
    public int UserProfileId { get; set; }

    public string? UserId { get; set; }

    public string? Bio { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual Aspnetuser? User { get; set; }
}
