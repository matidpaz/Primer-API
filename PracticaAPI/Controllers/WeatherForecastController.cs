using Microsoft.AspNetCore.Mvc;

namespace PracticaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private static WeatherForecast[] ListWeatherForecasts;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;

            ListWeatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return ListWeatherForecasts;
        }

        [HttpGet()]
        [Route("{id}")]
        public ActionResult<WeatherForecast> GetByPosition(int id)
        {
            //Para controlar que el id este dentro de un rango:
            if (id>5 || id<0)
            {
                return BadRequest();
            }
            return Ok(ListWeatherForecasts[id]);
        }
    }
}
