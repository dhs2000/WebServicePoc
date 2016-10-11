using Microsoft.Extensions.Configuration;

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