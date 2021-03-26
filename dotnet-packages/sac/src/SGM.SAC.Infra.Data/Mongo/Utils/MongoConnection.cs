using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace SGM.GEP.Infra.Data.Mongo.Utils
{
    public static class MongoConnection
    {
        private static readonly object Lock = new object();

        private static bool _isRegistered;
        private static readonly Dictionary<string, IMongoDatabase> Databases = new Dictionary<string, IMongoDatabase>();

        public static IMongoDatabase GetDatabase(string connectionString)
        {
            var conn = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

            lock (Lock)
            {
                Databases.TryGetValue(conn, out var database);

                if (database != null) return database;

                var urlBuilder = new MongoUrlBuilder(conn);

                var databaseName = urlBuilder.DatabaseName;

                if (databaseName == null)
                    databaseName = GetDatabaseNameDefault();

                var client = new MongoClient(urlBuilder.ToMongoUrl());
                database = client.GetDatabase(databaseName);

                Register();

                Databases[conn] = database;

                return database;
            }
        }

        private static void Register()
        {
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreElements", conventionPack, type => true);

            if (!_isRegistered)
            {
                BsonSerializer.RegisterSerializer(typeof(DateTime), new UtcDateTimeSerializer());
                _isRegistered = true;
            }
        }

        internal static string GetDatabaseNameDefault()
        {
            return $"SGM_GEP";
        }
    }
}