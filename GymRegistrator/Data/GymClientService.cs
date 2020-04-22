using GymRegistrator.DataAccess;
using GymRegistrator.Model;
using System;
using System.Collections.Generic;
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

        public async Task<IList<GymClient>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.GymClients.AsNoTracking().ToListAsync();
            }
        }
    }
}
