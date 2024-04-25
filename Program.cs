using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventoryManagementSystem
{
    internal class Program
    {

      public static void Main(string[] args)
        {
            string selection = string.Empty;
            do
            {
                selection = Utilities.ShowMenu();
                Utilities.LaunchSelection(selection);
            } while (selection != "0");
        }
    }
}
