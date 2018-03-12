using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServerClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Add the authentication services to DI and the authentication middleware to the pipeline. These will:

        // Validate the incoming token to make sure it is coming from a trusted issuer
        // Validate that the token is valid to be used with this api (aka scope)
        // Add the IdentityServer4.AccessTokenValidation NuGet package to your project.


        //AddAuthentication adds the authentication services to DI and configures "Bearer" as the default scheme. 
        //AddIdentityServerAuthentication adds the IdentityServer access token validation handler into DI for use by the authentication services. 
        //UseAuthentication adds the authentication middleware to the pipeline so authentication will be performed automatically on every call into the host.

        //If you use the browser to navigate to the controller(http://localhost:5001/identity), you should get a 401 status code in return. This means your API requires a credential.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
