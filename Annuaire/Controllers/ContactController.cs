using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Annuaire.Models;
using Microsoft.AspNetCore.Mvc;

namespace Annuaire.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(string id)
        {
            if (id != null)
                ViewBag.message = id;
            List<ContactModel> listeContacts = ContactModel.GetAllContacts();
            return View(listeContacts);
        }

        public IActionResult DetailContact(int? id)
        {
            if(id == null)
            {
                return View("~/Views/Shared/ErrorContact.cshtml");
            }
            return View(ContactModel.GetContactById((int)id));
        }

        public IActionResult FormsAddContact()
        {
            return View();
        }
        //Limite l'action au verb POST
        [HttpPost]
        public IActionResult AddContact(ContactModel c)
        {
            c.Add();
            //TempData["message"] = "coucou";
            return RedirectToAction("Index", new { id = "Contact ajouté"});
        }
    }
}