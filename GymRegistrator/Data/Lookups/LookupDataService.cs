using GymRegistrator.DataAccess;
using GymRegistrator.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data.Lookups
{
    public class LookupDataService : IGymClientLookupDataService
    {
        private Func<GymRegistratorDbContext> _contextCreator;

        public LookupDataService(Func<GymRegistratorDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetGymClientAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.GymClients.AsNoTracking()
                    .Select(c => new LookupItem
                    {
                        Id = c.Id,
                        DisplayMember = c.FirstName + " " + c.LastName
                    })
                    .ToListAsync();
            }
        }
    }
}
