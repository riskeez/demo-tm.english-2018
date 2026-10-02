using System;
using System.Collections.Generic;
using System.Text;

namespace Tm.Mobile.Core.Security
{
    public interface ICipherService
    {
        string Encrypt(string plaintText, string keyPhrase);

        string Decrypt(string encryptedText, string keyPhrase);
    }
}
