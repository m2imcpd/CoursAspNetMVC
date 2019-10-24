using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class UserController : Controller
    {
        private DataDbContext data;

        public UserController(DataDbContext _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRegister([Bind("Nom, Prenom, Email, Password")] UserModel user)
        {
            if(user.Password.Length < 4)
            {
                ViewBag.message = "Error password";
                return View("Index");
            }
            else
            {
                data.Users.Add(user);
                data.SaveChanges();
                return RedirectToAction("Index", "Panier",null);
            }
        }
    }
}