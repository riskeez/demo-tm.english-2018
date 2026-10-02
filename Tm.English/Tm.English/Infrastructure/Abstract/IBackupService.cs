using System;
using System.Collections.Generic;
using System.Text;

namespace Tm.English.Infrastructure.Abstract
{
    public interface IBackupService
    {
        bool BackupFile(string fileName);

        bool RestoreBackup(string fileName);
    }
}
