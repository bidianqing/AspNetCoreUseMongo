using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace AspNetCoreUseMongo.Mongo
{
    public class MongoRepository<T> where T : class, new()
    {
        private readonly MongoOptions _options;
        private readonly IMongoDatabase _database;
        public MongoRepository(IOptions<MongoOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }
            _options = optionsAccessor.Value;
            _database = new MongoClient(_options.ConnectionString).GetDatabase(_options.DataBaseName);
        }

        public void Insert(T document)
        {
            this._database.GetCollection<T>(typeof(T).Name).InsertOne(document);
        }
    }
}
