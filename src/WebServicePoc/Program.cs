using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.ServiceFabric.Services.Runtime;
using System.Threading;
using System.Diagnostics;
using System;
using System.Linq;
using WebServicePoc.ServiceFabric;

namespace WebServicePoc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var argsParser = new CommandLine.Parser();
            var cmdOptions = new ProgramOptions();
            bool argsParseSucceeded = argsParser.ParseArguments(args, cmdOptions);
            if (argsParseSucceeded && cmdOptions.Host == "service-fabric-host")
            {
                ServiceRuntime.RegisterServiceAsync("WebServicePocType", context => new StatelessWebService(context, "ServiceEndpoint")).GetAwaiter().GetResult();
                Thread.Sleep(Timeout.Infinite);
            }
            else
            {
                IWebHost host = new WebHostBuilder().UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            }
        }

        //public static void Main(string[] args)
        //{
        //    IWebHost host =
        //        new WebHostBuilder().UseKestrel()
        //            .UseContentRoot(Directory.GetCurrentDirectory())
        //            .UseIISIntegration()
        //            .UseStartup<Startup>()
        //            .Build();

        //    host.Run();
        //}
    }
}