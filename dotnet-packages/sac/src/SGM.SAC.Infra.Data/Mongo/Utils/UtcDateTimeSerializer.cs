using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace SGM.GEP.Infra.Data.Mongo.Utils
{
    public class UtcDateTimeSerializer : DateTimeSerializer
    {
        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = base.Deserialize(context, args);
            return DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            base.Serialize(context, args, value);
        }
    }
}