using GymRegistrator.DataAccess;
using GymRegistrator.Model;

namespace GymRegistrator.UI.Data.Repositories
{

    public class GymClientRepository : GenericRepository<GymClient, GymRegistratorDbContext>, IGymClientRepository
    {
        public GymClientRepository(GymRegistratorDbContext context) : base(context)
        {
        }
    }
}
