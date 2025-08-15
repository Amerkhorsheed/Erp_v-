using Microsoft.Owin.Hosting;
using System;

namespace Seller.API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Define the base address for your API
            string baseAddress = "http://localhost:9001/";

            // Start the OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"API Server running at {baseAddress}");
                Console.WriteLine("Press [Enter] to quit.");
                Console.ReadLine();
            }
        }
    }
}