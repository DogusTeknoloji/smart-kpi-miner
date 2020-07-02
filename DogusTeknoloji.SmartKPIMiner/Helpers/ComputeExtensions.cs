using DogusTeknoloji.SmartKPIMiner.Core;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Helpers
{
    public static class ComputeExtensions
    {
        private enum ResponseStatus { Failed = 0, Success = 1 }
        private static List<string> _successCodes = new List<string> { "1", "2", "3", "401" };
        private static ResponseStatus GetResponseStatus(this AggregationResponseItem item)
        {

            var settingsSuccessCodes = ServiceManager.Appsettings.GetSection("HttpSuccessCodes")
                                                                 .AsEnumerable()
                                                                 .Where(x => x.Value != null)
                                                                 .Select(x => x.Value);
            if (settingsSuccessCodes != null)
            {
                _successCodes = settingsSuccessCodes.Cast<string>().ToList();
            }

            string responseCode = item.HttpResponseCode.ToString();
            bool isSuccess = false;
            foreach (string successCode in _successCodes)
            {
                int codeLength = successCode.Length;
                isSuccess = codeLength > 3 ? responseCode == successCode
                                           : responseCode.StartsWith(successCode);
                if (isSuccess) { break; }
            }
            return isSuccess ? ResponseStatus.Success : ResponseStatus.Failed;
        }

        public static double GetSuccessRate(this AggregationItem item, out int successCount, out double successAverageResponseTime)
        {
            int totalCount = 0;
            int sCount = 0;

            successAverageResponseTime = 0;

            foreach (AggregationResponseItem responseItem in item.ResponseInformation)
            {
                if (responseItem.GetResponseStatus() == ResponseStatus.Success)
                {
                    successAverageResponseTime += responseItem.AverageResponseTime;
                    sCount += responseItem.DocumentCount;
                }
                totalCount += responseItem.DocumentCount;
            }

            double percentage = (double)sCount * 100 / totalCount;

            if (sCount != 0)
            {
                successAverageResponseTime = (successAverageResponseTime / sCount) / 1000;
            }

            successCount = sCount;

            return percentage;
        }
        public static double GetFailedRate(this AggregationItem item, out int failedCount, out double failedAverageResponseTime)
        {
            int totalCount = 0;
            int fCount = 0;

            failedAverageResponseTime = 0;

            foreach (AggregationResponseItem responseItem in item.ResponseInformation)
            {
                if (responseItem.GetResponseStatus() == ResponseStatus.Failed)
                {
                    failedAverageResponseTime += responseItem.AverageResponseTime;
                    fCount += responseItem.DocumentCount;
                }
                totalCount += responseItem.DocumentCount;
            }

            double percentage = (double)fCount * 100 / totalCount;

            if (fCount != 0)
            {
                failedAverageResponseTime = (failedAverageResponseTime / fCount) / 1000;
            }

            failedCount = fCount;

            return percentage;
        }
        public static List<AggregationItem> GetAsAggregationItems(this Aggregation aggregation)
        {
            List<AggregationItem> aggregationItems = new List<AggregationItem>();
            foreach (var (server, site, item) in aggregation.Items.SelectMany(server => server.Sites.SelectMany(site => site.Urls.Select(item => (server, site, item)))))
            {
                item.ServerName = server.ServerName;
                item.SiteName = site.SiteName;
                aggregationItems.Add(item);
            }
            return aggregationItems;
        }
        public static bool SplitAndGetLastPartOfString(this string data, char seperator, out string lastPart)
        {
            string lastPartOfData = data.Split(seperator).LastOrDefault();
            lastPart = lastPartOfData;
            if (string.IsNullOrWhiteSpace(lastPartOfData)) { return false; }
            return true;
        }
        public static bool CheckIsFormatIncluded(this string url)
        {
            if (string.IsNullOrEmpty(url)) { return true; }

            List<string> excludedFileFormats = ServiceManager._kpiService.GetExcludedFileFormats();
            bool isParseSuccess = url.SplitAndGetLastPartOfString('/', out string lastPartOfUrl);
            if (!isParseSuccess) { return true; }
            isParseSuccess = lastPartOfUrl.SplitAndGetLastPartOfString('.', out lastPartOfUrl);
            if (!isParseSuccess) { return true; }
            if (excludedFileFormats.Contains(lastPartOfUrl.ToLowerInvariant())) { return false; }
            return true;
        }
    }
}