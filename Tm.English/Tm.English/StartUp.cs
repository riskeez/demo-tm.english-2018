using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Tm.English.Data;
using Tm.English.Infrastructure.Abstract;
using Tm.English.Services;
using Tm.English.UI;
using Tm.Mobile.Core.Abstract;
using Tm.Mobile.Core.Security;
using Tm.Mobile.CoreX.Abstract;

namespace Tm.English
{
    public static class StartUp
    {
        public static void RegisterServices()
        {
            SimpleIoc.Default.Reset();
            Messenger.Reset();
            
            SimpleIoc.Default.Register<IXFileHelper, FileService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<INavigationService>(() => CreateNavigationService());
            SimpleIoc.Default.Register<ICipherService, AesCipherService>();
            SimpleIoc.Default.Register<IBackupService, BackupLocalService>();

            SimpleIoc.Default.Register<IScannerService, ScannerService>();
            SimpleIoc.Default.Register<IDataRepository, SQLiteRepository>(true);
        }

        private static INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(nameof(ScannerPage), typeof(ScannerPage));

            navigationService.Configure(nameof(HomePage), typeof(HomePage));
            navigationService.Configure(nameof(HistoryPage), typeof(HistoryPage));
            navigationService.Configure(nameof(SettingsPage), typeof(SettingsPage));

            return navigationService;
        }
    }
}
