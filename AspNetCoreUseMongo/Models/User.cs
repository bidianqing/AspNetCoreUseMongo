using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AspNetCoreUseMongo.Models
{
    public class User
    {
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        [BsonElement("createTime")]
        public DateTime CreateTime { get; set; }
    }
}
