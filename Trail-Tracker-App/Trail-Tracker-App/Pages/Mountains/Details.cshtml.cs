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

        public ICollection<Picture> PictureList { get; set; }
        
        public WeatherData.Rootobject WeatherData { get; set; } = default!;

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
