using LiteDB;

namespace api.Database
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}