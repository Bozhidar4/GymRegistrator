using GymRegistrator.UI.Data;
using GymRegistrator.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IGymClientLookupDataService _clientLookupService;
        private IEventAggregator _eventAggregator;
        private NavigationItemViewModel _selectedClient;

        public NavigationViewModel(IGymClientLookupDataService clientLookupService, IEventAggregator eventAggregator)
        {
            _clientLookupService = clientLookupService;
            _eventAggregator = eventAggregator;
            GymClients = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterClientSavedEvent>().Subscribe(AfterClientSaved);
        }

        private void AfterClientSaved(AfterClientSavedEventArgs obj)
        {
            var lookupItem = GymClients.Single(c => c.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        
        public NavigationItemViewModel SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged();

                if (_selectedClient != null)
                {
                    _eventAggregator.GetEvent<OpenClientDetailViewEvent>().Publish(_selectedClient.Id);
                }
            }
        }

        public ObservableCollection<NavigationItemViewModel> GymClients { get; }

        public async Task LoadAsync()
        {
            var lookup = await _clientLookupService.GetGymClientAsync();
            GymClients.Clear();
            foreach (var item in lookup)
            {
                GymClients.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }
    }
}
