namespace Trail_Tracker_App.Entities
{
    public class ProfileDTO
    {
        public string uuid {  get; set; }

        public string bio { get; set; }

        public IFormFile? pictureUpload { get; set; }
    }
}
