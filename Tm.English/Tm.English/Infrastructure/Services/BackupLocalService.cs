using System;
using System.Diagnostics;
using Tm.English.Infrastructure.Abstract;
using Tm.Mobile.CoreX.Abstract;

namespace Tm.English.Services
{
    public class BackupLocalService : IBackupService
    {
        readonly IXFileHelper fileService;
        public BackupLocalService(IXFileHelper fileService)
        {
            this.fileService = fileService;
        }

        public bool BackupFile(string filename)
        {
            try
            {
                var filePath = fileService.GetLocalFilePath(filename);
                if (System.IO.File.Exists(filePath))
                {
                    var destPath = fileService.GetExternalFilePath(filename);

                    System.IO.File.Copy(filePath, destPath, true);

                    if (System.IO.File.Exists(destPath))
                    {
                        Settings.LastBackup = DateTime.Now;
                        return true;
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }

            return false;
        }

        public bool RestoreBackup(string filename)
        {
            try
            {
                var backupPath = fileService.GetExternalFilePath(filename);
                if (System.IO.File.Exists(backupPath))
                {
                    var destPath = fileService.GetLocalFilePath(filename);

                    System.IO.File.Copy(backupPath, destPath, true);
                    if (System.IO.File.Exists(destPath))
                    {
                        Settings.LastBackup = DateTime.MinValue;
                        return true;
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }

            return false;
        }
    }
}
