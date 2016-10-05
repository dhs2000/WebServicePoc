using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;

namespace DataAccess.Tests
{
    public class NLogLoggerFactory : ILoggerFactory
    {
        public IInternalLogger LoggerFor(string keyName)
        {
            return new NLogLogger(keyName);
        }

        public IInternalLogger LoggerFor(System.Type type)
        {
            return new NLogLogger(type.FullName);
        }
    }
}
