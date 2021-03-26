using MongoDB.Driver;

namespace SGM.GEP.Infra.Data.Mongo.Contexts
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}