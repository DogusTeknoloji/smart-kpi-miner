using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public static class ElasticSearchRESTAdapter
    {
        public static async Task<Root> GetResponseFromElasticUrlAsync(string urlAddress, string index, string requestBody)
        {
            Root result = await GetResponseFromElasticUrlAsync(urlAddress, port: "9200", index, requestBody);
            return result;
        }

        public static async Task<Root> GetResponseFromElasticUrlAsync(string urlAddress, string port, string index, string requestBody)
        {
            if (string.IsNullOrEmpty(urlAddress)) { throw new ArgumentException("Url Address cannot be null or empty", nameof(urlAddress)); }
            if (string.IsNullOrEmpty(port)) { throw new ArgumentException("Port cannot be null or empty", nameof(port)); }
            if (string.IsNullOrEmpty(index)) { throw new ArgumentException("Index cannot be null or empty", nameof(index)); }
            if (string.IsNullOrEmpty(requestBody)) { throw new ArgumentException("Request Message cannot be null or empty", nameof(requestBody)); }

            WebRequest request = WebRequest.Create($"http://{urlAddress}:{port}/{index}/_search?pretty");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = requestBody.Length;
            request.Timeout = (int)TimeSpan.FromMinutes(3).TotalMilliseconds;

            using (StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), Encoding.ASCII))
            {
                requestWriter.Write(requestBody);
            }

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                return null;
            }

            Stream incomingStream = response.GetResponseStream();
            using (StreamReader responseReader = new StreamReader(incomingStream))
            {
                string jsonResponse = await responseReader.ReadToEndAsync();
                Root jsonData = JsonConvert.DeserializeObject<Root>(jsonResponse);
                return jsonData;
            }
        }


        public static string GetFullIndexName(string index, string indexPattern, DateTime? indexPatternValue)
        {
            if (index == null | indexPattern == null) { return null; }

            string result;

            //Prevent multiple call of ValidateDateFormat() method
            bool validateFormatResult = indexPattern.ValidateDateFormat();
            if (validateFormatResult && !indexPatternValue.HasValue) { return null; }

            if (!validateFormatResult)
            {
                result = index + "-" + indexPattern;
            }
            else if (validateFormatResult && indexPatternValue.HasValue)
            {
                result = index + "-" + indexPatternValue.Value.ToString(indexPattern);
            }
            else
            {
                result = index + "-*";
            }

            return result;
        }
        public static string GetRequestBody(DateTime? start)
        {
            if (start == null) return null;

            DateTime? end = start.Value.AddMinutes(CommonFunctions.UnifyingConstant);
            string result = GetRequestBody(start, end);
            return result;
        }

        public static string GetRequestBody(DateTime? start, DateTime? end)
        {
            if (start == null || end == null || end < start) { return null; }

            long startMilliSec = CommonFunctions.GetCurrentUnixTimestampMillisec(start.Value);
            long endMilliSec = CommonFunctions.GetCurrentUnixTimestampMillisec(end.Value);

            using (StreamReader dataStream = new StreamReader(CommonFunctions.AssemblyDirectory + "\\RequestJsonTemplate.txt"))
            {
                string jsonBody = dataStream.ReadToEnd();
                jsonBody = jsonBody.Replace("~@GreaterThanOrEqual@~", startMilliSec.ToString());
                jsonBody = jsonBody.Replace("~@LowerThanOrEqual@~", endMilliSec.ToString());
                return jsonBody;
            }
        }
    }
}
