using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoursAspNet.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoursAspNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {
            object o = new { Nom = id == null ? "" : id };
            HttpContext.Session.SetString("personne", JsonConvert.SerializeObject(o));
            return View();
        }

        public IActionResult About()
        {
            dynamic o =JsonConvert.DeserializeObject(HttpContext.Session.GetString("personne"));
            ViewData["Message"] = o.Nom;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
