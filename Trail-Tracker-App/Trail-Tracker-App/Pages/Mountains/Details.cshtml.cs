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
        
        public WeatherData.Rootobject WeatherData { get; set; } = default!;
        
        public Moon Moon { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mountain = await _context.Mountains.FirstOrDefaultAsync(m => m.MountainId == id);

            var range = await _context.Mountainranges.FirstOrDefaultAsync(x => x.RangeId == mountain.RangeId);

            // list of pictures where trail mtn id is mountain id
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
                var client = _httpClientFactory.CreateClient();
                string zipCode = Mountain.Zip; // Replace with Mountain.Zip or appropriate zip code
                string apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/weatherdata/forecast?aggregateHours=24&combinationMethod=aggregate&includeAstronomy=true&contentType=json&unitGroup=us&locationMode=single&key=FZ8HVEBVWV3LA6ZK3HRVKNR5P&dataElements=default&locations={zipCode}";

                var response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to retrieve weather data. Status code: {response.StatusCode}");
                    return Page(); 
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();
                WeatherData = JsonConvert.DeserializeObject<WeatherData.Rootobject>(jsonResponse);


                // moon logic section
                string moonHeading = "";
                string filePath = "";

                if (WeatherData.location.currentConditions.moonphase == 0 )
                {
                    moonHeading = "Full Moon";
                    filePath = "/Images/Moon/full_moon.jpeg";
                }

                else if (WeatherData.location.currentConditions.moonphase >= 0 && WeatherData.location.currentConditions.moonphase < 0.25)
                {
                    moonHeading = "Waxing Crescent";
                    filePath = "/Images/Moon/WaxingCrescent.png";
                }

                else if (WeatherData.location.currentConditions.moonphase == 0.25)
                {
                    moonHeading = "First Quarter";
                }
                else if (WeatherData.location.currentConditions.moonphase >= 0.25 && WeatherData.location.currentConditions.moonphase <0.5)
                {
                    moonHeading = "Waxing Gibbous";
                }
                else if (WeatherData.location.currentConditions.moonphase == 0.5 )
                {
                    moonHeading = "Full Moon";
                }
                else if (WeatherData.location.currentConditions.moonphase >= 0.5 && WeatherData.location.currentConditions.moonphase < 0.75)
                {
                    moonHeading = "Waning Gibbous";
                }
                else if (WeatherData.location.currentConditions.moonphase == 0.75)
                {
                    moonHeading = "Last Quarter";
                }
                else if (WeatherData.location.currentConditions.moonphase >= 0.75 && WeatherData.location.currentConditions.moonphase < 1)
                {
                    moonHeading = "Waning Crescent";
                    filePath = "/Images/Moon/WaningCrescent.jpg";

                }
                
                Moon moon = new Moon();
                moon.Heading = moonHeading;
                moon.FilePath = filePath;
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
