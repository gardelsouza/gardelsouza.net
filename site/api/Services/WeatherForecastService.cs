using LiteDB;
using System.Collections.Generic;
using System.Linq;
using api.Database;
using api.Models;

namespace api.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {

        private LiteDatabase _liteDb;

        public WeatherForecastService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<WeatherForecast> FindAll()
        {
            var result = _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .FindAll();
            return result;
        }

        public WeatherForecast FindOne(int id)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(WeatherForecast forecast)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Insert(forecast);
        }

        public bool Update(WeatherForecast forecast)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .Update(forecast);
        }

        public int Delete(int id)
        {
            return _liteDb.GetCollection<WeatherForecast>("WeatherForecast")
                .DeleteMany(x => x.Id == id);
        }
    }

}