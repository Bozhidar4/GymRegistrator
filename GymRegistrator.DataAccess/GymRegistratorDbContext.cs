using GymRegistrator.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GymRegistrator.DataAccess
{
    public class GymRegistratorDbContext : DbContext
    {
        public GymRegistratorDbContext():base("GymRegistratorDb")
        {

        }
        public DbSet<GymClient> GymClients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
