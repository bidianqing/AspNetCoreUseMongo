using AspNetCoreUseMongo.Models;
using AspNetCoreUseMongo.Mongo;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCoreUseMongo.Controllers
{
    public class LogController : Controller
    {
        private readonly MongoRepository<Log> _logMongoRepository;
        public LogController(MongoRepository<Log> logMongoRepository)
        {
            _logMongoRepository = logMongoRepository;
        }
        public IActionResult Insert()
        {
            _logMongoRepository.Insert(new Models.Log
            {
                CreateTime = DateTime.Now
            });
            return Content("插入成功");
        }

        public IActionResult List()
        {
            var userList = _logMongoRepository.Find(new Models.Log());
            return View(userList);
        }
    }
}
