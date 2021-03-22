using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = ConfigurationManager.AppSettings["MY_KEY"]; 
            string connectionString = ConfigurationManager.ConnectionStrings["MY_CONNECTION"].ConnectionString;
            Console.WriteLine("{0}: {1}", msg, connectionString);
        }
    }
}
