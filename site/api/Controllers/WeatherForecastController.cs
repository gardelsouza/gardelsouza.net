using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Models;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _forecastDbService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService forecastDbService)
        {
            _forecastDbService = forecastDbService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _forecastDbService.FindAll();
        }

        [HttpGet("{id}", Name = "FindOne")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var result = _forecastDbService.FindOne(id);
            if (result != default)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<WeatherForecast> Insert(WeatherForecast dto)
        {
            var id = _forecastDbService.Insert(dto);
            if (id != default)
                return CreatedAtRoute("FindOne", new { id = id }, dto);
            else
                return BadRequest();
        }

        [HttpPut]
        public ActionResult<WeatherForecast> Update(WeatherForecast dto)
        {
            var result = _forecastDbService.Update(dto);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<WeatherForecast> Delete(int id)
        {
            var result = _forecastDbService.Delete(id);
            if (result > 0)
                return NoContent();
            else
                return NotFound();
        }
    }
}
