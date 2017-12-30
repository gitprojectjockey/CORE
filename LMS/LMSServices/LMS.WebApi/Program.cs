using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LMS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseIISIntegration()
                .CaptureStartupErrors(true)
                .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                .UseEnvironment("Development")
                .UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "LMS.WebApi")
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}
