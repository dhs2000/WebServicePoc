using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Communication.Runtime;

namespace WebServicePoc.ServiceFabric
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessWebService : Microsoft.ServiceFabric.Services.Runtime.StatelessService, ICommunicationListener
    {
        private readonly string endpointName;

        private readonly IWebHostBuilder webHostBuilder;

        private IWebHost webHost;

        public StatelessWebService(StatelessServiceContext serviceContext, string endpointName, IWebHostBuilder webHostBuilder = null)
                : base(serviceContext)
        { 
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException(nameof(endpointName));
            }

            this.endpointName = endpointName;
            this.webHostBuilder = webHostBuilder ?? new WebHostBuilder();
        }

        #region ICommunicationListener

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
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint(this.endpointName);
            string serverUrl = $"{endpoint.Protocol}://{FabricRuntime.GetNodeContext().IPAddressOrFQDN}:{endpoint.Port}/webservicepoc";

            this.webHost = this.webHostBuilder
                .UseUrls(serverUrl)
                .Build();

            this.webHost.Start();

            return Task.FromResult(serverUrl);
        }

        #endregion ICommunicationListener

        #region StatelessService

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(_ => this) };
        }

        #endregion StatelessService
    }
}
