using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbanda.Domain.Domain;

namespace Umbanda.Api
{
    public class Startup
    {
        readonly string PolicyCors = "AllowOrigins";
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region INJECTION MIGRATIONS MODELS CLASS

            //services.AddDbContext<Context>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("VDATA.AUTHENTICATION"),
            //                b => b.MigrationsAssembly(typeof(Context).Assembly.FullName)));

            //services.AddDbContext<Context>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("VDATA.AUTHENTICATION")));

            #endregion

            #region DEPENDENCY INJECTION 

            //services.AddScoped<Context>(provider => provider.GetService<Context>());

            #endregion

            #region INJECTION ORACLE

          

            #endregion

            //services.AddCors(options => {
            //    options.AddPolicy(name: PolicyCors,
            //                      builder => {
            //                          builder.WithOrigins("http://localhost:3000/", "https://localhost:3000/")
            //                                 .AllowAnyHeader()
            //                                 .AllowAnyMethod()
            //                                 .AllowAnyOrigin();
            //                      });
            //});

            #region SWAGGER

            services.AddSwaggerGen(swagger => {
                swagger.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Jwt Authorization header using Bearer scheme. \r\r\rn Enter 'Bearer' [space] and then your in the text input below.\r\n\rnExemple: \"Bearer 123456ysgdys\"",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                       new OpenApiSecurityScheme{
                           Reference =  new OpenApiReference {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           },
                       }, new List<string>()
                    }
                });
                var contacts = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Email = "paulo000natale@gmail.com",
                    Extensions = null,
                    Name = "Paulo Roberto Natale Junior",
                    Url = null
                };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1, new OpenApiInfo
                {
                    Contact = contacts,
                    Description = SwaggerConfiguration.DocInfoDescription,
                    Extensions = null,
                    License = new OpenApiLicense(),
                    TermsOfService = null,
                    Title = SwaggerConfiguration.DocInfoTitle,
                    Version = SwaggerConfiguration.DocInfoVersion
                });
                //swagger.IncludeXmlComments(xmlPath);
                swagger.OperationFilter<DefaulHeaderFilter>();
            });

            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseCors(PolicyCors);

            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowAnyOrigin());



            app.UseRouting();

            //app.UseAuthorization();

            #region SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl,
                                  SwaggerConfiguration.EndpointDescription);
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
