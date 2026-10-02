using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace Tm.English
{
    public class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string ScanKey
        {
            get => AppSettings.GetValueOrDefault(nameof(ScanKey), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(ScanKey), value);
        }

        public static int LessonPerMonth
        {
            get => AppSettings.GetValueOrDefault(nameof(LessonPerMonth), 50);
            set => AppSettings.AddOrUpdateValue(nameof(LessonPerMonth), value);
        }

        public static DateTime LastBackup
        {
            get => AppSettings.GetValueOrDefault(nameof(LastBackup), DateTime.MinValue);
            set => AppSettings.AddOrUpdateValue(nameof(LastBackup), value);
        }
    }
}
