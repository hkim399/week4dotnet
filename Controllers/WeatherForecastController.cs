using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers;

[ApiController]
// [Route("[controller]")]
// rather do this
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // private readonly ILogger<WeatherForecastController> _logger;

    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }

    // this is what it matters
    // [HttpGet(Name = "GetWeatherForecast")]
    // it can also be called this
    [HttpGet]
    // HttpGet is default. if you dont have this, then automatically use httpget
    // this will have only one method.

// action method of get, but still. regardless of name on the IEnumerable Get(), it can have any name
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            // you are generating random things here and creating WeatherForecast objects
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    // might need annotation here because of another one
    // [HttpGet]
    // [Route("api/[controller]/id")]
    // public WeatherForecast GetSomething(int id)
    // {
    //     var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         // you are generating random things here and creating WeatherForecast objects
    //         Date = DateTime.Now.AddDays(index),
    //         TemperatureC = Random.Shared.Next(-20, 55),
    //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     })
    //     .ToArray();

    //     return result[id];
    // }
}
