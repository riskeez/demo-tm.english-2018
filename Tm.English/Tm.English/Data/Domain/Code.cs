using SQLite;
using System;

namespace Tm.English.Data.Domain
{
    public class Code 
    {
        [Indexed]
        public string Key { get; set; }

        public string CodeNo { get; set; }

        public int Index { get; set; }

        public DateTime ScannedDate { get; set; }

        [Ignore]
        public string DisplayName => $"#{Index}. {ScannedDate.ToString("g")}. No: {CodeNo}";
    }
}
