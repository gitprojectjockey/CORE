using LMS.Data.DataContext;
using LMS.Data.UnitOfWork;
using LMS.DataTransfer.ObjectMaps;
using LMS.Services;
using LMS.WebApi.Exceptions.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Collections.Generic;

namespace LMS.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();// could add connection strings here.
           
            Configuration = builder.Build();
            env.ConfigureNLog("nlog.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            List<ILoggerProvider> loggers = new List<ILoggerProvider>
            {
                new NLogLoggerProvider(),
            };
            ILoggerFactory loggerFactory = new LoggerFactory(loggers);
            loggerFactory.AddDebug();

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                options.Filters.Add(new GlobalExceptionFilter(loggerFactory));
                options.ReturnHttpNotAcceptable = true;
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddOptions();
            services.AddCors();


            services.Configure<LMSConfigurations>(Configuration.GetSection("LMSConfigurations"));

            services.AddSingleton(Configuration);
            services.AddSingleton(loggerFactory);
            services.AddSingleton<ILMSMaps, LMSMaps>();
            services.AddDbContext<LMSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LMSConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPatronService, PatronService>();
            services.AddScoped<ILibraryAssetService, LibraryAssetService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<ICheckoutService, CheckoutService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<ILibraryBranchService, LibraryBranchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
            app.UseMvc();
        }
    }
}
