namespace Trail_Tracker_App.Pages.Services
{
    public class WeatherData
    {

        public class Rootobject
        {
            public Columns columns { get; set; }
            public int remainingCost { get; set; }
            public int queryCost { get; set; }
            public object messages { get; set; }
            public Location location { get; set; }
        }

        public class Columns
        {
            public Temp temp { get; set; }
            public Sunrise sunrise { get; set; }
            public Maxt maxt { get; set; }
            public Visibility visibility { get; set; }
            public Wspd wspd { get; set; }
            public Heatindex heatindex { get; set; }
            public Cloudcover cloudcover { get; set; }
            public Pop pop { get; set; }
            public Mint mint { get; set; }
            public Datetime datetime { get; set; }
            public Precip precip { get; set; }
            public Moonphase moonphase { get; set; }
            public Snowdepth snowdepth { get; set; }
            public Snow snow { get; set; }
            public Sunset sunset { get; set; }
            public Name name { get; set; }
            public Humidity humidity { get; set; }
            public Wgust wgust { get; set; }
            public Conditions conditions { get; set; }
            public Windchill windchill { get; set; }
        }

        public class Temp
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Sunrise
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Maxt
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Visibility
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Wspd
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Heatindex
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Cloudcover
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Pop
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Mint
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Datetime
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Precip
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Moonphase
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Snowdepth
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Snow
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Sunset
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Name
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Humidity
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Wgust
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Conditions
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public object unit { get; set; }
        }

        public class Windchill
        {
            public string id { get; set; }
            public string name { get; set; }
            public int type { get; set; }
            public string unit { get; set; }
        }

        public class Location
        {
            public object stationContributions { get; set; }
            public Value[] values { get; set; }
            public string id { get; set; }
            public string address { get; set; }
            public string name { get; set; }
            public int index { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public float distance { get; set; }
            public float time { get; set; }
            public string tz { get; set; }
            public Currentconditions currentConditions { get; set; }
            public object alerts { get; set; }
        }

        public class Currentconditions
        {
            public float wdir { get; set; }
            public float temp { get; set; }
            public DateTime sunrise { get; set; }
            public float visibility { get; set; }
            public float wspd { get; set; }
            public string icon { get; set; }
            public string stations { get; set; }
            public object heatindex { get; set; }
            public float cloudcover { get; set; }
            public DateTime datetime { get; set; }
            public float precip { get; set; }
            public float moonphase { get; set; }
            public object snowdepth { get; set; }
            public float sealevelpressure { get; set; }
            public float dew { get; set; }
            public DateTime sunset { get; set; }
            public float humidity { get; set; }
            public float wgust { get; set; }
            public object windchill { get; set; }
        }

        public class Value
        {
            public float temp { get; set; }
            public DateTime sunrise { get; set; }
            public float maxt { get; set; }
            public float visibility { get; set; }
            public float wspd { get; set; }
            public DateTime datetimeStr { get; set; }
            public float? heatindex { get; set; }
            public float cloudcover { get; set; }
            public float pop { get; set; }
            public float mint { get; set; }
            public long datetime { get; set; }
            public float precip { get; set; }
            public float moonphase { get; set; }
            public float snowdepth { get; set; }
            public float snow { get; set; }
            public DateTime sunset { get; set; }
            public float humidity { get; set; }
            public float wgust { get; set; }
            public string conditions { get; set; }
            public object windchill { get; set; }
        }

    }
}
