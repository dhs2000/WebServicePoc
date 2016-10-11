using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Threading;
using WebServicePoc.ServiceFabric;
using Microsoft.Extensions.Configuration;
using Autofac;
using WebServicePoc.Infrastructure;

namespace WebServicePoc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configurationData = configurationBuilder
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();

            var webServiceHostBootStrap = new WebServiceHostBootstrapper(configurationData);
            webServiceHostBootStrap.Run();
        }
    }
}