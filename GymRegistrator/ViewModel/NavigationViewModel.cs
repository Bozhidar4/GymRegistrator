using GymRegistrator.Model;
using GymRegistrator.UI.Data;
using GymRegistrator.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
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
            GymClients = new ObservableCollection<LookupItem>();
        }

        private LookupItem _selectedClient;
        public LookupItem SelectedClient
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

        public ObservableCollection<LookupItem> GymClients { get; }

        public async Task LoadAsync()
        {
            var lookup = await _clientLookupService.GetGymClientAsync();
            GymClients.Clear();
            foreach (var item in lookup)
            {
                GymClients.Add(item);
            }
        }
    }
}
