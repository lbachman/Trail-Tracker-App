using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trail_Tracker_App.Entities;
using Trail_Tracker_App.Pages.Services;
using Newtonsoft.Json;


namespace Trail_Tracker_App.Pages.Mountains
{
    public class DetailsModel : PageModel
    {
        private readonly MountaintrailsContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        

        public DetailsModel(MountaintrailsContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;           
        }

        public Mountain Mountain { get; set; } = default!;
        public Mountainrange Range { get; set; } = default!;

        public List<Picture> PictureList { get; set; } = default!;
        
        public _7Day_MoonData.Rootobject? WeatherData { get; set; } = default!;

        public HourlyWeatherData.Rootobject HourlyData { get; set; } = default!;
        
        public Moon Moon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountain = await _context.Mountains.FirstOrDefaultAsync(m => m.MountainId == id);
            var range = await _context.Mountainranges.FirstOrDefaultAsync(x => x.RangeId == mountain.RangeId);
            var pictures = _context.Pictures.Where(x => x.Trail.MountainId == mountain.MountainId).ToList();

            if (mountain == null)
            {
                return NotFound();
            }
            else
            {
                Mountain = mountain;
                Range = range;
                PictureList = pictures;
            }

            try
            {
                string apiKey = "FZ8HVEBVWV3LA6ZK3HRVKNR5P";
                string zipCode = Mountain.Zip;
                string formattedDate = DateTime.Now.ToString("yyyy-M-d");
                
                var client = _httpClientFactory.CreateClient();

                string sevenDay_MoonEndpoint = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{zipCode}/?unitGroup=us&key={apiKey}&include=days&elements=moonrise,moonset,tempmin,tempmax,conditions,description,datetime";
                // request for 7 day forecast data
                var moonresponse = await client.GetAsync(sevenDay_MoonEndpoint);
                if (!moonresponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to retrieve weather data. {moonresponse.StatusCode}");
                    return Page();
                }
                string moonjsonResponse = await moonresponse.Content.ReadAsStringAsync();
                WeatherData = JsonConvert.DeserializeObject<_7Day_MoonData.Rootobject>(moonjsonResponse);


                string currentWeather = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{zipCode}/{formattedDate}?unitGroup=us&key={apiKey}&include=hours%2Calerts%2Ccurrent";
                // request for current data and moon data
                var responseHourly = await client.GetAsync(currentWeather);
                if (!responseHourly.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to retrieve weather data. {responseHourly.StatusCode}");
                    return Page();
                }             
                string jsonResponseHourly = await responseHourly.Content.ReadAsStringAsync();
                HourlyData = JsonConvert.DeserializeObject<HourlyWeatherData.Rootobject>(jsonResponseHourly);

                // convert sunrise and sunset to 12 hour format
                HourlyData.days[0].sunrise = ConvertTime(HourlyData.days[0].sunrise);
                HourlyData.days[0].sunset = ConvertTime(HourlyData.days[0].sunset);


                // moon logic section
                string moonHeading = "";
                string filePath = "";
                double moonPhase = HourlyData.days[0].moonphase;

                if (moonPhase >= 0.97 || moonPhase <= 0.02)
                {
                    moonHeading = "New Moon";
                    filePath = "/Images/Moon/NewMoon.jpg";
                }

                else if (moonPhase >= 0 && moonPhase < 0.25)
                {
                    moonHeading = "Waxing Crescent";
                    filePath = "/Images/Moon/WaxingCrescent.png";
                }

                else if (moonPhase == 0.25)
                {
                    moonHeading = "First Quarter";
                    filePath = "/Images/Moon/FirstQuarter.jpg";
                }
                else if (moonPhase >= 0.25 && moonPhase < 0.5)
                {
                    moonHeading = "Waxing Gibbous";
                }
                else if (moonPhase == 0.5)
                {
                    moonHeading = "Full Moon";
                    filePath = "/Images/Moon/FullMoon.jpg";
                }
                else if (moonPhase >= 0.5 && moonPhase < 0.75)
                {
                    moonHeading = "Waning Gibbous";
                }
                else if (moonPhase == 0.75)
                {
                    moonHeading = "Last Quarter";
                }
                else if (moonPhase >= 0.75 && moonPhase < 0.97)
                {
                    moonHeading = "Waning Crescent";
                    filePath = "/Images/Moon/WaningCrescent.jpg";

                }

                // calcualte moon percentage fullness by decimal value. 
                float moonPercentage = HourlyData.days[0].moonphase;
                if (moonPercentage > 0.5)
                {
                    moonPercentage = (1 - moonPercentage);
                }
                moonPercentage *= 100;

                Moon moon = new Moon()
                {
                    Heading = moonHeading,
                    Moonrise = ConvertTime(WeatherData.days[0].moonrise),
                    Moonset = ConvertTime(WeatherData.days[0].moonset),
                    Percent = moonPercentage,
                    FilePath = filePath,
                };
                
                Moon = moon;
                

                if (WeatherData == null)
                {
                    return Page();
                }

                return Page();

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return Page();
            }
        }


        /// <summary>
        /// Converts time from 24 hour format to 12 hour format. 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string ConvertTime(string time)
        {
            string formatedTime = DateTime.ParseExact(time, "HH:mm:ss", null).ToString("hh:mm:ss tt");
            return formatedTime;
        }


        public string Degrees2Direction(float degrees)
        {
            if (degrees >= 80 && degrees <= 100)
            {
                return "N";
            }
            else if (degrees >= 170 && degrees <= 190)
            {
                return "W";
            }
            else if (degrees >= 260 && degrees <= 280)
            {
                return "S";
            }
            else if (degrees >= 350 || degrees <= 10)
            {
                return "E";
            }
            else if (degrees > 10 && degrees < 80)
            {
                return "NE";
            }
            else if (degrees > 100 && degrees < 170)
            {
                return "NW";
            }
            else if (degrees > 190 && degrees < 260)
            {
                return "SW";
            }
            else
            {
                return "SE";
            }
        }


    }
}
