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
    public class GymClientDetailViewModel : DetailViewModelBase, IGymClientDetailViewModel
    {
        private IGymClientRepository _gymClientRepository;
        private IMessageDialogService _messageDialogService;
        private GymClientWrapper _client;
        private bool _hasChanges;

        public GymClientDetailViewModel(IGymClientRepository gymClientRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
            :base(eventAggregator, messageDialogService)
        {
            _gymClientRepository = gymClientRepository;
            _messageDialogService = messageDialogService;

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

        public override async Task LoadAsync(int clientId)
        {
            var gymClient = clientId > 0
                ? await _gymClientRepository.GetByIdAsync(clientId)
                : CreateNewClient();

            Id = clientId;

            InitializeGymClient(gymClient);
        }

        protected override async void OnSaveExecute()
        {
            await _gymClientRepository.SaveAsync();
            HasChanges = _gymClientRepository.HasChanges();
            Id = Client.Id;
            RaiseDetailSavedEvent(Client.Id, $"{Client.FirstName} {Client.LastName}");
        }

        protected override bool OnSaveCanExecute()
        {
            return Client != null && !Client.HasErrors && HasChanges;
        }

        private GymClient CreateNewClient()
        {
            var gymClient = new GymClient();
            _gymClientRepository.Add(gymClient);
            return gymClient;
        }

        protected override async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete client {Client.FirstName} {Client.LastName}?", "Warning");

            if (result == MessageDialogResult.OK)
            {
                _gymClientRepository.Remove(Client.Model);
                await _gymClientRepository.SaveAsync();
                RaiseDetailDeletedEvent(Client.Id);
            }
        }

        private void InitializeGymClient(GymClient gymClient)
        {
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
                if (e.PropertyName == nameof(Client.FirstName)
                 || e.PropertyName == nameof(Client.LastName))
                {
                    SetTitle();
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Client.Id == 0) Client.FirstName = "";
            
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Client.FirstName} {Client.LastName}";
        }
    }
}
