using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursAPI.Model;
using CoursAPI.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoursAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private DataDbContext data;

        public ContactController(DataDbContext _data)
        {
            data = _data;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(data.Contacts.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(data.Contacts.Find(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] Contact c)
        {
            data.Contacts.Add(c);
            if(data.SaveChanges() >= 1)
            {
                return Ok(new { message = "contact ajouté", error = false, contactId = c.Id });
            }
            else
            {
                return Ok(new { message = "erreur serveur", error = true });
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Contact c)
        {
            Contact contact = data.Contacts.Find(id);
            if(contact != null)
            {
                contact.Nom = (c.Nom != null) ? c.Nom :  contact.Nom;
                contact.Prenom = (c.Prenom != null) ? c.Prenom :  contact.Prenom;
                contact.Email = (c.Email != null) ? c.Email :  contact.Email;
                if(data.SaveChanges() >=1)
                {
                    return Ok(new { message = "contact modifié", error = false, contactId = c.Id });
                }
                else
                {
                    return Ok(new { message = "erreur serveur", error = true });
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Contact contact = data.Contacts.Find(id);
            if (contact != null)
            {
                data.Contacts.Remove(contact);
                if (data.SaveChanges() >= 1)
                {
                    return Ok(new { message = "contact supprimé", error = false });
                }
                else
                {
                    return Ok(new { message = "erreur serveur", error = true });
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}