using GalaSoft.MvvmLight.Command;
using Plugin.Vibrate;
using System;
using System.Collections.Generic;

namespace Tm.English.UI
{
    public class ScannerPageModel : PageViewModel
    {
        public event EventHandler<string> OnScanResult;

        public ScannerPageModel()
        {
            Title = "Scanner";
        }

        public ZXing.Result ScanResult { get; set; }

        private ZXing.Mobile.MobileBarcodeScanningOptions scanOptions;
        public ZXing.Mobile.MobileBarcodeScanningOptions ScanOptions
        {
            get => scanOptions ?? (scanOptions = new ZXing.Mobile.MobileBarcodeScanningOptions()
            {
                AutoRotate = false,
                TryHarder = true,
                TryInverted = true,
                PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE }
            });
        }

        bool isAnalyzing;
        public bool IsAnalyzing
        {
            get => isAnalyzing;
            set => Set(ref isAnalyzing, value);
        }

        bool isScanning;
        public bool IsScanning
        {
            get => isScanning;
            set => Set(ref isScanning, value);
        }

        RelayCommand scanResultCommand;
        public RelayCommand ScanResultCommand
        {
            get => scanResultCommand ?? (scanResultCommand = new RelayCommand(ScanResultProcessing));
        }

        private void ScanResultProcessing()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsAnalyzing = false;

            if (!string.IsNullOrEmpty(ScanResult.Text))
            {
                OnScannedResult(ScanResult.Text);
            }

            IsBusy = false;
        }

        protected virtual void OnScannedResult(string scannedData)
        {
            OnScanResult?.Invoke(this, ScanResult.Text);
            CrossVibrate.Current.Vibration();
        }
    }
}
