using MongoDB.Driver;
using SGM.GEP.Domain.Entities;

namespace SGM.GEP.Infra.Data.Mongo.Contexts
{
    public class GEPContextMongo: BaseMongoContext
    {
        public GEPContextMongo(string connectionString)
            : base(connectionString)
        {

        }

        public IMongoCollection<Project> Projects => GetCollection<Project>(Database);
    }

}
