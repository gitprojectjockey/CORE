using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace JsonWebToken_WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        // Register JWT authentication schema by using AddAuthentication method and specifying JwtBearerDefaults.AuthenticationScheme.
        // Then we configure the authentication schema with options for JWT bearer. 
        // In particular, we specify which parameters must be taken into account in order to consider valid a JSON Web Token. 
        // Our code is saying that to consider a token valid we must:

        // Validate the server that created that token (ValidateIssuer = true);
        // Ensure that the recipient of the token is authorized to receive it(ValidateAudience = true);
        // Check that the token is not expired and that the signing key of the issuer is valid(ValidateLifetime = true);
        // Verify that the key used to sign the incoming token is part of a list of trusted keys(ValidateIssuerSigningKey = true).
        // In addition, we specify the values for the issuer, the audience and the signing key.
        // We can store these values in the appsettings.json file to make them accessible via Configuration object:

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });


            // CORS
            // Specify that our API accepts requests coming from other origins(other domains). 
            // When issuing AJAX requests, browsers make preflights to check if a server accepts requests from the domain hosting the web app. 
            // If the response for these preflights don't contain at least the Access-Control-Allow-Origin header specifying that accepts requests 
            // from the original domain, browsers won't proceed with the real requests(to improve security).

           services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials().Build());
            });


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Make the authentication service available to the application.
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
