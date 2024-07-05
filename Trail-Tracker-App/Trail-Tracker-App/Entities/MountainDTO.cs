namespace Trail_Tracker_App.Entities
{
    public class MountainDTO
    {
        public string RangeName { get; set; }
        public string Name { get; set; } = null!;

        public string? Zip { get; set; }

        public int? Height { get; set; }

        public string? Description { get; set; }
    }
}
