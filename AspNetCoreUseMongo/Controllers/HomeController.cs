using AspNetCoreUseMongo.Models;
using AspNetCoreUseMongo.Mongo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace AspNetCoreUseMongo.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoRepository<User> _userMongoRepository;
        public HomeController(MongoRepository<User> userMongoRepository)
        {
            _userMongoRepository = userMongoRepository;
        }

        public IActionResult Index()
        {
            _userMongoRepository.Insert(new Models.User
            {
                CreateTime = DateTime.Now
            });
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
