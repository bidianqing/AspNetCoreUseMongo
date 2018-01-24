using AspNetCoreUseMongo.Models;
using AspNetCoreUseMongo.Mongo;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCoreUseMongo.Controllers
{
    public class UserController : Controller
    {
        private readonly MongoRepository<User> _userMongoRepository;
        public UserController(MongoRepository<User> userMongoRepository)
        {
            _userMongoRepository = userMongoRepository;
        }
        public IActionResult Insert()
        {
            _userMongoRepository.Insert(new Models.User
            {
                CreateTime = DateTime.Now
            });
            return Content("插入成功");
        }

        public IActionResult List()
        {
            var userList = _userMongoRepository.Find(new Models.User());
            return View(userList);
        }
    }
}
