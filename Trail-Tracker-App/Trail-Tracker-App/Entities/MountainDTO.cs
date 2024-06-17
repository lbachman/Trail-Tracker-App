namespace Trail_Tracker_App.Entities
{
    public class MountainDTO
    {
        public string Range { get; set; }
        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public int? Height { get; set; }

        public string? Description { get; set; }
    }
}
