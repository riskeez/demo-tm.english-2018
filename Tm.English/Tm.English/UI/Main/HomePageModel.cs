using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Tm.English.Data;
using Tm.English.Data.Domain;
using Tm.English.Infrastructure.Abstract;
using Tm.Mobile.Core;
using Tm.Mobile.Core.Security;
using Xamarin.Forms;

namespace Tm.English.UI
{
    public class HomePageModel : PageViewModel
    {
        readonly IDataRepository repository;
        readonly IScannerService scannerService;
        readonly ICipherService cipherService;

        public HomePageModel(IDataRepository repository, IScannerService scannerService, ICipherService cipherService)
        {
            Title = "TM English";

            this.repository = repository;
            this.scannerService = scannerService;
            this.cipherService = cipherService;

            Messenger.Default.Register<NotificationMessage>(this, NotificationReceived);

            RefreshData();
        }

        private void NotificationReceived(NotificationMessage msg)
        {
            switch (msg.Notification)
            {
                case Constants.MessageKeys.NeedRefresh:
                    RefreshData();
                    break;
                case Constants.MessageKeys.NeedBackup:
                    repository.BackupData();
                    break;
            }
        }

        public async void RefreshData()
        {
            await RefreshChart();
            await LoadHistoryList();
        }

        bool hasHistoryData;
        public bool HasHistoryData
        {
            get => hasHistoryData;
            set => Set(ref hasHistoryData, value);
        }

        Chart chart;
        public Chart Chart
        {
            get => chart ?? (chart = new DonutChart() { Entries = new Microcharts.Entry[0] });
            set => Set(ref chart, value);
        }

        private async Task RefreshChart()
        {
            Planned = await repository.GetPlannedForPastMonths() + Settings.LessonPerMonth;
            Passed = await repository.GetCodesCount();
            Rest = Planned - Passed;

            var entries = new Microcharts.Entry[]
            {
                new Microcharts.Entry(Passed) { Color = SKColor.Parse(Constants.Colors.BlueHEX) },
                new Microcharts.Entry(Rest) { Color = SKColor.Parse(Constants.Colors.OrangeHEX) },
            };

            Chart = new DonutChart()
            {
                Entries = entries,
                HoleRadius = 0.4f,
            };
        }

        int planned;
        public int Planned
        {
            get => planned;
            set => Set(ref planned, value);
        }

        int passed;
        public int Passed
        {
            get => passed;
            set => Set(ref passed, value);
        }

        int rest;
        public int Rest
        {
            get => rest;
            set => Set(ref rest, value);
        }

        RelayCommand scanQRCommand;
        public RelayCommand ScanQRCommand
        {
            get => scanQRCommand ?? (scanQRCommand = new RelayCommand(async () =>
            {
                if (string.IsNullOrEmpty(Settings.ScanKey))
                {
                    await DialogService.ShowMessage("Please set private key in 'Settings'", Title);
                    return;
                }

                scannerService.StartScan();
                scannerService.OnScanResult += OnScanResult;
            }));
        }

        private void OnScanResult(object sender, string scanResult)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            scannerService.OnScanResult -= OnScanResult;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await ScanResultProcessing(scanResult);
            });

            IsBusy = false;
        }

        bool isProcessing;
        private async Task ScanResultProcessing(string scanResult)
        {
            if (isProcessing)
                return;

            isProcessing = true;

            await NavigationService.GoBack();

            QRCode qr = null;
            try
            {
                var decodedData = cipherService.Decrypt(scanResult, Settings.ScanKey);
                qr = QRCode.DeserialzeFromJSON(decodedData);
            }
            catch(Exception exc)
            {
                Debug.Write(exc.Message);
            }

            string msg;

            if (qr != null)
            {
                bool success = await repository.AddCode(qr.Key, qr.No);
                if (success)
                {
                    msg = "Successfully added";

                    Messenger.Default.Send(new NotificationMessage(Constants.MessageKeys.NeedRefresh));
                    Messenger.Default.Send(new NotificationMessage(Constants.MessageKeys.NeedBackup));
                }
                else
                {
                    msg = "Code already registered!";
                }
            }
            else
            {
                msg = "Unreadable QR Code";
            }

            await DialogService.ShowMessage(msg, Title);

            isProcessing = false;
        }

        RelayCommand historyCommand;
        public RelayCommand HistoryCommand
        {
            get => historyCommand ?? (historyCommand = new RelayCommand(async () =>
            {
                await NavigationService.NavigateTo<HistoryPageModel>(nameof(HistoryPage));
            }));
        }

        RangeObservableCollection<Code> historyList;
        public RangeObservableCollection<Code> HistoryList
        {
            get => historyList ?? (historyList = new RangeObservableCollection<Code>());
        }

        RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
           get => settingsCommand ?? (settingsCommand = new RelayCommand(async () =>
           {
               await NavigationService.NavigateTo<SettingsPageModel>(nameof(SettingsPage));
           }));
        }

        private async Task LoadHistoryList()
        {
            HistoryList.Clear();

            var records = await repository.GetCodes(-1, 8);

            HasHistoryData = records.Count > 0;

            if (hasHistoryData)
            {
                HistoryList.AddRange(records);
            }
        }
    }
}
