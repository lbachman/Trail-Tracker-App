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

                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyy-M-d");
                
                string sevenDay_MoonEndpoint = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{zipCode}/?unitGroup=us&key={apiKey}&include=days&elements=moonrise,moonset,tempmin,tempmax,conditions,description,datetime";              

                string hourlyWeatherUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{zipCode}/{formattedDate}?unitGroup=us&key={apiKey}&include=hours%2Calerts%2Ccurrent";

                var client = _httpClientFactory.CreateClient();
 
                var moonresponse = await client.GetAsync(sevenDay_MoonEndpoint);

                var responseHourly = await client.GetAsync(hourlyWeatherUrl);

                if (!moonresponse.IsSuccessStatusCode || !responseHourly.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to retrieve weather data. {moonresponse.StatusCode}");
                    return Page();
                }

                string moonjsonResponse = await moonresponse.Content.ReadAsStringAsync();
                string jsonResponseHourly = await responseHourly.Content.ReadAsStringAsync();

                WeatherData = JsonConvert.DeserializeObject<_7Day_MoonData.Rootobject>(moonjsonResponse);
                HourlyData = JsonConvert.DeserializeObject<HourlyWeatherData.Rootobject>(jsonResponseHourly);

                

                // moon logic section
                string moonHeading = "";
                string filePath = "";
                if (HourlyData.days[0].moonphase >= 0.97 || HourlyData.days[0].moonphase <= 0.02)
                {
                    moonHeading = "New Moon";
                    filePath = "/Images/Moon/NewMoon.jpg";
                }

                else if (HourlyData.days[0].moonphase >= 0 && HourlyData.days[0].moonphase < 0.25)
                {
                    moonHeading = "Waxing Crescent";
                    filePath = "/Images/Moon/WaxingCrescent.png";
                }

                else if (HourlyData.days[0].moonphase == 0.25)
                {
                    moonHeading = "First Quarter";
                    filePath = "/Images/Moon/FirstQuarter.jpg";
                }
                else if (HourlyData.days[0].moonphase >= 0.25 && HourlyData.days[0].moonphase < 0.5)
                {
                    moonHeading = "Waxing Gibbous";
                }
                else if (HourlyData.days[0].moonphase == 0.5)
                {
                    moonHeading = "Full Moon";
                    filePath = "/Images/Moon/FullMoon.jpg";
                }
                else if (HourlyData.days[0].moonphase >= 0.5 && HourlyData.days[0].moonphase < 0.75)
                {
                    moonHeading = "Waning Gibbous";
                }
                else if (HourlyData.days[0].moonphase == 0.75)
                {
                    moonHeading = "Last Quarter";
                }
                else if (HourlyData.days[0].moonphase >= 0.75 && HourlyData.days[0].moonphase < 0.97)
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


                string rise = WeatherData.days[0].moonrise;
                // Parse the 24-hour format time to a DateTime object
                DateTime time = DateTime.ParseExact(rise, "HH:mm:ss", null);
                // Convert to 12-hour format with AM/PM
                string rise12Hour = time.ToString("hh:mm:ss tt");
                string set = WeatherData.days[0].moonset;
                DateTime time2 = DateTime.ParseExact(set, "HH:mm:ss", null);
                string set12Hour = time2.ToString("hh:mm:ss tt");


                Moon moon = new Moon()
                {
                    Heading = moonHeading,
                    Moonrise = rise12Hour,
                    Moonset = set12Hour,
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
    }
}
