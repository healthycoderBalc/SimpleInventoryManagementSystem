﻿using System;
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
            Console.WriteLine(p.ToString());
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


        public static void EditProduct()
        {
            Console.WriteLine("***********************");
            Console.WriteLine("** Editing a product **");
            Console.WriteLine("***********************");
            string productName = Utilities.RequestProductName();
            bool found = false;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == productName)
                {
                    found = true;
                    Console.WriteLine("Product found: ");
                    Console.WriteLine(products[i].ToString());
                    string selection;
                    do
                    {
                        selection = Utilities.ShowEditMenu();
                        EditProductAttribute(products[i], selection);
                        Console.WriteLine();
                        Console.WriteLine($"This is the product you updated: ");
                        Console.WriteLine(products[i].ToString());
                        Console.WriteLine();

                    } while (selection != "0");
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("There is no product with such name");

            }
        }

        private static void EditProductAttribute(Product p, string selection)
        {
            switch (selection)
            {
                case "1":
                    string productName = Utilities.RequestProductName();
                    p.Name = productName;
                    break;
                case "2":
                    double productPrice = Utilities.RequestProductPriceOrQuantity("price");
                    p.Price = productPrice;
                    break;
                case "3":
                    double productQuantity = Utilities.RequestProductPriceOrQuantity("quantity");
                    p.Quantity = productQuantity;
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;

            }

        }


        public static void DeleteProduct()
        {
            Console.WriteLine("************************");
            Console.WriteLine("** Deleting a product **");
            Console.WriteLine("************************");
            string productName = Utilities.RequestProductName();
            bool found = false;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == productName)
                {
                    found = true;
                    Console.WriteLine("Product found: ");
                    Console.WriteLine(products[i].ToString());
                    string remove = Utilities.ConfirmDeletion();
                    Console.WriteLine();

                    if (remove == "y")
                    {
                        products.Remove(products[i]);
                        Console.WriteLine("The product has been deleted successfully");
                        Console.WriteLine("This is the updated list of products in the inventory: ");
                    }
                    else
                    {
                        Console.WriteLine("Deletion cancelled");
                        Console.WriteLine("The list of products in the inventory has not changed");
                    }

                    Console.WriteLine();
                    ViewAllProducts();
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("There is no product with such name");

            }

        }

        public static void SearchProduct()
        {
            Console.WriteLine("*****************************");
            Console.WriteLine("** Searching for a product **");
            Console.WriteLine("*****************************");
            string productName = Utilities.RequestProductName();
            bool found = false;

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == productName)
                {
                    found = true;

                    Console.WriteLine("Product found: ");
                    Console.WriteLine(products[i].ToString());
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("There is no product with such name");
            }
        }
    }



}
