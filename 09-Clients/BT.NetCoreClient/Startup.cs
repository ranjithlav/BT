using AutoMapper;
using BT.Contacts.Application.Api;
using BT.Contacts.Domain;
using BT.Contacts.Infrastructure.Api.Repository;
using BT.Contacts.Infrastructure.Implementation.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Appl = BT.Contacts.Application.Implementation;

namespace BT.NetCoreClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DB>(Configuration.GetSection("DBConfiguration"));
            IocRegistration(services);

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "BT.Contacts", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var log4ConfigFilename = $"log4net.{env.EnvironmentName}.config";
            if (File.Exists(log4ConfigFilename))
            {
                loggerFactory.AddLog4Net(log4ConfigFilename);
            }
            else
            {
                loggerFactory.AddLog4Net();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "BT.Contacts");
            });
        }

        private void IocRegistration(IServiceCollection services)
        {
            services.AddTransient<IContacts, Appl.Contacts>();
            services.AddTransient<IContactRepo, ContactRepo>();
        }
    }
}
