using GymRegistrator.UI.Data;
using GymRegistrator.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IGymClientService _gymClientService;
        private GymClient _selectedGymClient;

        public MainViewModel(IGymClientService gymClientService)
        {
            GymClients = new ObservableCollection<GymClient>();
            _gymClientService = gymClientService;
        }

        public async Task LoadAsync()
        {
            var gymClients = await _gymClientService.GetAllAsync();
            GymClients.Clear();
            foreach (var client in gymClients)
            {
                GymClients.Add(client);
            }
        }

        public ObservableCollection<GymClient> GymClients { get; set; }

        public GymClient SelectedGymClient
        {
            get { return _selectedGymClient; }
            set 
            { 
                _selectedGymClient = value;
                OnPropertyChanged();
            }
        }
    }
}
