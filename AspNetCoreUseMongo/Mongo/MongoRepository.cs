using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;

namespace AspNetCoreUseMongo.Mongo
{
    public class MongoRepository
    {
        // https://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/
        private readonly MongoOptions _options;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private static object _obj = new object();


        private readonly ConcurrentDictionary<RuntimeTypeHandle, object> _mongoCollections = new ConcurrentDictionary<RuntimeTypeHandle, object>();

        public MongoRepository(IOptions<MongoOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }
            _options = optionsAccessor.Value;

            if (_mongoClient == null)
            {
                lock (_obj)
                {
                    if (_mongoClient == null)
                    {
                        _mongoClient = new MongoClient(_options.ConnectionString);
                        _mongoDatabase = _mongoClient.GetDatabase(_options.DataBaseName);
                    }
                }
            }

        }

        public IMongoCollection<T> GetCollection<T>()
        {
            if (_mongoCollections.TryGetValue(typeof(T).TypeHandle, out object collectionObject))
            {
                return collectionObject as IMongoCollection<T>;
            }

            IMongoCollection<T> collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
            _mongoCollections.TryAdd(typeof(T).TypeHandle, collection);

            return collection;
        }
    }
}
