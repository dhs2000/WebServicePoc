using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Runtime;
using WebServicePoc.ServiceFabric;

namespace WebServicePoc
{
    internal sealed class WebServiceHostBootstrapper
    {
        private IConfigurationRoot configurationData;

        public WebServiceHostBootstrapper(IConfigurationRoot configurationData)
        {
            if (configurationData == null)
            {
                throw new ArgumentNullException(nameof(configurationData));
            }

            this.configurationData = configurationData;
        }

        public void Run()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseWebListener()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();

            if (this.configurationData["host"] == "service-fabric-host")
            {
                ServiceRuntime.RegisterServiceAsync(
                    "WebServicePocType", 
                    context => {
                        var statelessService = new StatelessWebService(context, "ServiceEndpoint", webHostBuilder);
                        return statelessService;
                }).GetAwaiter().GetResult();
                Thread.Sleep(Timeout.Infinite);
            }
            else
            {
                string serverUrl = this.configurationData["WebServiceUrl"];
                webHostBuilder
                    .UseUrls(serverUrl);

                using (var webHost = webHostBuilder.Build())
                {
                    webHost.Start();
                    Thread.Sleep(Timeout.Infinite);
                }
            }
        }
    }
}
