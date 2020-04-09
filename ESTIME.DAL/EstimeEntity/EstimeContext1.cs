using Microsoft.EntityFrameworkCore;

namespace ESTIME.DAL.EstimeEntity
{
    public partial class EstimeContext : DbContext
    {
        private readonly string dbConnectString;

        public EstimeContext(string connString)
            :base()
        {
            dbConnectString = connString;
        }
        public EstimeContext(DbContextOptions<EstimeContext> options, string connString)
            : base(options)
        {
            dbConnectString = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(dbConnectString, providerOptions => providerOptions.CommandTimeout(60))
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
    }
}
