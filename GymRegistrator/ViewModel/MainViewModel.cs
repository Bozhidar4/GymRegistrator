using GymRegistrator.UI.Event;
using GymRegistrator.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IGymClientDetailViewModel> _gymClientDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;
        private IGymClientDetailViewModel _gymClientDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IGymClientDetailViewModel> gymClientDetailViewModelCreator, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _gymClientDetailViewModelCreator = gymClientDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenClientDetailViewEvent>().Subscribe(OnOpenClientDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }
        
        public IGymClientDetailViewModel GymClientDetailViewModel
        {
            get { return _gymClientDetailViewModel; }
            private set 
            {
                _gymClientDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenClientDetailView(int clientId)
        {
            if (GymClientDetailViewModel != null && GymClientDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You have made changes. Do you really want to navigate away?", "Warning");

                if (result == MessageDialogResult.Cancel) return;
            }
            GymClientDetailViewModel = _gymClientDetailViewModelCreator();
            await GymClientDetailViewModel.LoadAsync(clientId);
        }
    }
}
