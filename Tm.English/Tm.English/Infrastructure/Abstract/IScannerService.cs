using System;
using System.Collections.Generic;
using System.Text;

namespace Tm.English.Infrastructure.Abstract
{
    public interface IScannerService
    {
        event EventHandler<string> OnScanResult;

        void StartScan();
    }
}
