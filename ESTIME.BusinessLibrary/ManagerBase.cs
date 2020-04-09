using Microsoft.Extensions.Configuration;

namespace ESTIME.BusinessLibrary
{
    public abstract class ManagerBase
    {
        protected readonly IConfiguration configuration;
        protected readonly string connectionString;

        public ManagerBase(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("EstimeDb");
        }
        public ManagerBase() { }
    }
}
