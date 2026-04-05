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
        /// <summary>
        /// Return all weather forecast
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Retorna lista de weatherforecast");
            return ListWeatherForecasts;
        }

        /// <summary>
        /// Return the weather by position
        /// </summary>
        /// <param name="id">Position</param>
        /// <returns>The element by position</returns>
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
