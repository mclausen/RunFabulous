using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Profile.Worker.Service.Handler;
using Rebus.Config;
using Rebus.ServiceProvider;
using Rebus.Retry.Simple;
using System.IO;

namespace Profile.Worker.Service
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class Service : StatelessService
    {
        private readonly ServiceCollection services;
        private IServiceProvider provider;

        public Service(StatelessServiceContext context)
            : base(context)
        {
            services = new ServiceCollection();
            services.AutoRegisterHandlersFromAssemblyOf<CreateProfileCommandHandler>();
        }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
           if(cancellationToken.IsCancellationRequested == false)
            {
                var connString = GetConnectionString();

                services.AddRebus(configure =>
                {
                    return configure
                        .Logging(l => l.ColoredConsole())
                        .Transport(t => t.UseAzureServiceBus(connectionStringNameOrConnectionString: connString, inputQueueAddress: "profile-input"))
                        .Options(o => o.SimpleRetryStrategy("profile-error"));
                });

                provider = services.BuildServiceProvider();
                provider.UseRebus();
            }
        }

        protected override Task OnCloseAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return base.OnCloseAsync(cancellationToken);
        }

        protected override void OnAbort()
        {
            Dispose();
            base.OnAbort();
        }

        private void Dispose()
        {
            
        }

        private string GetConnectionString()
        {
            const string path = @"C:\Deployment\ASB-ConnString.txt";
            var constring = File.ReadAllLines(path).First().Trim();
            return constring;
        }
    }
}
