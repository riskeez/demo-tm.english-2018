using System.Threading.Tasks;

namespace Tm.Mobile.Core.Abstract
{
    public interface INavigationService
    {
        string CurrentPageKey { get; }

        Task GoBack();

        /// <summary>
        /// Navigate to page with already created viewModel
        /// </summary>
        /// <param name="forceNavigate">Navigate even if pageKey = CurrentPageKey</param>
        Task NavigateTo(string pageKey, ViewModelBase viewModel, bool forceNavigate = false);

        /// <summary>
        /// Navigate to determined page and automatically create view model
        /// </summary>
        /// <param name="forceNavigate">Navigate even if pageKey = CurrentPageKey</param>
        Task NavigateTo<TViewModel>(string pageKey, bool forceNavigate = false) where TViewModel : ViewModelBase;
    }
}
