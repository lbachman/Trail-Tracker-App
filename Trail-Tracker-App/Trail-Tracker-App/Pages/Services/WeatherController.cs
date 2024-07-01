using Microsoft.AspNetCore.Mvc;

namespace Trail_Tracker_App.Pages.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string key = "FZ8HVEBVWV3LA6ZK3HRVKNR5P";


        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        // GET: api/<WeatherController>
        [HttpGet("{zipCode}")]

        public async Task<IActionResult> GetWeather(string zipCode)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                string apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/weatherdata/forecast?aggregateHours=24&combinationMethod=aggregate&includeAstronomy=true&contentType=json&unitGroup=us&locationMode=single&key={key}&dataElements=default&locations=37601";

                var response = await client.GetAsync(apiUrl);
                Console.WriteLine(response.ToString());

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }

            catch (HttpRequestException)
            {
                return StatusCode(500, "Error fetching weather data.");
            }
        }  
    }
}
