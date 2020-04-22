using GymRegistrator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data
{
    public interface IGymClientLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetGymClientAsync();
    }
}