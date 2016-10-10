using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebServicePoc;

namespace WebServicePoc.ServiceFabric
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessWebService : Microsoft.ServiceFabric.Services.Runtime.StatelessService, ICommunicationListener
    {
        private readonly string _endpointName;

        private IWebHost _webHost;

        public StatelessWebService(StatelessServiceContext serviceContext, string endpointName)
                : base(serviceContext)
            {
            _endpointName = endpointName;
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
            _webHost?.Dispose();
        }

        Task ICommunicationListener.CloseAsync(CancellationToken cancellationToken)
        {
            _webHost?.Dispose();

            return Task.FromResult(true);
        }

        Task<string> ICommunicationListener.OpenAsync(CancellationToken cancellationToken)
        {
            var endpoint = FabricRuntime.GetActivationContext().GetEndpoint(_endpointName);

            string serverUrl = $"{endpoint.Protocol}://{FabricRuntime.GetNodeContext().IPAddressOrFQDN}:{endpoint.Port}";

            _webHost = new WebHostBuilder().UseKestrel()
                                           .UseContentRoot(Directory.GetCurrentDirectory())
                                           .UseStartup<Startup>()
                                           .UseUrls(serverUrl)
                                           .Build();

            _webHost.Start();

            return Task.FromResult(serverUrl);
        }

        #endregion ICommunicationListener
    }
}
