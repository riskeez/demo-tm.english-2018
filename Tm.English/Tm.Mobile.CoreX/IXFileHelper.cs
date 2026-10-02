using System;
using System.Collections.Generic;
using System.Text;

namespace Tm.Mobile.CoreX.Abstract
{
    public interface IXFileHelper
    {
        string GetLocalFilePath(string fileName);

        string GetExternalFilePath(string fileName);

        long GetFileSize(string filePath);
    }
}
