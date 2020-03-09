using System.Collections.Generic;
using api.Models;

namespace api.Services
{
    public interface IWeatherForecastService
    {
        int Delete(int id);
        IEnumerable<WeatherForecast> FindAll();
        WeatherForecast FindOne(int id);
        int Insert(WeatherForecast forecast);
        bool Update(WeatherForecast forecast);
    }
}