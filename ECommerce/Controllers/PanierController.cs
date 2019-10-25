using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class PanierController : Controller
    {
        private IServicePanier servicePanier;
        private IServiceProvider serviceProvider;

        public PanierController(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
            servicePanier = (IServicePanier)serviceProvider.GetService(typeof(IServicePanier));
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
            return View();
        }
    }
}