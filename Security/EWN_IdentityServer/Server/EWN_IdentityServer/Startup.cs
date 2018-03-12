using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EWN_IdentityServer
{
    public class Startup
    {
        //AddIdentityServer registers the IdentityServer services in DI.It also registers an in-memory store for runtime state.This is useful for development scenarios. 
        //For production scenarios you need a persistent or shared store like a database or cache for that.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients()); 
        }

        //The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.Again this might be useful to get started, 
        //but needs to be replaced by some persistent key material for production scenarios. See the cryptography docs for more information.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
