using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServicePoc
{
    public class ProgramOptions
    {
        [Option(DefaultValue = "self-host", HelpText = "Sets hosting desire - self-host or service-fabric-host.", Required = false)]
        public string Host { get; set; }

        [Option(DefaultValue = "http", HelpText = "The target protocol - Options [http] or [https]")]
        public string Protocol { get; set; }

        [Option(DefaultValue = "localhost", HelpText = "The target IP Address or Uri.")]
        public string IpAddressOrFQDN { get; set; }

        [Option(DefaultValue = "8585", HelpText = "The target port where app will run")]
        public string Port { get; set; }
    }
}
