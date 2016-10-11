using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.AspNetCore.Hosting;
using System;

namespace WebServicePoc.ServiceFabric
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessWebService : Microsoft.ServiceFabric.Services.Runtime.StatelessService, ICommunicationListener
    {
        private readonly string _endpointName;

        private readonly IWebHostBuilder _webHostBuilder;

        private IWebHost _webHost;

        public StatelessWebService(StatelessServiceContext serviceContext, string endpointName, IWebHostBuilder webHostBuilder = null)
                : base(serviceContext)
        { 
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException(nameof(endpointName));
            }

            this._endpointName = endpointName;
            this._webHostBuilder = webHostBuilder ?? new WebHostBuilder();
        }

        #region StatelessService

        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[] { new ServiceInstanceListener(_ => this) };
        }

        #endregion StatelessService

        #region ICommunicationListener

        void ICommunicationListener.Abort()
        {
            this._webHost?.Dispose();
        }

        Task ICommunicationListener.CloseAsync(CancellationToken cancellationToken)
        {
            this._webHost?.Dispose();

            return Task.FromResult(true);
        }

        Task<string> ICommunicationListener.OpenAsync(CancellationToken cancellationToken)
        {
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint(this._endpointName);
            string serverUrl = $"{endpoint.Protocol}://{FabricRuntime.GetNodeContext().IPAddressOrFQDN}:{endpoint.Port}/webservicepoc";

            this._webHost = this._webHostBuilder
                .UseUrls(serverUrl)
                .Build();

            _webHost.Start();

            return Task.FromResult(serverUrl);
        }

        #endregion ICommunicationListener
    }
}
