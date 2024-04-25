﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventoryManagementSystem
{
    internal class Utilities
    {

        internal static string ShowMenu()
        {
            Console.WriteLine("*****************************");
            Console.WriteLine("***Please select an option***");
            Console.WriteLine("* 1 - Add a product         *");
            Console.WriteLine("* 2 - View all products     *");
            Console.WriteLine("* 3 - Edit a product        *");
            Console.WriteLine("* 4 - Delete a product      *");
            Console.WriteLine("* 5 - Search for a product  *");
            Console.WriteLine("* 0 - Exit                  *");

            Console.Write("Your selection is: ");
            string? selection = Console.ReadLine();
            return selection != null ? selection : "0";
        }

        internal static void LaunchSelection(string selection)
        {
            switch (selection)
            {
                case "1":
                    Inventory.AddProduct();
                    Console.WriteLine();
                    Console.ReadLine();
                    //ShowMenu();
                    break;
                case "2":
                    Inventory.ViewAllProducts();
                    Console.WriteLine();
                    Console.ReadLine();
                    //ShowMenu();
                    break;
                case "3":
                    // Edit Product
                    break;
                case "4":
                    // Delete Product
                    break;
                case "5":
                    // Search Product
                    break;
                case "0":
                    // Exit
                    break;
                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;
            }
        }

        internal static string RequestProductName()
        {
            string? productName = null;
            while (productName == null)
            {
                Console.Write("Please write the name of the product: ");
                productName = Console.ReadLine();
            }
            return productName;
        }
        internal static double RequestProductPriceOrQuantity(string priceOrQuantity)
        {
            double value = 0.0;
            while (value <= 0)
            {
                Console.Write($"Please write the {priceOrQuantity} of the product: ");
                string? input = Console.ReadLine();

                if (!double.TryParse(input, out value))
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive number.");
                }
                else
                {
                    value = Convert.ToDouble(input);

                }
            }
            return value;
        }

        internal static string CompletingColumnSize(string columnText, int columnSize)
        {
            int textLenght = columnText.Length;
            int remainingSpace = columnSize - textLenght;
            string spaceToAddAfter = string.Empty;
            string spaceToAddBefore = string.Empty;
            if (remainingSpace > 0)
            {
                int spaceAfter = remainingSpace % 2 == 0 ? remainingSpace / 2 : (remainingSpace/2 + 1);
                spaceToAddAfter = new string(' ', (spaceAfter));
                spaceToAddBefore = new string(' ', remainingSpace/2);
            }
            string textCompleted = spaceToAddBefore+columnText+spaceToAddAfter;
            return textCompleted;
        }

    }
}
