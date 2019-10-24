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
            //Ajouter des données dans les sessions
            //object o = new { Nom = id == null ? "" : id };
            //HttpContext.Session.SetString("personne", JsonConvert.SerializeObject(o));
            Response.Cookies.Append("nom_cookie", "value of cookie", new CookieOptions { Expires = DateTime.Now.AddDays(1) }) ;
            return View();
        }

        public IActionResult About()
        {
            //Récupérer les données à partir d'une session
            //dynamic o =JsonConvert.DeserializeObject(HttpContext.Session.GetString("personne"));
            //ViewData["Message"] = o.Nom;
            ViewData["Message"] = Request.Cookies["nom_cookie"];
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

        public IActionResult CreateCookie()
        {
            Response.Cookies.Append(".AspNet.Consent", "yes", new CookieOptions { Expires = DateTime.Now.AddYears(1) });
            return  View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
