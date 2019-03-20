using System;
using System.IO;
using System.Reflection;
using Api.Validations;
using dapper_example.Repository;
using Data_Access.Models;
using Data_Access.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation(o =>
            {
                o.RegisterValidatorsFromAssemblyContaining<TestModelValidator>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "iTest",
                    Description = "Super cool API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
                c.SwaggerDoc("v2", new Info
                {
                    Version = "v2",
                    Title = "iTest",
                    Description = "Super cool API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.AddFluentValidationRules();
            });

            services.AddApiVersioning(o =>
           {
               o.ReportApiVersions = true;
               o.AssumeDefaultVersionWhenUnspecified = true;
               o.DefaultApiVersion = new ApiVersion(1, 0);

           });

            services.AddDistributedRedisCache(options => { options.Configuration = "127.0.0.1:6379"; });
            services.AddScoped<IRepository<TestModel>, TestRepository>();
            services.AddResponseCaching();
        }

        private string GetXmlCommentsPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            return xmlPath;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseResponseCaching();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
           });
        }
    }
}