using GymRegistrator.Data;
using GymRegistrator.Model;
using System.Collections.ObjectModel;

namespace GymRegistrator.ViewModel
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

        public void Load()
        {
            var gymClients = _gymClientService.GetAll();
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
