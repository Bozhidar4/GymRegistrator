using GymRegistrator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data.Repositories
{
    public interface IGymClientRepository
    {
        Task<GymClient> GetByIdAsync(int clientId);

        Task SaveAsync();

        bool HasChanges();

        void Add(GymClient gymClient);
    }
}