using System;
using System.IO;
using Tm.English.Droid.Helpers;
using Tm.Mobile.CoreX.Abstract;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Tm.English.Droid.Helpers
{
    public class FileHelper : IXFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public long GetFileSize(string filename)
        {
            FileInfo file = new FileInfo(filename);
            return file.Length;
        }

        public string GetExternalFilePath(string fileName)
        {
            var folder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {
                var extDirs = Android.App.Application.Context.GetExternalFilesDirs(null);
                if (extDirs != null && extDirs.Length > 1)
                {
                    folder = extDirs[1].AbsolutePath;
                }
            }

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return Path.Combine(folder, fileName);
        }
    }
}