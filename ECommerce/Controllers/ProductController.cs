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
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private DataDbContext data;
        private IResizeImageService serviceImage;

        public ProductController(DataDbContext _data, IResizeImageService _serviceImage)
        {
            data = _data;
            serviceImage = _serviceImage;
        }
        public IActionResult Index(int? id)
        {
            List<Product> liste;
            if (id == null)
                liste = data.Products.Include("Categories").Include("Images").ToList();
            else
                //Foreach sur Products pour garder que les produits dont l'id d'une catégorie est égale à l'Id
                liste = data.Products.Include("Categories").Include("Images").Where(p => p.Categories.Any(c => c.CategorieId == id)).ToList();

            liste.ForEach(produit =>
            {
                produit.Images.ToList().ForEach(img =>
                {
                    img.Image = data.Images.Find(img.ImageId);
                });
            });
           return View(liste);
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
                string imageFile = Path.Combine("wwwroot", "images", produit.Title + "-" + Path.GetFileName(image.FileName));
                string urlImage = "http://"+Request.Host+ "/images/" + produit.Title + "-" + Path.GetFileName(image.FileName);
                //var stream = System.IO.File.Create(imageFile);
                //image.CopyTo(stream);
                //stream.Close();
                BinaryReader reader = new BinaryReader(image.OpenReadStream());
                serviceImage.SaveImage(reader.ReadBytes((int)image.Length), imageFile, 250, 250);
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

        public IActionResult Detail(int id)
        {
            Product p = data.Products.Include("Images").FirstOrDefault(x => x.Id == id);
            p.Images.ToList().ForEach(img =>
            {
                img.Image = data.Images.Find(img.ImageId);
            });
            return View(p);
        }
    }
}