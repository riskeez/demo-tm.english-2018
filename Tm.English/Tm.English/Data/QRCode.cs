using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tm.English.Data
{
    public class QRCode
    {
        public string Key { get; set; }

        public string No { get; set; }

        public static QRCode DeserialzeFromJSON(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
                return null;

            return JsonConvert.DeserializeObject<QRCode>(jsonData);
        }
    }
}
