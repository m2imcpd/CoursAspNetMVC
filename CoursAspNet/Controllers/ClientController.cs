using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursAspNet.Models;
using CoursAspNet.Tools;
using Microsoft.AspNetCore.Mvc;

namespace CoursAspNet.Controllers
{
    public class ClientController : Controller
    {
        public string GetClient()
        {
            return "abadi";
        }

        public IActionResult GetAllClients()
        {
            List<object> listeClients = new List<object>();
            listeClients.Add(new { Nom = "toto", Prenom = "tata" });
            listeClients.Add(new { Nom = "titi", Prenom = "minet" });

            return Json(listeClients);
        }

        public IActionResult GetContentClient()
        {
            Response.ContentType = "text/html";
            return Content("<html><head></head><body><h1>Bonjour tout le monde</h1></body></html>");
        }

        public IActionResult GetViewClient()
        {
            //return view même nom que l'action
            //return View()
            //return view qui s'appelle Index
            //return View("Index");
            //return view avec le chemin de la view

            ViewData["nom"] = "abadi";
            ViewData["prenom"] = "ihab";

            List<dynamic> listePersonne = new List<dynamic>
            {
                new {Nom = "toto", Prenom="tata"},
                new {Nom = "titi", Prenom="minet"},
            };

            ViewData["liste"] = listePersonne;

            ViewBag.Rue = "Paris";
            ViewBag.Ville = "Tourcoing";

            return View("~/Views/Client/Index.cshtml");
        }

        public IActionResult DetailPersonne()
        {
            PersonneModel personne = new PersonneModel() { Nom = "abadi", Prenom = "ihab" };
            DataDbContext context = new DataDbContext();
            context.PersonnesASP.Add(personne);
            context.SaveChanges();
            return View(personne);
        }

        public IActionResult ListePersonnes()
        {

            DataDbContext data = new DataDbContext();
            return View(data.PersonnesASP.ToList());
        }

        public IActionResult AddClientForms()
        {
            return View();
        }

        //public IActionResult ValidAddClient(string nom, string prenom)
        //{
        //    //return View("ListePersonnes");
        //    PersonneModel p = new PersonneModel { Nom = nom, Prenom = prenom };
        //    DataDbContext data = new DataDbContext();
        //    data.PersonnesASP.Add(p);
        //    data.SaveChanges();
        //    return RedirectToAction("ListePersonnes");
        //}

        public IActionResult ValidAddClient(PersonneModel p)
        { 
            DataDbContext data = new DataDbContext();
            data.PersonnesASP.Add(p);
            data.SaveChanges();
            return RedirectToAction("ListePersonnes");
        }
    }
}