using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventoryManagementSystem
{
    internal static class Inventory
    {
        private static readonly string connectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
        private static readonly string databaseName = ConfigurationManager.AppSettings["MongoDbDatabaseName"];
        private static readonly IMongoClient client = new MongoClient(connectionString);
        private static readonly IMongoDatabase database = client.GetDatabase(databaseName);
        private static readonly IMongoCollection<Product> productCollection = database.GetCollection<Product>("Products");

        public static void AddProduct()
        {
            string productName = Utilities.RequestProductName();
            double productPrice = Utilities.RequestProductPriceOrQuantity("price");
            int productQuantity = (int)Utilities.RequestProductPriceOrQuantity("quantity");

            Product p = new Product(productName, productPrice, productQuantity);
            productCollection.InsertOne(p);

            Console.WriteLine($"Product added to the database");
        }

        public static void ViewAllProducts()
        {
            var products = productCollection.Find(new BsonDocument()).ToList();

            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("|   Product    |    $Price    |   Quantity   |");
            Console.WriteLine("|--------------------------------------------|");

            foreach (var product in products)
            {
                string name = Utilities.CompletingColumnSize(product.Name, 14);
                string price = Utilities.CompletingColumnSize(product.Price.ToString(), 14); ;
                string quantity = Utilities.CompletingColumnSize(product.Quantity.ToString(), 14);
                Console.WriteLine($"|{name}|{price}|{quantity}|");
            }
            Console.WriteLine("|--------------------------------------------|");
        }

        public static void ManageProduct(string text, string selection)
        {
            string textCentered = Utilities.CompletingColumnSize(text, 30);
            Console.WriteLine("********************************");
            Console.WriteLine($"*{textCentered}*");
            Console.WriteLine("********************************");
            string productName = Utilities.RequestProductName();
            var filter = Builders<Product>.Filter.Eq("Name", productName);
            var product = productCollection.Find(filter).FirstOrDefault();

            if (product != null)
            {
                Console.WriteLine("Product found: ");
                Console.WriteLine(product.ToString());
                switch (selection)
                {
                    case "3":
                        EditProduct(product);
                        break;
                    case "4":
                        DeleteProduct(product);
                        break;
                    case "5":
                        break;
                }
            }
            else
            {
                Console.WriteLine("There is no product with such name");
            }
        }

        private static void EditProduct(Product p)
        {
            string selection;
            do
            {
                selection = Utilities.ShowEditMenu();
                EditProductAttribute(p, selection);
                Console.WriteLine();
                Console.WriteLine($"This is the product you updated: ");
                Console.WriteLine(p.ToString());
                Console.WriteLine();

            } while (selection != "0");
        }

        private static void EditProductAttribute(Product product, string selection)
        {
            var filter = Builders<Product>.Filter.Eq("Id", product.Id);
            switch (selection)
            {
                case "1":
                    string productName = Utilities.RequestProductName();
                    var updateName = Builders<Product>.Update.Set("Name", productName);
                    productCollection.UpdateOne(filter, updateName);
                    product.Name = productName;
                    break;
                case "2":
                    double productPrice = Utilities.RequestProductPriceOrQuantity("price");
                    var updatePrice = Builders<Product>.Update.Set("Price", productPrice);
                    productCollection.UpdateOne(filter, updatePrice);
                    product.Price = productPrice;
                    break;
                case "3":
                    int productQuantity = (int)Utilities.RequestProductPriceOrQuantity("quantity");
                    var updateQuantity = Builders<Product>.Update.Set("Quantity", productQuantity);
                    productCollection.UpdateOne(filter, updateQuantity);
                    product.Quantity = productQuantity;
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    return;

            }

        }

        private static void DeleteProduct(Product product)
        {

            string remove = Utilities.ConfirmDeletion();
            Console.WriteLine();

            if (remove == "y")
            {
                var filter = Builders<Product>.Filter.Eq("Id", product.Id);
                productCollection.DeleteOne(filter);
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
        }

        public static void ExitApplication()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("*  Thank you for you time  *");
            Console.WriteLine("****************************");
        }
    }
}
