namespace Trail_Tracker_App.Pages.Services
{
    public class HourlyWeatherData
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
            public object[] alerts { get; set; }
            public Stations stations { get; set; }
            public Currentconditions currentConditions { get; set; }
        }

        public class Stations
        {
            public KVJI KVJI { get; set; }
            public E6448 E6448 { get; set; }
            public KTRI KTRI { get; set; }
            public F5248 F5248 { get; set; }
            public K0A9 K0A9 { get; set; }
        }

        public class KVJI
        {
            public float distance { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int useCount { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public int quality { get; set; }
            public float contribution { get; set; }
        }

        public class E6448
        {
            public float distance { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int useCount { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public int quality { get; set; }
            public float contribution { get; set; }
        }

        public class KTRI
        {
            public float distance { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int useCount { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public int quality { get; set; }
            public float contribution { get; set; }
        }

        public class F5248
        {
            public float distance { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int useCount { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public int quality { get; set; }
            public float contribution { get; set; }
        }

        public class K0A9
        {
            public float distance { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int useCount { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public int quality { get; set; }
            public float contribution { get; set; }
        }

        public class Currentconditions
        {
            public string datetime { get; set; }
            public int datetimeEpoch { get; set; }
            public float temp { get; set; }
            public float feelslike { get; set; }
            public float humidity { get; set; }
            public float dew { get; set; }
            public float precip { get; set; }
            public float precipprob { get; set; }
            public float snow { get; set; }
            public float snowdepth { get; set; }
            public object preciptype { get; set; }
            public float windgust { get; set; }
            public float windspeed { get; set; }
            public float winddir { get; set; }
            public float pressure { get; set; }
            public float visibility { get; set; }
            public float cloudcover { get; set; }
            public float solarradiation { get; set; }
            public float solarenergy { get; set; }
            public float uvindex { get; set; }
            public string conditions { get; set; }
            public string icon { get; set; }
            public string[] stations { get; set; }
            public string source { get; set; }
            public string sunrise { get; set; }
            public int sunriseEpoch { get; set; }
            public string sunset { get; set; }
            public int sunsetEpoch { get; set; }
            public float moonphase { get; set; }
        }

        public class Day
        {
            public string datetime { get; set; }
            public int datetimeEpoch { get; set; }
            public float tempmax { get; set; }
            public float tempmin { get; set; }
            public float temp { get; set; }
            public float feelslikemax { get; set; }
            public float feelslikemin { get; set; }
            public float feelslike { get; set; }
            public float dew { get; set; }
            public float humidity { get; set; }
            public float precip { get; set; }
            public float precipprob { get; set; }
            public float precipcover { get; set; }
            public object preciptype { get; set; }
            public float snow { get; set; }
            public float snowdepth { get; set; }
            public float windgust { get; set; }
            public float windspeed { get; set; }
            public float winddir { get; set; }
            public float pressure { get; set; }
            public float cloudcover { get; set; }
            public float visibility { get; set; }
            public float solarradiation { get; set; }
            public float solarenergy { get; set; }
            public float uvindex { get; set; }
            public float severerisk { get; set; }
            public string sunrise { get; set; }
            public int sunriseEpoch { get; set; }
            public string sunset { get; set; }
            public int sunsetEpoch { get; set; }
            public float moonphase { get; set; }
            public string conditions { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
            public string[] stations { get; set; }
            public string source { get; set; }
            public Hour[] hours { get; set; }
        }

        public class Hour
        {
            public string datetime { get; set; }
            public int datetimeEpoch { get; set; }
            public float temp { get; set; }
            public float feelslike { get; set; }
            public float humidity { get; set; }
            public float dew { get; set; }
            public float precip { get; set; }
            public float precipprob { get; set; }
            public float snow { get; set; }
            public float snowdepth { get; set; }
            public object preciptype { get; set; }
            public float windgust { get; set; }
            public float windspeed { get; set; }
            public float winddir { get; set; }
            public float pressure { get; set; }
            public float visibility { get; set; }
            public float cloudcover { get; set; }
            public float solarradiation { get; set; }
            public float solarenergy { get; set; }
            public float uvindex { get; set; }
            public float severerisk { get; set; }
            public string conditions { get; set; }
            public string icon { get; set; }
            public string[] stations { get; set; }
            public string source { get; set; }
        }


    }
}
