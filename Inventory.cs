using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventoryManagementSystem
{
    internal static class Inventory
    {
        private static List<Product> products = new List<Product>();

        //internal List<Product>? Products { get ; set ; }

        public static void AddProduct()
        {
            string productName = Utilities.RequestProductName();
            double productPrice = Utilities.RequestProductPriceOrQuantity("price");
            double productQuantity = Utilities.RequestProductPriceOrQuantity("quantity");
            Product p = new Product(productName, productPrice, productQuantity);
            products.Add(p);
            Console.WriteLine($"This is the product you added: ");
            Console.WriteLine($"Product: {p.Name} - ${p.Price} - Nº {p.Quantity}");
        }

        

    }
}
