using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationViewModel NavigationViewModel { get; }
        public IGymClientDetailViewModel GymClientDetailViewModel { get; }


        public MainViewModel(INavigationViewModel navigationViewModel, IGymClientDetailViewModel gymClientDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            GymClientDetailViewModel = gymClientDetailViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}
