namespace Trail_Tracker_App.Pages.Services
{
    public class _7Day_MoonData
    {

        public class Rootobject
        {
            public int queryCost { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public string resolvedAddress { get; set; }
            public string address { get; set; }
            public string timezone { get; set; }
            public float tzoffset { get; set; }
            public Day[] days { get; set; }
        }

        public class Day
        {
            public string datetime { get; set; }
            public float tempmax { get; set; }
            public float tempmin { get; set; }
            public string moonrise { get; set; }
            public string moonset { get; set; }
            public string conditions { get; set; }
            public string description { get; set; }
        }

    }
}
