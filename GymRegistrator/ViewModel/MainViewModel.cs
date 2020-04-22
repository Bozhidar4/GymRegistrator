using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get; }
        public IGymClientDetailViewModel ClientDetailViewModel { get; }


        public MainViewModel(INavigationViewModel navigationViewModel, IGymClientDetailViewModel clientDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            ClientDetailViewModel = clientDetailViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
