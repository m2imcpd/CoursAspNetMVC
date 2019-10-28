using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class PanierController : Controller
    {
        private IServicePanier servicePanier;
        private IServiceProvider serviceProvider;

        private ILoginService serviceLogin;

        private DataDbContext data;

        public PanierController(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
            servicePanier = (IServicePanier)serviceProvider.GetService(typeof(IServicePanier));
            serviceLogin = (ILoginService)serviceProvider.GetService(typeof(ILoginService));
            data = (DataDbContext)serviceProvider.GetService(typeof(DataDbContext));
        }
        public IActionResult Index()
        {
            ViewBag.Total = servicePanier.TotalPanier();
            return View(servicePanier.GetProduitsPanier());
        }

        public IActionResult AddProduct(int id)
        {
            servicePanier.AjouterProduit(id);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int id)
        {
            servicePanier.RetirerProduit(id);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateQty(int id, int qty)
        {
            servicePanier.UpdateQuantite(qty, id);
            return RedirectToAction("Index");
        }

        [Authorize("client")]
        public IActionResult ValidPanier()
        {
            Commande c = new Commande();
            c.Client = serviceLogin.GetUser();
            foreach(dynamic p in servicePanier.GetProduitsPanier())
            {
                ProductCommande pc = new ProductCommande { Product = data.Products.Find((int)p.Produit.Id), Qty = p.qty };
                c.Products.Add(pc);
            }
            c.Total = servicePanier.TotalPanier();
            data.Commandes.Add(c);
            if(data.SaveChanges() >= 1)
            {
                servicePanier.ResetPanier();
            }
            return View();
        }
    }
}