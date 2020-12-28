using DogusTeknoloji.SmartKPIMiner.Core;
using DogusTeknoloji.SmartKPIMiner.Data.DataAccessObjects;
using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Model.Database;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public class OperationContext
    {
        private KPIService _kpiService;
        public LogManager LogManager;
        public OperationContext()
        {
            _kpiService = ServiceManager._kpiService;
            LogManager = new LogManager();
        }

        public async Task ProcessItemsAsync()
        {
            LogManager.Log($"ProcessItems Triggered - {DateTime.Now}");

            IList<Task> indexTaskList = new List<Task>();
            IList<SearchIndex> searchIndices = await this._kpiService.GetSearchIndicesAsync();
            LogManager.Log($"Index count - {searchIndices.Count}");
            foreach (SearchIndex searchIndex in searchIndices)
            {
                Task indexPushOperation = ProcessIndexAsync(searchIndex);
                indexTaskList.Add(indexPushOperation);
            }
            await Task.WhenAll(indexTaskList);
            LogManager.Log($"ProcessItems completed for all tasks - {DateTime.Now}");
            Console.WriteLine("All Done");
        }

        private async Task ProcessIndexAsync(SearchIndex searchIndex)
        {
            DateTime startDate = await this._kpiService.GetSearchRangeAsync(searchIndex.IndexId); // Get last log insertion date 
            int fragmentCount = CalculateLoopCount(startDate); // Calculate fragment count
            LogManager.Log($"Index:{searchIndex.IndexName}, Last Log insertion date - {startDate}");


            DateTime searchRange = startDate.AddMinutes(-CommonFunctions.UnifyingConstant); //It will be fix first iteration's 15 min addition.

            for (int i = 0; i < fragmentCount; i++)
            {
                searchRange = searchRange.AddMinutes(CommonFunctions.UnifyingConstant); // add 15 min for each iteration.

                string requestBody = ElasticSearchRESTAdapter.GetRequestBody(start: searchRange);
                string fullIndexName = ElasticSearchRESTAdapter.GetFullIndexName(index: searchIndex.IndexName, indexPattern: searchIndex.IndexPattern, indexPatternValue: searchRange);
                Root responseRoot = await ElasticSearchRESTAdapter.GetResponseFromElasticUrlAsync(urlAddress: searchIndex.UrlAddress, index: fullIndexName, requestBody: requestBody);

                if (responseRoot == null)
                {
                    // Root is null for current search range
                    // Check for next day of search range > today 
                    // Example: searchRange: 05.01.2020 -> 06.01.2020 > 10.01.2020 ?
                    LogManager.Log($"Index:{fullIndexName}, Search Range: {searchRange} query is returned null response");
                    double checkForNextDay = (DateTime.Now - searchRange.AddDays(1)).TotalMilliseconds;
                    if (checkForNextDay < 0)
                    {
                        break; // it became greater than current date. 
                    }

                    searchRange = searchRange.AddDays(1); // Increase search range by one day
                    // then increase the current loop iterator for increment above to prevent out of range exception.
                    // 24 Hour * 60 Min / fragmentation by minute) - 1 for current iteration
                    i += (24 * 60 / CommonFunctions.UnifyingConstant) - 1;

                    Console.WriteLine($"{searchIndex.UrlAddress}->{searchIndex.IndexName} is expired, skipped to [{i + 1}/{fragmentCount}].");
                    continue;
                }

                InsertDataToDatabase(aggregation: responseRoot.Aggregation, indexId: searchIndex.IndexId, logDate: searchRange);

                Console.WriteLine($"{searchIndex.UrlAddress}->{searchIndex.IndexName} [{i + 1}/{fragmentCount}] added.");
            }

            LogManager.Log($"Index:{searchIndex.IndexName} processing completed. - {DateTime.Now}");
        }

        private void InsertDataToDatabase(Aggregation aggregation, long indexId, DateTime logDate)
        {
            List<AggregationItem> aggregationItems = aggregation?.GetAsAggregationItems(); // if passed aggregation is not null... get items

            if (aggregationItems != null)
            {
                Task insertOperation = this._kpiService.InsertKPIsAsync(items: aggregationItems, searchIndexId: indexId, logDate: logDate);
            }
        }

        private int CalculateLoopCount(DateTime start)
        {
            LogManager.Log($"Current Date: {DateTime.Now}, Start Date: {start}");
            TimeSpan difference = DateTime.Now - start;

            int totalMins = (int)difference.TotalMinutes;
            LogManager.Log($"Difference: {totalMins} minutes");
            if (totalMins < CommonFunctions.UnifyingConstant)
            {
                return 0;// if count is 0 skip this index
            }

            int result = totalMins / CommonFunctions.UnifyingConstant;
            LogManager.Log($"Fragment count until the current date: {result}");
            return result;
        }

    }
}