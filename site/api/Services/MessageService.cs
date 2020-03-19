using LiteDB;
using System.Collections.Generic;
using System.Linq;
using api.Database;
using api.Models;
using api.Infra;

namespace api.Services
{
    public class MessageService : IMessageService
    {

        private LiteDatabase _liteDb;

        public MessageService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public IEnumerable<Message> FindAll(PageParameters paging)
        {
            var result = _liteDb.GetCollection<Message>("Messages")
                .FindAll()
                .OrderBy(x => x.Date)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);
            return result;
        }

        public IEnumerable<Message> FindPendingReading(PageParameters paging)
        {
            var result = _liteDb.GetCollection<Message>("Messages")
                .Find(x => x.Viewed == false)
                .OrderBy(x => x.Date)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);
            return result;
        }

        public Message FindOne(int id)
        {
            return _liteDb.GetCollection<Message>("Messages")
                .Find(x => x.Id == id).FirstOrDefault();
        }

        public int Insert(Message forecast)
        {
            return _liteDb.GetCollection<Message>("Messages")
                .Insert(forecast);
        }

        public bool Update(Message forecast)
        {
            return _liteDb.GetCollection<Message>("Messages")
                .Update(forecast);
        }
        public int UpdateViewed(Message lastViewedMessage)
        {
            return _liteDb.GetCollection<Message>("Messages")
                .UpdateMany("$.Viewed = false", $"$.Id < {lastViewedMessage.Id}");
        }

        public int Delete(int id)
        {
            return _liteDb.GetCollection<Message>("Messages")
                .DeleteMany(x => x.Id == id);
        }

    }

}