using ECommerce.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public class ServicePanier : IServicePanier
    {
        private DataDbContext data;
        private IHttpContextAccessor accessor;
        private ISession session => accessor.HttpContext.Session;

        public ServicePanier(IHttpContextAccessor _accessor, DataDbContext _data)
        {
            data = _data;
            accessor = _accessor;
        }

        public List<dynamic> GetProduitsPanier()
        {
            return (session.GetString("panier") != null) ? JsonConvert.DeserializeObject<List<dynamic>>(session.GetString("panier")) : new List<dynamic>();
        }
        public void AjouterProduit(int produitId)
        {
            List<dynamic> panier = GetProduitsPanier();
            Product p = data.Products.FirstOrDefault(x => x.Id == produitId);
            if(p!= null)
            {
                dynamic pPanier = panier.FirstOrDefault(x => x.Produit.Id == p.Id);
                if(pPanier != null)
                {
                    pPanier.qty = (int)pPanier.qty + 1;
                }
                else
                {
                    panier.Add(new { Produit = p, qty = 1 });
                }
            }
            session.SetString("panier", JsonConvert.SerializeObject(panier));
        }

        public void RetirerProduit(int produitId)
        {
            List<dynamic> panier = GetProduitsPanier();
            Product p = data.Products.FirstOrDefault(x => x.Id == produitId);
            if(p != null)
            {
                dynamic pPanier = panier.FirstOrDefault(x => x.Produit.Id == p.Id);
                if (pPanier != null)
                {
                    panier.Remove(pPanier);
                }
            }
            session.SetString("panier", JsonConvert.SerializeObject(panier));
        }

        public void UpdateQuantite(int qty, int produitId)
        {
            List<dynamic> panier = GetProduitsPanier();
            dynamic pPanier = panier.FirstOrDefault(x => x.Produit.Id == produitId);
            if(pPanier != null)
            {
                if (pPanier.qty > 0)
                {
                    pPanier.qty += qty;
                    if (pPanier.qty == 0)
                        RetirerProduit(produitId);
                    else 
                        session.SetString("panier", JsonConvert.SerializeObject(panier));
                } 
            }
        }

        public decimal TotalPanier()
        {
            decimal total = 0;
            GetProduitsPanier().ForEach(p =>
            {
                total += (decimal)p.qty * (decimal)p.Produit.Price;
            });
            return total;
        }

        public void ResetPanier()
        {
            session.SetString("panier", JsonConvert.SerializeObject(new List<dynamic>()));
        }
    }
}
