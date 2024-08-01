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
        
        public WeatherData.Rootobject? WeatherData { get; set; } = default!;

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

                string zipCode = Mountain.Zip;

                string apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/weatherdata/forecast?aggregateHours=24&combinationMethod=aggregate&includeAstronomy=true&contentType=json&unitGroup=us&locationMode=single&key=FZ8HVEBVWV3LA6ZK3HRVKNR5P&dataElements=default&locations={zipCode}";

                string hourlyWeatherUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{zipCode}/2024-7-10?unitGroup=us&key=FZ8HVEBVWV3LA6ZK3HRVKNR5P&include=hours%2Calerts%2Ccurrent";

                var client = _httpClientFactory.CreateClient();
 

                var response = await client.GetAsync(apiUrl);
                var responseHourly = await client.GetAsync(hourlyWeatherUrl);

                if (!response.IsSuccessStatusCode || !responseHourly.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to retrieve weather data. Status code: {response.StatusCode}");
                    return Page();
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                string jsonResponseHourly = await responseHourly.Content.ReadAsStringAsync();
                Console.WriteLine(jsonResponseHourly);

                WeatherData = JsonConvert.DeserializeObject<WeatherData.Rootobject>(jsonResponse);
                HourlyData = JsonConvert.DeserializeObject<HourlyWeatherData.Rootobject>(jsonResponseHourly);

                

                // moon logic section
                string moonHeading = "";
                string filePath = "";
                Console.WriteLine(HourlyData.days[0].conditions);
                if (HourlyData.days[0].moonphase == 0)
                {
                    moonHeading = "New Moon";
                    filePath = "/Images/Moon/full_moon.jpeg";
                }

                else if (HourlyData.days[0].moonphase >= 0 && HourlyData.days[0].moonphase < 0.25)
                {
                    moonHeading = "Waxing Crescent";
                    filePath = "/Images/Moon/WaxingCrescent.png";
                }

                else if (HourlyData.days[0].moonphase == 0.25)
                {
                    moonHeading = "First Quarter";
                }
                else if (HourlyData.days[0].moonphase >= 0.25 && HourlyData.days[0].moonphase < 0.5)
                {
                    moonHeading = "Waxing Gibbous";
                }
                else if (HourlyData.days[0].moonphase == 0.5)
                {
                    moonHeading = "Full Moon";
                }
                else if (HourlyData.days[0].moonphase >= 0.5 && HourlyData.days[0].moonphase < 0.75)
                {
                    moonHeading = "Waning Gibbous";
                }
                else if (HourlyData.days[0].moonphase == 0.75)
                {
                    moonHeading = "Last Quarter";
                }
                else if (HourlyData.days[0].moonphase >= 0.75 && HourlyData.days[0].moonphase < 1)
                {
                    moonHeading = "Waning Crescent";
                    filePath = "/Images/Moon/WaningCrescent.jpg";

                }

                Moon moon = new Moon()
                {
                    Heading = moonHeading,
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
