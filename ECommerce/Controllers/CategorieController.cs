using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class CategorieController : Controller
    {
        private DataDbContext data;
        public CategorieController(DataDbContext d)
        {
            data = d;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormCategorie()
        {
            return View();
        }

        public IActionResult AddCategory([Bind("Titre")] Categorie categorie, IFormFile image)
        {
            if(image != null)
            {
                string imageFile = Path.Combine("wwwroot", "images", categorie.Titre + "-" + image.FileName);
                string urlImage ="~/images/"+categorie.Titre + "-" + image.FileName;
                var stream = System.IO.File.Create(imageFile);
                image.CopyTo(stream);
                Image img = new Image { UrlImage = urlImage };
                data.Images.Add(img);
                data.SaveChanges();
                categorie.ImageCategorie = img;
            }
            data.Categories.Add(categorie);
            data.SaveChanges();
            return RedirectToAction("Index", new { controller = "Product" });
        }
    }
}