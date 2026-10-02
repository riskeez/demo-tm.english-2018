using SQLite;
using System;

namespace Tm.English.Data.Domain
{
    public class SystemData
    {
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime PlannedLastUpdate { get; set; }

        public int PlannedForPastMonths { get; set; }
    }
}
