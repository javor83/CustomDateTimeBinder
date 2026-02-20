using demo_proj.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace demo_proj.Controllers
{
    public class HomeController : Controller
    {

        //**********************************************************************
        public IActionResult Index()
        {
            Person p = new Person()
            {
                pAge = DateTime.Now,
                PName = "yavor"
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
