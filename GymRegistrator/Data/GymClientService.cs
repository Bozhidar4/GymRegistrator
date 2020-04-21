using GymRegistrator.Model;
using System.Collections.Generic;

namespace GymRegistrator.UI.Data
{
    public class GymClientService : IGymClientService
    {
        public IEnumerable<GymClient> GetAll()
        {
            yield return new GymClient { FirstName = "Bozhidar", LastName = "Rangelov" };
            yield return new GymClient { FirstName = "Michael", LastName = "Carter" };
            yield return new GymClient { FirstName = "John", LastName = "Smith" };
            yield return new GymClient { FirstName = "Ivan", LastName = "Ivanov" };
        }
    }
}
