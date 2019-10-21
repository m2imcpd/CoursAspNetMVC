using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private DataDbContext data;

        public ProductController(DataDbContext _d)
        {
            data = _d;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormsProduct()
        {
            ViewBag.Categories = new List<SelectListItem>();
            data.Categories.Cast<Categorie>().ToList().ForEach(c =>
            {
                SelectListItem s = new SelectListItem(c.Titre, c.Id.ToString());
                ViewBag.Categories.Add(s);
            });
            return View();
        }
    }
}