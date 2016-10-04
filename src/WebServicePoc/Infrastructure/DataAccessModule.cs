using Autofac;

namespace WebServicePoc.Infrastructure
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.Register(i => new DatabaseContext("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebServicePoc.Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")).InstancePerLifetimeScope();
        }
    }
}