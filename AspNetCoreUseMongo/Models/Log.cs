using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AspNetCoreUseMongo.Models
{
    public class Log
    {
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
