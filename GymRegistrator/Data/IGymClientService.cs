using GymRegistrator.Model;
using System.Collections.Generic;

namespace GymRegistrator.Data
{
    public interface IGymClientService
    {
        IEnumerable<GymClient> GetAll();
    }
}