using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventoryManagementSystem
{
    internal class Product
    {
        private string name = string.Empty;
        private double price;
        private double quantity;


        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public double Quantity { get => quantity; set => quantity = value; }
        public Product(string name, double price, double quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }


    }
}
