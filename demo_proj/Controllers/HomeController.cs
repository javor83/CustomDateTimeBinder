using demo_proj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo_proj.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home/NotFound")]
        public IActionResult NotFound(int statusCode)
        {
            // You can log the specific URL that failed here if needed
            return View();
        }
        //**********************************************************************
        public IActionResult Index()
        {
            Person p = new Person()
            {
                pAge = DateTime.Now,
                PName = "yavor",
                Smoke =false,
                Age = 10,
                Grades = new string[] { "A", "B1", "C1" },
                PointCards =  new PointCards() { X = 10, Y = 20 }
            };
            return View(p);
        }


        //**********************************************************************
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Index(Person sender)
        {
            if (ModelState.IsValid)
            {
                return Json(sender);
            }
            else
                return View(sender);
        }
        //**********************************************************************

        public IActionResult Privacy()
        {
            return View();
        }
        //**********************************************************************
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //**********************************************************************
    }
}
