using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tm.English.Data.Domain;

namespace Tm.English.Data
{
    public interface IDataRepository
    {
        bool BackupData();
        bool RestoreBackup();

        /// <summary>
        /// Get Planned for Past Months
        /// </summary>
        /// <returns></returns>
        Task<int> GetPlannedForPastMonths();
        /// <summary>
        /// Set Planned for Past Months value
        /// </summary>
        /// <param name="plannedForPastMonths"></param>
        /// <returns></returns>
        Task SetPlannedForPastMonths(int plannedValue);
        /// <summary>
        /// Get date of last update of Planned field
        /// </summary>
        /// <returns></returns>
        Task<DateTime> GetPlannedLastUpdate();
        
        Task<List<Code>> GetCodes(Expression<Func<Code, bool>> filter = null);
        Task<List<Code>> GetCodes(int start, int count, Expression<Func<Code, bool>> filter = null);

        Task<bool> AddCode(string key, string codeNo);
        Task<int> GetCodesCount();

        Task ClearAllDataAsync();
    }
}
