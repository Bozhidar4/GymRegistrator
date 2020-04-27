using GymRegistrator.DataAccess;
using GymRegistrator.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data.Repositories
{
    public class GymClientRepository : IGymClientRepository
    {
        private GymRegistratorDbContext _context;

        public GymClientRepository(GymRegistratorDbContext context)
        {
            _context = context;
        }

        public void Add(GymClient gymClient)
        {
            _context.GymClients.Add(gymClient);
        }

        public async Task<GymClient> GetByIdAsync(int clientId)
        {
            return await _context.GymClients.SingleAsync(c => c.Id == clientId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(GymClient model)
        {
            _context.GymClients.Remove(model);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
