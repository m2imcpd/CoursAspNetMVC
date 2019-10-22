using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Http;
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

        public IActionResult addProduct([Bind("Title, Description, Price")] Product produit, List<IFormFile> photos, List<int> Categories)
        {
            foreach(IFormFile image in photos)
            {
                string imageFile = Path.Combine("wwwroot", "images", produit.Title + "-" + image.FileName);
                string urlImage = "~/images/" + produit.Title + "-" + image.FileName;
                var stream = System.IO.File.Create(imageFile);
                image.CopyTo(stream);
                Image img = new Image { UrlImage = urlImage };
                data.Images.Add(img);
                data.SaveChanges();
                produit.Images.Add(new ImageProduit { Product = produit, Image = img});
            }
            foreach(int c in Categories)
            {
                produit.Categories.Add(new ProductCategorie { Categorie = data.Categories.Find(c), Product = produit });
            }
            data.Products.Add(produit);
            data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}