using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tm.English.Data.Domain;
using Tm.English.Infrastructure;
using Tm.English.Infrastructure.Abstract;
using Tm.Mobile.CoreX.Abstract;

namespace Tm.English.Data
{
    public class SQLiteRepository : IDataRepository
    {
        readonly SQLiteAsyncConnection connect;
        readonly IBackupService backupService;

        public SQLiteRepository(IXFileHelper fileService, IBackupService backupService)
        {
            this.backupService = backupService;

            var dbPath = fileService.GetLocalFilePath(Constants.DataBase.DbName);

            connect = new SQLiteAsyncConnection(dbPath);

            InitDataBase();
        }

        private void InitDataBase()
        {
            Task.WhenAll(new Task[]
            {
                connect.CreateTableAsync<SystemData>(),
                connect.CreateTableAsync<Code>(),
            })
            .ConfigureAwait(false);
        }

        #region BackUp
        public bool BackupData()
        {
            return backupService.BackupFile(Constants.DataBase.DbName);
        }

        public bool RestoreBackup()
        {
            var success = backupService.RestoreBackup(Constants.DataBase.DbName);
            if (success)
            {
                InitDataBase();
            }

            return success;
        }
        #endregion

        private async Task<SystemData> GetSystemDataObject()
        {
            var sysData = await connect.Table<SystemData>().FirstOrDefaultAsync();
            if (sysData == null)
            {
                sysData = new SystemData { PlannedLastUpdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) };
                await connect.InsertAsync(sysData);
            }
            return sysData;
        }

        public async Task<int> GetPlannedForPastMonths()
        {
            DateTime current = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var sysData = await GetSystemDataObject();

            if (DateTime.Compare(sysData.PlannedLastUpdate, current) >= 0)
            {
                return sysData.PlannedForPastMonths;
            }

            int gapLessonsCount = current.GetMonthDiff(sysData.PlannedLastUpdate) * Settings.LessonPerMonth;

            int planned = sysData.PlannedForPastMonths + gapLessonsCount;
            await SetPlannedForPastMonths(planned);

            return planned;
        }

        public async Task SetPlannedForPastMonths(int plannedValue)
        {
            var sysData = new SystemData { PlannedForPastMonths = plannedValue, PlannedLastUpdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) };

            bool updated = await connect.UpdateAsync(sysData) > 0;
            if (!updated)
            {
                await connect.InsertAsync(sysData);
            }
        }

        public async Task<DateTime> GetPlannedLastUpdate()
        {
            var sysData = await GetSystemDataObject();
            return sysData.PlannedLastUpdate;
        }

        public async Task<bool> AddCode(string key, string codeNo)
        {
            var foundItem = await connect.FindAsync<Code>(l => l.Key == key);
            if (foundItem == null)
            {
                int order = await GetCodesCount() + 1;
                var newKey = new Code()
                {
                    Key = key,
                    CodeNo = codeNo,
                    Index = order,
                    ScannedDate = DateTime.UtcNow
                };
                return await connect.InsertAsync(newKey) > 0;
            }
            return false;
        }

        public async Task<int> GetCodesCount()
        {
            var tQuery = connect.Table<Code>();
            return await tQuery.CountAsync();
        }

        public async Task<List<Code>> GetCodes(Expression<Func<Code, bool>> filter = null) => await GetCodes(-1, -1, filter);

        public async Task<List<Code>> GetCodes(int start, int count, Expression<Func<Code, bool>> filter = null)
        {
            return await GetListAsync(start, count, filter, o => o.OrderByDescending(c => c.Index));
        }

        public async Task ClearAllDataAsync()
        {
            var taskList = new List<Task>()
            {
                ClearTableAsync(nameof(SystemData)),
                ClearTableAsync(nameof(Code)),
            };

            await Task.WhenAll(taskList).ConfigureAwait(false);

            await connect.ExecuteAsync("vacuum");
        }

        private async Task ClearTableAsync(string tableName)
        {
            await connect.ExecuteAsync($"delete from '{tableName}'");
        }

        private Task<List<TEntity>> GetListAsync<TEntity>(int startPos, int count, Expression<Func<TEntity, bool>> filter,
                                                            Func<AsyncTableQuery<TEntity>, AsyncTableQuery<TEntity>> order)
            where TEntity : new()
        {
            var tQuery = connect.Table<TEntity>();

            if (filter != null)
                tQuery = tQuery.Where(filter);

            if (order != null)
                tQuery = order(tQuery);

            tQuery = startPos > -1 ? tQuery.Skip(startPos) : tQuery;
            tQuery = count > -1 ? tQuery.Take(count) : tQuery;

            return tQuery.ToListAsync();
        }
    }
}
