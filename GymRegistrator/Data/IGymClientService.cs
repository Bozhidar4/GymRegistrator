using GymRegistrator.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymRegistrator.UI.Data
{
    public interface IGymClientService
    {
        Task<IList<GymClient>> GetAllAsync();
    }
}