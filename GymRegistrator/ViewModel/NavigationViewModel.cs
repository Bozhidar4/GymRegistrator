using GymRegistrator.UI.Data.Lookups;
using GymRegistrator.UI.Event;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IGymClientLookupDataService _clientLookupService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IGymClientLookupDataService clientLookupService, IEventAggregator eventAggregator)
        {
            _clientLookupService = clientLookupService;
            _eventAggregator = eventAggregator;
            GymClients = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterClientSavedEvent>().Subscribe(AfterClientSaved);
            _eventAggregator.GetEvent<AfterClientDeletedEvent>().Subscribe(AfterClientDeleted);
        }

        public ObservableCollection<NavigationItemViewModel> GymClients { get; }

        public async Task LoadAsync()
        {
            var lookup = await _clientLookupService.GetGymClientAsync();
            GymClients.Clear();
            foreach (var item in lookup)
            {
                GymClients.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        private void AfterClientSaved(AfterClientSavedEventArgs obj)
        {
            var lookupItem = GymClients.SingleOrDefault(c => c.Id == obj.Id);

            if (lookupItem == null)
            {
                GymClients.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = obj.DisplayMember;
            }
        }

        private void AfterClientDeleted(int clientId)
        {
            var client = GymClients.SingleOrDefault(c => c.Id == clientId);

            if (client != null) GymClients.Remove(client);
        }
    }
}
