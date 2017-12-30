using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OdeToFood.Services;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _myConfigurations;

        public Startup(IHostingEnvironment env)
        {
            _myConfigurations = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json")
                    .Build();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();



            // Register application services.
            // Dependency Injection
            //services.AddSingleton(provider => _myConfigurations);
        
            services.AddSingleton<IGreeterService, GreeterService>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(_myConfigurations["database:connection"]));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<OdeToFoodDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //..
                //Pretty error page
            }

            //DefaultFilesOptions options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.html");
            //app.UseDefaultFiles(options);
            //OR
            var options = new FileServerOptions();
            options.EnableDirectoryBrowsing = false;
            options.RequestPath = "/wwwroot";
            app.UseFileServer(options);
            app.UseAuthentication();

            // This is my extention method on IApplicationBuilder 
            // This extension allows us to work with static files that are not in wwwroot
            app.UseNodeModules(env);
            app.UseMvc(ConfigureRoutes);


            //app.Run is terminal it will not call into any other middleware
            app.Run(async (context) =>
            {

                
                await context.Response.WriteAsync("Hello");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
