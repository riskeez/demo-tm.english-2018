using System.Threading.Tasks;
using Tm.Mobile.Core;
using Tm.Mobile.Core.Abstract;

namespace Tm.English.UI
{
    public abstract class PageViewModel : ViewModelBase
    {
        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }

        string busyMessage;
        public string BusyMessage
        {
            get => busyMessage;
            set => Set(ref busyMessage, value);
        }

        string title;
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public string IconSource { get; set; }

        protected INavigationService NavigationService => this.GetService<INavigationService>();

        protected IDialogService DialogService => this.GetService<IDialogService>();

        public virtual Task OnAppearing(bool firstAppearing) => Task.FromResult(0);

        public virtual Task OnDisappearing() => Task.FromResult(0);
    }
}
