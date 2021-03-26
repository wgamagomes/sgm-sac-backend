using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SGM.GEP.Infra.Data.Mongo.Utils;
using System.Linq;

namespace SGM.GEP.Infra.Data.Mongo.Contexts
{
    public abstract class BaseMongoContext : IMongoContext
    {
        protected BaseMongoContext(string connectionString)
        {
            Database = MongoConnection.GetDatabase(connectionString);
            Configuring();
        }

        public IMongoDatabase Database { get; }

        protected virtual void Configure()
        {
        }

        protected void Register<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        private void Configuring()
        {
            Configure();
        }

        public IMongoCollection<T> GetCollection<T>(string name = null)
        {
            return name != null ? Database.GetCollection<T>(name) : GetCollection<T>(Database);
        }

        public  IMongoCollection<T> GetCollection<T>(IMongoDatabase session)
        {
            var attrs = typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), false).OfType<CollectionNameAttribute>().FirstOrDefault();
            var collectionName = attrs?.Name ?? typeof(T).Name;

            return session.GetCollection<T>(collectionName);
        }
    }
}