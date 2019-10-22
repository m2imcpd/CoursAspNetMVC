using ECommerce.Models;
using ECommerce.Tools;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ViewComponents
{
    [ViewComponent(Name ="Categorie")]
    public class CategorieViewComponent : ViewComponent
    {
        private DataDbContext data;

        public CategorieViewComponent(DataDbContext _data)
        {
            data = _data;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Categorie> liste = await GetAllCategoriesAsync();
            return View("Categorie",liste);
        }

        public Task<List<Categorie>> GetAllCategoriesAsync()
        {
            return Task<List<Categorie>>.Run(()=> data.Categories.ToList());
        }

    }
}
