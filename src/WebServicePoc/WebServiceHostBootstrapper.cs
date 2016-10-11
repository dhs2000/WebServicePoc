using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebServicePoc.ServiceFabric;

namespace WebServicePoc
{
    internal sealed class WebServiceHostBootstrapper
    {
        private IConfigurationRoot _configurationData;

        private IWebHostBuilder _webHostBuilder;

        public WebServiceHostBootstrapper(IConfigurationRoot configurationData)
        {
            if (configurationData == null)
            {
                throw new ArgumentNullException(nameof(configurationData));
            }

            this._configurationData = configurationData;
        }

        public void Run()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var webHostBuilder = new WebHostBuilder()
                .UseWebListener()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            if (this._configurationData["host"] == "service-fabric-host")
            {
                ServiceRuntime.RegisterServiceAsync("WebServicePocType", context => {
                    var statelessService = new StatelessWebService(context, "ServiceEndpoint", webHostBuilder);
                    return statelessService;
                }).GetAwaiter().GetResult();
                Thread.Sleep(Timeout.Infinite);
            }
            else
            {
                string serverUrl = this._configurationData["WebServiceUrl"];
                webHostBuilder
                    .UseUrls(serverUrl)
                    .Build();
                using (var webHost = this._webHostBuilder.Build())
                {
                    webHost.Start();
                    Thread.Sleep(Timeout.Infinite);
                }
            }
        }
    }
}
