using GymRegistrator.UI.Event;
using GymRegistrator.UI.View.Services;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymRegistrator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Func<IGymClientDetailViewModel> _gymClientDetailViewModelCreator;
        private IMessageDialogService _messageDialogService;
        private IDetailViewModel _detailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
            Func<IGymClientDetailViewModel> gymClientDetailViewModelCreator, 
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _eventAggregator = eventAggregator;
            _gymClientDetailViewModelCreator = gymClientDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }
        
        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            private set 
            {
                _detailViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewDetailCommand { get; }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel != null && DetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("You have made changes. Do you really want to navigate away?", "Warning");

                if (result == MessageDialogResult.Cancel) return;
            }

            switch (args.ViewModelName)
            {
                case nameof(GymClientDetailViewModel):
                    DetailViewModel = _gymClientDetailViewModelCreator();
                    break;
            }

            await DetailViewModel.LoadAsync(args.Id);
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { ViewModelName = viewModelType.Name});
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            DetailViewModel = null;
        }
    }
}
