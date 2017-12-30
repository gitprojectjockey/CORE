using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;


namespace Http.Mvc.Listner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Validate Contacts Json File Service";
            Console.ForegroundColor = ConsoleColor.Cyan;
            var pathToContentRoot = Directory.GetCurrentDirectory();

            var host = new WebHostBuilder()
                     .UseKestrel()
                     .UseContentRoot(pathToContentRoot)
                     .UseIISIntegration()
                     .UseStartup<Startup>()
                     .UseApplicationInsights()
                     .Build();

            host.Run();
        }
    }
}
