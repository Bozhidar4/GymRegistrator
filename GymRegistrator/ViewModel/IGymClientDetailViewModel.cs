using System.Threading.Tasks;

namespace GymRegistrator.UI.ViewModel
{
    public interface IGymClientDetailViewModel
    {
        Task LoadAsync(int? clientId);
        bool HasChanges { get; }
    }
}