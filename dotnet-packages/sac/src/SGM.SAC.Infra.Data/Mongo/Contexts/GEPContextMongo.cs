using MongoDB.Driver;

namespace SGM.GEP.Infra.Data.Mongo.Contexts
{
    public class GEPContextMongo: BaseMongoContext
    {
        public GEPContextMongo(string connectionString)
            : base(connectionString)
        {

        }

        //public IMongoCollection<WhateverEntity> Projects => GetCollection<WhateverEntity>(Database);
    }

}
