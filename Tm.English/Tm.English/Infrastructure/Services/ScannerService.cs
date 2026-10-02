using Plugin.Vibrate;
using System;
using Tm.English.Infrastructure.Abstract;
using Tm.English.UI;
using Tm.Mobile.Core.Abstract;

namespace Tm.English.Services
{
    public class ScannerService : IScannerService
    {
        readonly INavigationService navService;
        public ScannerService(INavigationService navigation)
        {
            navService = navigation;
        }

        public event EventHandler<string> OnScanResult;

        public void StartScan()
        {
            var scanVM = ViewModelLocator.GetInstance<ScannerPageModel>();

            navService.NavigateTo(nameof(ScannerPage), scanVM);

            scanVM.OnScanResult += RaiseScanResult;
        }

        private void RaiseScanResult(object sender, string e)
        {
            OnScanResult?.Invoke(sender, e);
        }
    }
}
