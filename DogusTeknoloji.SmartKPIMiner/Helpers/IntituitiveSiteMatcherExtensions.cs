using System;
using System.Collections.Generic;
using System.Linq;

namespace DogusTeknoloji.SmartKPIMiner.Helpers
{
    public static class IntituitiveSiteMatcherExtensions
    {
        private static readonly List<string> _appList = new List<string>();
        public static void AddAsPotentialAppName(this string potentialAppName)
        {
            _appList.Add(potentialAppName);
        }
        public static bool IsPartExcluded(this string part)
        {
            switch (part)
            {
                case "com":
                case "fw":
                case "http":
                case "https":
                case "www":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks Similarity of input between List(applist variable)
        /// Uses Levenshtein Distance Algorithm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double CheckSimilarity(this string input)
        {
            if (_appList.Count == 0) { _appList.Add(""); }

            double overallSimilarityRate = 0;

            List<int> similarityRateList = new List<int>();
            foreach (string potentialAppName in _appList)
            {
                if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(potentialAppName)) { return -1; }

                int m = input.Length + 1;
                int n = potentialAppName.Length + 1;
                int[,] matrix = new int[m, n];

                // Initialize the top and right of the table to 0,1,2,3...
                for (int i = 0; i < m; matrix[i, 0] = i++) ;
                for (int j = 0; j < n; matrix[0, j] = j++) ;

                for (int i = 1; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int cost = (potentialAppName[n - 1] == input[m - 1]) ? 0 : 1;
                        int min1 = matrix[i - 1, j] + 1;
                        int min2 = matrix[i, j - 1];
                        int min3 = matrix[i - 1, j - 1] + cost;

                        matrix[i, j] = Math.Min(Math.Min(min1, min2), min3);
                    }
                }

                int similarityValue = matrix[m, n];
                similarityRateList.Add(similarityValue);
            }
            overallSimilarityRate = similarityRateList.Average();
            return overallSimilarityRate;
        }
    }
}
