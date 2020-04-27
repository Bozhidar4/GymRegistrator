using GymRegistrator.UI.Data.Repositories;
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
        private IGymClientRepository _gymClientRepository;
        private IEventAggregator _eventAggregator;
        private GymClientWrapper _client;
        private bool _hasChanges;

        public GymClientDetailViewModel(IGymClientRepository gymClientRepository, IEventAggregator eventAggregator)
        {
            _gymClientRepository = gymClientRepository;
            _eventAggregator = eventAggregator;

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

        public bool HasChanges
        {
            get { return _hasChanges; }
            set 
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _gymClientRepository.SaveAsync();
            HasChanges = _gymClientRepository.HasChanges();
            _eventAggregator.GetEvent<AfterClientSavedEvent>().Publish(
                new AfterClientSavedEventArgs
                {
                    Id = Client.Id,
                    DisplayMember = $"{Client.FirstName} {Client.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return Client != null && !Client.HasErrors && HasChanges;
        }

        public async Task LoadAsync(int clientId)
        {
            var gymClient = await _gymClientRepository.GetByIdAsync(clientId);

            Client = new GymClientWrapper(gymClient);

            Client.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _gymClientRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Client.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}
