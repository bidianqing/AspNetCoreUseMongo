using MongoDB.Bson;
using System;

namespace AspNetCoreUseMongo.Models
{
    public class User
    {
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();
        public DateTime CreateTime { get; set; }
    }
}
