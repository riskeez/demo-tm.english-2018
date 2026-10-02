using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using Tm.English.Data;
using Tm.English.Infrastructure.Abstract;
using Xamarin.Forms;

namespace Tm.English.UI
{
    public class SettingsPageModel : PageViewModel
    {
        readonly IScannerService scannerService;
        readonly IDataRepository repository;

        public SettingsPageModel(IScannerService scannerService, IDataRepository repository)
        {
            Title = "Settings";
            this.scannerService = scannerService;
            this.repository = repository;

            InitFromSettings();
        }

        private async void InitFromSettings()
        {
            LessonsPerMonth = Settings.LessonPerMonth;
            PlannedPastMonths = await repository.GetPlannedForPastMonths();
        }

        IEnumerable<ToolbarItem> toolbarList;
        public IEnumerable<ToolbarItem> ToolbarList
        {
            get => toolbarList ?? (toolbarList = RegularButtons);
            set => Set(ref toolbarList, value);
        }

        ToolbarItem[] regularButtons;
        ToolbarItem[] RegularButtons
        {
            get => regularButtons ?? (regularButtons = new ToolbarItem[]
            {
                new ToolbarItem() { Icon = "ic_edit.png", Command = EditCommand }
            });
        }

        ToolbarItem[] editButtons;
        ToolbarItem[] EditButtons
        {
            get => editButtons ?? (editButtons = new ToolbarItem[]
            {
                new ToolbarItem() { Icon = "ic_close.png", Command = CancelCommand },
                new ToolbarItem() { Icon = "ic_check.png", Command = SaveCommand  }
            });
        }

        private void SetToolbar(bool isEdit)
        {
            ToolbarList = isEdit ? EditButtons : RegularButtons;
        }

        RelayCommand clearStorageCommand;
        public RelayCommand ClearStorageCommand
        {
            get => clearStorageCommand ?? (clearStorageCommand = new RelayCommand(async () =>
            {
                var result = await DialogService.ShowYesNoQuestion("Do you want to clear storage data?", Title);
                if (result)
                {
                    await repository.ClearAllDataAsync();

                    SendRefreshSignal();

                    await DialogService.ShowMessage("Successfully cleared.", Title);
                }
            }));
        }

        bool isEditMode;
        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                if (isEditMode != value)
                {
                    Set(ref isEditMode, value);
                    SetToolbar(isEditMode);
                }
            }
        }

        RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get => editCommand ?? (editCommand = new RelayCommand(() =>
            {
                IsEditMode = true;
            }));
        }

        RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get => cancelCommand ?? (cancelCommand = new RelayCommand(() =>
            {
                IsEditMode = false;

                InitFromSettings();
            }));
        }

        RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get => saveCommand ?? (saveCommand = new RelayCommand(async () =>
            {
                Settings.LessonPerMonth = LessonsPerMonth;
                await repository.SetPlannedForPastMonths(PlannedPastMonths);

                SendRefreshSignal();
                await NavigationService.GoBack();
            }));
        }
        
        private void SendRefreshSignal()
        {
            Messenger.Default.Send(new NotificationMessage(Constants.MessageKeys.NeedRefresh));
        }

        int lessonsPerMonth;
        public int LessonsPerMonth
        {
            get => lessonsPerMonth;
            set => Set(ref lessonsPerMonth, value);
        }

        int plannedPastMonths;
        public int PlannedPastMonths
        {
            get => plannedPastMonths;
            set => Set(ref plannedPastMonths, value);
        }

        public string LastBackup
        {
            get
            {
                var lastBkp = Settings.LastBackup;
                if (lastBkp == DateTime.MinValue)
                    return "Never";
                return lastBkp.ToLocalTime().ToString();
            }
        }

        RelayCommand backupCommand;
        public RelayCommand BackupCommand
        {
            get => backupCommand ?? (backupCommand = new RelayCommand(async() =>
            {
                if (await DialogService.ShowYesNoQuestion("Current data will be backed up.\nContinue?", Title))
                {
                    var success = repository.BackupData();
                    if (success)
                    {
                        OnPropertyChanged(nameof(LastBackup));
                        await DialogService.ShowMessage("Backup created successfully", Title);
                        return;
                    }
                    await DialogService.ShowMessage("Backup error", Title);
                }
            }));
        }

        RelayCommand restoreBackupCommand;
        public RelayCommand RestoreBackupCommand
        {
            get => restoreBackupCommand ?? (restoreBackupCommand = new RelayCommand(async () =>
            {
                if (await DialogService.ShowYesNoQuestion("Do you really want to restore data from backup?", Title))
                {
                    var success = repository.RestoreBackup();
                    if (success)
                    {
                        await DialogService.ShowMessage("Backup restored successfully", Title);

                        SendRefreshSignal();
                        await NavigationService.GoBack();
                        return;
                    }
                    await DialogService.ShowMessage("Backup can't be restored", Title);
                }
            }));
        }

        RelayCommand setPrivateKeyCommand;
        public RelayCommand SetPrivateKeyCommand
        {
            get => setPrivateKeyCommand ?? (setPrivateKeyCommand = new RelayCommand(() =>
            {
                scannerService.OnScanResult += OnScanResult;
                scannerService.StartScan();
            }));
        }

        private void OnScanResult(object sender, string scanResult)
        {
            scannerService.OnScanResult -= OnScanResult;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavigationService.GoBack();

                Settings.ScanKey = scanResult;

                await DialogService.ShowMessage("Private key was set successfully", Title);
            });
        }
    }
}
