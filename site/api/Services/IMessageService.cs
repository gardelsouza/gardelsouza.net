using System.Collections.Generic;
using api.Infra;
using api.Models;

namespace api.Services
{
    public interface IMessageService
    {
        int Delete(int id);
        IEnumerable<Message> FindAll(PageParameters paging);
        IEnumerable<Message> FindPendingReading(PageParameters paging);
        Message FindOne(int id);
        int Insert(Message message);
        bool Update(Message message);
        int UpdateViewed(Message lastViewedMessage);
    }
}