using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Http.Mvc.Listner
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
      
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

           // Helpers.IHostSettings hostSettings = new Helpers.HostSettings(Enviornment.WebRootPath, Enviornment.ContentRootPath);

           // services.AddScoped<IHostSettings, HostSettings>();

            //services.AddScoped<ILibraryAsset, LibraryAssetService>();
            //services.AddScoped<ILibraryCheckout, LibraryCheckoutService>();
            //services.AddScoped<ILibraryPatron, LibraryPatronService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            
            //System.AppDomain.CurrentDomain.

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Contacts}/{action=Index}/{id?}");
            });
        }
    }
}
