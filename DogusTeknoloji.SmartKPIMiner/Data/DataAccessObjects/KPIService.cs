using DogusTeknoloji.SmartKPIMiner.Core;
using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Model.Database;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Data.DataAccessObjects
{
    public class KPIService
    {
        private readonly string _connectionString;
        private readonly ExpiringList<string> _excludedFileExtensions = new ExpiringList<string>(expiringPeriod: TimeSpan.FromSeconds(60));
        protected KPIService()
        {

        }

        public KPIService(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<string> GetExcludedFileFormats()
        {
            if (_excludedFileExtensions.IsRunning)
            {
                return _excludedFileExtensions.ToList();
            }
            else
            {
                using (var context = new SmartKPIDbContext(this._connectionString))
                {
                    List<string> excFileFormatList = new List<string>();
                    List<ExcludedFileFormat> excludedFiles = context.ExcludedFileFormats.ToList();
                    var list = excludedFiles.Select(x => x.FormatExtension).ToList();
                    this._excludedFileExtensions.AddRange(list);
                    this._excludedFileExtensions.Run();
                    return list;
                }
            }
        }
        public async Task<List<SearchIndex>> GetSearchIndicesAsync()
        {
            using (var context = new SmartKPIDbContext(this._connectionString))
            {
                return await context.SearchIndices.Where(x => x.IsActive).ToListAsync();
            }
        }
        public async Task<DateTime> GetSearchRangeAsync(long indexId)
        {
            using (var context = new SmartKPIDbContext(this._connectionString))
            {
                DateTime? maxLogDate = await context.KPIMetrics.Where(x => x.IndexId == indexId)
                                                                  .MaxAsync(x => x.LogDate);
                if (maxLogDate != null)
                {
                    maxLogDate = maxLogDate.Value.AddMilliseconds(10);
                    return maxLogDate.Value;
                }
                return DateTime.Now.AddDays(-7);
            }
        }
        public async Task<byte> InsertKPIAsync(AggregationItem item, long searchIndexId, DateTime logDate)
        {
            if (!item.Url.CheckIsFormatIncluded()) { return 0; }

            double failedPercentage = item.GetFailedRate(out int failedCount, out double failedAverage);
            double successPercentage = item.GetSuccessRate(out int successCount, out double successAverage);

            if (ServiceManager.LastRuleId == 0)
            {
                ServiceManager.LastRuleId = await GetMaxComputeRuleId();
            }

            using (var context = new SmartKPIDbContext(this._connectionString))
            {
                _ = context.KPIMetrics.Add(new KPIMetric
                {
                    ServerName = item.ServerName.Trim(),
                    SiteName = item.SiteName.Trim(),
                    PageUrl = item.Url.Trim(),
                    AverageResponseTime = successAverage == 0 ? failedAverage     // if success average is 0 pass failed average
                                        : failedAverage == 0 ? successAverage     // if failed average is 0 pass success average
                                        : ((successAverage + failedAverage) / 2), // else take arithmetic average
                    SuccessAverageResponseTime = successAverage,
                    FailedAverageResponseTime = failedAverage,
                    TotalRequestCount = item.RequestCount,
                    FailedPercentage = failedPercentage,
                    FailedRequestCount = failedCount,
                    SuccessPercentage = successPercentage,
                    SuccessRequestCount = successCount,
                    IndexId = searchIndexId,
                    LogDate = logDate,
                    ComputeRuleId = ServiceManager.LastRuleId
                }); ;

                int result = await context.SaveChangesAsync();
                context.Dispose();
                return result > 0 ? (byte)1 : (byte)2;
            }
        }
        public async Task<bool> InsertKPIsAsync(List<AggregationItem> items, long searchIndexId, DateTime logDate)
        {
            List<Task<byte>> taskList = new List<Task<byte>>();
            foreach (var item in items)
            {
                var task = this.InsertKPIAsync(item, searchIndexId, logDate);
                taskList.Add(task);
            }
            bool isSuccess = taskList.TrueForAll(x => x.Result < 2);
            await Task.WhenAll(taskList);
            taskList.Clear();
            return isSuccess;
        }

        public async Task InsertComputeRule(IEnumerable<string> httpSuccessCodes)
        {
            httpSuccessCodes = httpSuccessCodes.OrderBy(x => x);
            string httpCodes = string.Join(',', httpSuccessCodes);

            ComputeRule computeRule = new ComputeRule { HttpSuccessCodes = httpCodes };
            using (var context = new SmartKPIDbContext(this._connectionString))
            {
                DateTime maxDate = await context.ComputeRules.MaxAsync(x => x.CreateDate);
                ComputeRule lastRule = await context.ComputeRules.FirstOrDefaultAsync(x => x.CreateDate == maxDate);
                if (lastRule.HttpSuccessCodes != httpCodes)
                {
                    context.ComputeRules.Add(computeRule);
                    await context.SaveChangesAsync();
                }
            }

            ServiceManager.LastRuleId = await GetMaxComputeRuleId();
        }

        public async Task<long> GetMaxComputeRuleId()
        {
            using (var context = new SmartKPIDbContext(this._connectionString))
            {
                DateTime maxDate = await context.ComputeRules.MaxAsync(x => x.CreateDate);
                ComputeRule lastRule = await context.ComputeRules.FirstOrDefaultAsync(x => x.CreateDate == maxDate);
                return lastRule.ComputeRuleId;
            }
        }
    }
}
