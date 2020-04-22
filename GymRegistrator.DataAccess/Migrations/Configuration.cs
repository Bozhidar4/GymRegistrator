using GymRegistrator.Model;
using System.Data.Entity.Migrations;

namespace GymRegistrator.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GymRegistrator.DataAccess.GymRegistratorDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GymRegistrator.DataAccess.GymRegistratorDbContext context)
        {
            context.GymClients.AddOrUpdate(
                gc => gc.FirstName,
                new GymClient { FirstName = "Bozhidar", LastName = "Rangelov" },
                new GymClient { FirstName = "Michael", LastName = "Carter" },
                new GymClient { FirstName = "John", LastName = "Smith" },
                new GymClient { FirstName = "Ivan", LastName = "Ivanov" }
            );
        }
    }
}
