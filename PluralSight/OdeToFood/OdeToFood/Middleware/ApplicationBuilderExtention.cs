using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtention
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, IHostingEnvironment env)
        {
            var path = System.IO.Path.Combine(env.ContentRootPath, "node_modules");
            var provider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions
            {
                RequestPath = "/node_modules",
                FileProvider = provider
            };
            app.UseStaticFiles(options);
            return app;
        }
    }
}
