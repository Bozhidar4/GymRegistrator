using GymRegistrator.Model;
using GymRegistrator.UI.Data.Repositories;
using GymRegistrator.UI.Event;
using GymRegistrator.UI.View.Services;
using GymRegistrator.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymRegistrator.UI.ViewModel
{
    public class GymClientDetailViewModel : ViewModelBase, IGymClientDetailViewModel
    {
        private IGymClientRepository _gymClientRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private GymClientWrapper _client;
        private bool _hasChanges;

        public GymClientDetailViewModel(IGymClientRepository gymClientRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _gymClientRepository = gymClientRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
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
        public ICommand DeleteCommand { get; }

        public async Task LoadAsync(int? clientId)
        {
            var gymClient = clientId.HasValue
                ? await _gymClientRepository.GetByIdAsync(clientId.Value)
                : CreateNewClient();

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

            if (gymClient.Id == 0) gymClient.FirstName = "";
        }

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

        private GymClient CreateNewClient()
        {
            var gymClient = new GymClient();
            _gymClientRepository.Add(gymClient);
            return gymClient;
        }

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete client {Client.FirstName} {Client.LastName}?", "Warning");

            if (result == MessageDialogResult.OK)
            {
                _gymClientRepository.Remove(Client.Model);
                await _gymClientRepository.SaveAsync();

                _eventAggregator.GetEvent<AfterClientDeletedEvent>().Publish(Client.Id);
            }
        }
    }
}
