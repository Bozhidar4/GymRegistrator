using GymRegistrator.Model;
using GymRegistrator.UI.Data;
using GymRegistrator.UI.Event;
using GymRegistrator.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymRegistrator.UI.ViewModel
{
    public class GymClientDetailViewModel : ViewModelBase, IGymClientDetailViewModel
    {
        private IGymClientService _clientService;
        private IEventAggregator _eventAggregator;
        private GymClientWrapper _client;

        public GymClientDetailViewModel(IGymClientService clientService, IEventAggregator eventAggregator)
        {
            _clientService = clientService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenClientDetailViewEvent>().Subscribe(OnOpenClientDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public GymClientWrapper Client
        {
            get { return _client; }
            private set
            {
                _client = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _clientService.SaveAsync(Client.Model);

            _eventAggregator.GetEvent<AfterClientSavedEvent>().Publish(
                new AfterClientSavedEventArgs
                {
                    Id = Client.Id,
                    DisplayMember = $"{Client.FirstName} {Client.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Client != null && !Client.HasErrors;
        }

        public async Task LoadAsync(int clientId)
        {
            var gymClient = await _clientService.GetByIdAsync(clientId);

            Client = new GymClientWrapper(gymClient);

            Client.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Client.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private async void OnOpenClientDetailView(int clientId)
        {
            await LoadAsync(clientId);
        }
    }
}
