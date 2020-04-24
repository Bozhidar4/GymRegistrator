using GymRegistrator.DataAccess;
using GymRegistrator.Model;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data
{
    public class GymClientService : IGymClientService
    {
        private Func<GymRegistratorDbContext> _contextCreator;

        public GymClientService(Func<GymRegistratorDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<GymClient> GetByIdAsync(int clientId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.GymClients.AsNoTracking().SingleAsync(c => c.Id == clientId);
            }
        }

        public async Task SaveAsync(GymClient client)
        {
            using (var ctx = _contextCreator())
            {
                ctx.GymClients.Attach(client);
                ctx.Entry(client).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
