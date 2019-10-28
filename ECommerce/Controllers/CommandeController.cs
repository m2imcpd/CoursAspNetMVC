using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    [Authorize("client")]
    public class CommandeController : Controller
    {
        private ILoginService loginService;
        private DataDbContext data;

        public CommandeController(ILoginService _loginservice, DataDbContext _data)
        {
            loginService = _loginservice;
            data = _data;
        }
        public IActionResult Index()
        {
            List<Commande> listeCommande;
            if(loginService.GetUserProfil() >=3 )
            {
                listeCommande = data.Commandes.Include("Products").Include("Client").ToList();
            }
            else 
            {
                listeCommande = data.Commandes.Include("Products").Include("Client").Where(c => c.Client.Id == loginService.GetUser().Id).ToList();
            }
            return View(listeCommande);
        }
    }
}