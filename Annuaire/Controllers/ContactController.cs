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
        public IActionResult Index(string id, string search)
        {
            List<ContactModel> listeContacts;
            if (id != null)
                ViewBag.message = id;
            if (search == null)
                listeContacts = ContactModel.GetAllContacts();
            else
                listeContacts = ContactModel.GetContactsBySearch(search);
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
            string message = "";
            if(c.Id == 0)
            {
                c.Add();
                message = "contact ajouté";
            }
            else
            {
                c.Update();
                message = "contact modifié";
            }
            //TempData["message"] = "coucou";
            return RedirectToAction("Index", new { id = message});
        }

        public IActionResult DeleteContact(int id)
        {
            ContactModel.DeleteContact(id);
            return RedirectToAction("Index", new { id = "Contact supprimé" });
        }

        public IActionResult EditContact(int id)
        {
            return View("FormsAddContact", ContactModel.GetContactById(id));
        }
    }
}