using AspNetCoreUseMongo.Models;
using AspNetCoreUseMongo.Mongo;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCoreUseMongo.Controllers
{
    public class UserController : Controller
    {
        private readonly MongoRepository _mongoRepository;
        public UserController(MongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public IActionResult Insert()
        {
            _mongoRepository.GetCollection<User>().InsertOne(new Models.User
            {
                CreateTime = DateTime.Now
            });
            return Content("插入成功");
        }
    }
}
