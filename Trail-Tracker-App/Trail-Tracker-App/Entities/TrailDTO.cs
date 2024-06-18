namespace Trail_Tracker_App.Entities
{
    public class TrailDTO
    {
        public string MountainName { get; set; }

        public string Name { get; set; } = null!;

        public string? Difficulty { get; set; }

        public decimal? Length { get; set; }

        public string? Description { get; set; }
    }
}
