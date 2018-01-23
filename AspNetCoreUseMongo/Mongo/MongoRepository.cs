using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Concurrent;

namespace AspNetCoreUseMongo.Mongo
{
    public class MongoRepository<T> where T : class, new()
    {
        // https://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/connecting/#re-use
        private readonly MongoOptions _options;
        private static IMongoClient _mongoClient;
        private static IMongoDatabase _mongoDatabase;
        private static object _obj = new object();

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IMongoCollection<T>> _mongoCollections = new ConcurrentDictionary<RuntimeTypeHandle, IMongoCollection<T>>();
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

        public void Insert(T document)
        {
            this.GetCollection(document).InsertOne(document);
        }

        private IMongoCollection<T> GetCollection(T document)
        {
            if (_mongoCollections.TryGetValue(document.GetType().TypeHandle, out IMongoCollection<T> collection))
            {
                return collection;
            }

            collection = _mongoDatabase.GetCollection<T>(typeof(T).Name);
            _mongoCollections.TryAdd(document.GetType().TypeHandle, collection);
            return collection;
        }
    }
}
