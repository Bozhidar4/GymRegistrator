﻿using GymRegistrator.Model;
using GymRegistrator.UI.Data;
using GymRegistrator.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymRegistrator.UI.ViewModel
{
    public class GymClientDetailViewModel : ViewModelBase, IGymClientDetailViewModel
    {
        private IGymClientService _clientService;
        private IEventAggregator _eventAggregator;

        public GymClientDetailViewModel(IGymClientService clientService, IEventAggregator eventAggregator)
        {
            _clientService = clientService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenClientDetailViewEvent>().Subscribe(OnOpenClientDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
            await _clientService.SaveAsync(Client);

            _eventAggregator.GetEvent<AfterClientSavedEvent>().Publish(
                new AfterClientSavedEventArgs
                {
                    Id = Client.Id,
                    DisplayMember = $"{Client.FirstName} {Client.LastName}"
                });
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }

        private GymClient _client;

        public GymClient Client
        {
            get { return _client; }
            private set
            {
                _client = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync(int clientId)
        {
            Client = await _clientService.GetByIdAsync(clientId);
        }

        private async void OnOpenClientDetailView(int clientId)
        {
            await LoadAsync(clientId);
        }

        public ICommand SaveCommand { get; }
    }
}
