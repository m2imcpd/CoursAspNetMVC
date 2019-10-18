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
        public IActionResult Index()
        {
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
    }
}