using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public interface IServicePanier
    {
        void AjouterProduit(int produitId);

        void RetirerProduit(int produitId);

        decimal TotalPanier();

        List<dynamic> GetProduitsPanier();

        void UpdateQuantite(int qty, int produitId);
    }
}
