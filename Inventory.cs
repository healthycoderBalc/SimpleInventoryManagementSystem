using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace SimpleInventoryManagementSystem
{
    internal static class Inventory
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["InventoryManagementSystemDB"].ConnectionString;

        public static void AddProduct()
        {
            string productName = Utilities.RequestProductName();
            double productPrice = Utilities.RequestProductPriceOrQuantity("price");
            int productQuantity = (int)Utilities.RequestProductPriceOrQuantity("quantity");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", productName);
                    command.Parameters.AddWithValue("@Price", productPrice);
                    command.Parameters.AddWithValue("@Quantity", productQuantity);
                    command.ExecuteNonQuery();
                }

            }

            Console.WriteLine("Product added to the database.");
        }

        public static void ViewAllProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("|--------------------------------------------|");
                        Console.WriteLine("|   Product    |    $Price    |   Quantity   |");
                        Console.WriteLine("|--------------------------------------------|");
                        while (reader.Read())
                        {
                            string name = Utilities.CompletingColumnSize(reader["Name"].ToString(), 14);
                            string price = Utilities.CompletingColumnSize(reader["Price"].ToString(), 14); ;
                            string quantity = Utilities.CompletingColumnSize(reader["Quantity"].ToString(), 14);
                            Console.WriteLine($"|{name}|{price}|{quantity}|");

                        }
                        Console.WriteLine("|--------------------------------------------|");

                    }
                }
            }
        }

        public static void ManageProduct(string text, string selection)
        {
            string textCentered = Utilities.CompletingColumnSize(text, 30);
            Console.WriteLine("********************************");
            Console.WriteLine($"*{textCentered}*");
            Console.WriteLine("********************************");
            string productName = Utilities.RequestProductName();
            bool found = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Products WHERE Name = @Name";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", productName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            found = true;
                            Product product = new Product(
                                reader["Name"].ToString(),
                                Convert.ToDouble(reader["Price"]),
                                Convert.ToInt32(reader["Quantity"])
                            );
                            product.ProductId = Convert.ToInt32(reader["ProductID"]);

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
                    }
                }

            }
            if (!found)
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

        private static void EditProductAttribute(Product p, string selection)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "";

                switch (selection)
                {
                    case "1":
                        string productName = Utilities.RequestProductName();
                        p.Name = productName;
                        query = "UPDATE Products SET Name = @Name WHERE ProductID = @ProductID";
                        break;
                    case "2":
                        double productPrice = Utilities.RequestProductPriceOrQuantity("price");
                        p.Price = productPrice;
                        query = "UPDATE Products SET Price = @Price WHERE ProductID = @ProductID";
                        break;
                    case "3":
                        double productQuantity = Utilities.RequestProductPriceOrQuantity("quantity");
                        p.Quantity = productQuantity;
                        query = "UPDATE Products SET Quantity = @Quantity WHERE ProductID = @ProductID";
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                        return;

                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (selection == "1")
                    {
                        command.Parameters.AddWithValue("@Name", p.Name);
                    }
                    else if (selection == "2")
                    {
                        command.Parameters.AddWithValue("@Price", p.Price);
                    }
                    else if (selection == "3")
                    {
                        command.Parameters.AddWithValue("@Quantity", p.Quantity);
                    }
                    command.Parameters.AddWithValue("@ProductID", p.ProductId);
                    command.ExecuteNonQuery();
                }

            }

        }

        private static void DeleteProduct(Product p)
        {

            string remove = Utilities.ConfirmDeletion();
            Console.WriteLine();

            if (remove == "y")
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", p.ProductId);
                        command.ExecuteNonQuery();
                    }
                }
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
