using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rebus.Config;
using Rebus.ServiceProvider;
using Rebus.AzureServiceBus;
using Rebus.Routing.TypeBased;
using Profile.Messages.External.Commands;
using System.IO;

namespace Gateway.Api.Service
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connstring = GetConnectionString();

            services.AddRebus(configure =>
            {
                return configure
                    .Logging(l => l.ColoredConsole())
                    .Transport(t => t.UseAzureServiceBusAsOneWayClient(connstring))
                    .Routing(r => r.TypeBased().Map<CreateProfileCommand>("profile-input"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseRebus();
        }

        private string GetConnectionString()
        {
            const string path = @"C:\Deployment\ASB-ConnString.txt";
            var constring = File.ReadAllLines(path).First().Trim();
            return constring;
        }
    }
}
