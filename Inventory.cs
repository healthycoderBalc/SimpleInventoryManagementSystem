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

        public static void ViewAllProducts()
        {
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("|   Product    |    $Price    |   Quantity   |");
            Console.WriteLine("|--------------------------------------------|");
            for (int i = 0; i < products.Count; i++)
            {
                string name = Utilities.CompletingColumnSize(products[i].Name, 14);
                string price = Utilities.CompletingColumnSize(products[i].Price.ToString(), 14); ;
                string quantity = Utilities.CompletingColumnSize(products[i].Quantity.ToString(), 14);
                Console.WriteLine($"|{name}|{price}|{quantity}|");
            }
            Console.WriteLine("|--------------------------------------------|");

        }


    }
}
