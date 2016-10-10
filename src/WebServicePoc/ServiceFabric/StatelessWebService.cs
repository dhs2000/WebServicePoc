﻿using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace WebServicePoc.ServiceFabric
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessWebService : Microsoft.ServiceFabric.Services.Runtime.StatelessService,
        ICommunicationListener
    {
        private readonly string endpointName;

        private IWebHost webHost;

        public StatelessWebService(StatelessServiceContext serviceContext, string endpointName)
            : base(serviceContext)
        {
            this.endpointName = endpointName;
        }

        void ICommunicationListener.Abort()
        {
            this.webHost?.Dispose();
        }

        Task ICommunicationListener.CloseAsync(CancellationToken cancellationToken)
        {
            this.webHost?.Dispose();

            return Task.FromResult(true);
        }

        Task<string> ICommunicationListener.OpenAsync(CancellationToken cancellationToken)
        {
            EndpointResourceDescription endpoint = FabricRuntime.GetActivationContext().GetEndpoint(this.endpointName);

            string serverUrl = $"{endpoint.Protocol}://{FabricRuntime.GetNodeContext().IPAddressOrFQDN}:{endpoint.Port}";

            this.webHost =
                new WebHostBuilder().UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .UseUrls(serverUrl)
                    .Build();

            this.webHost.Start();

            return Task.FromResult(serverUrl);
        }

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(_ => this) };
        }
    }
}