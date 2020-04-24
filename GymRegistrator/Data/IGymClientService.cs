using GymRegistrator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data
{
    public interface IGymClientService
    {
        Task<GymClient> GetByIdAsync(int clientId);

        Task SaveAsync(GymClient client);
    }
}