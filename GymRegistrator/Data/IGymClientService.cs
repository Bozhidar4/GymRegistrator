using GymRegistrator.Model;
using System.Collections.Generic;

namespace GymRegistrator.UI.Data
{
    public interface IGymClientService
    {
        IEnumerable<GymClient> GetAll();
    }
}