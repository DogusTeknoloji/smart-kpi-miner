using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DogusTeknoloji.SmartKPIMiner.Model.Auth;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public static class ElasticSearchRESTAdapter
    {
        public static async Task<Root> GetResponseFromElasticUrlAsync(string urlAddress, string index, string requestBody)
        {
            var result = await GetResponseFromElasticUrlAsync(urlAddress, port: "9200", index, requestBody);
            return result;
        }

        public static async Task<Root> GetResponseFromElasticUrlWithAuthAsync(string urlAddress, string index,
            string requestBody)
        {
            Root result = await GetResponseFromElasticUrlAsync(urlAddress, port: "9200", index, requestBody, true);
            return result;
        }

        public static async Task<Root> GetResponseFromElasticUrlAsync(string urlAddress, string port, string index,
            string requestBody, bool isSecure = false)
        {
            if (string.IsNullOrEmpty(urlAddress)) { throw new ArgumentException("Url Address cannot be null or empty", nameof(urlAddress)); }
            if (string.IsNullOrEmpty(port)) { throw new ArgumentException("Port cannot be null or empty", nameof(port)); }
            if (string.IsNullOrEmpty(index)) { throw new ArgumentException("Index cannot be null or empty", nameof(index)); }
            if (string.IsNullOrEmpty(requestBody)) { throw new ArgumentException("Request Message cannot be null or empty", nameof(requestBody)); }

            WebRequest request;
            
            if (!isSecure)
                request = WebRequest.Create($"http://{urlAddress}:{port}/{index}/_search?pretty");
            else
                request = WebRequest.Create($"http://{ServiceManager._authModel.UserName}:{ServiceManager._authModel.Password}@{urlAddress}:{port}/{index}/_search?pretty");
            
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = requestBody.Length;
            request.Timeout = (int)TimeSpan.FromMinutes(3).TotalMilliseconds;
            request.Credentials = new NetworkCredential(ServiceManager._authModel.UserName,ServiceManager._authModel.Password);
            await using (var requestWriter = new StreamWriter(await request.GetRequestStreamAsync(), Encoding.ASCII))
            {
                await requestWriter.WriteAsync(requestBody);
            }

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException ex)
            {
                return null;
            }

            var incomingStream = response.GetResponseStream();
            using (var responseReader = new StreamReader(incomingStream))
            {
                var jsonResponse = await responseReader.ReadToEndAsync();
                var jsonData = JsonConvert.DeserializeObject<Root>(jsonResponse);
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
        public static string GetRequestBody(DateTime? start, bool isNewTemplate = false)
        {
            if (start == null) return null;

            DateTime? end = start.Value.AddMinutes(CommonFunctions.UnifyingConstant);
            string result = GetRequestBody(start, end, isNewTemplate);
            
            return result;
        }

        public static string GetRequestBody(DateTime? start, DateTime? end, bool isNewTemplate = false)
        {
            if (start == null || end == null || end < start) { return null; }

            long startMilliSec = CommonFunctions.GetCurrentUnixTimestampMillisec(start.Value);
            long endMilliSec = CommonFunctions.GetCurrentUnixTimestampMillisec(end.Value);

            var template = !isNewTemplate ? "\\RequestJsonTemplate.txt" : "\\RequestJsonTemplate-New.txt";
            
            using (StreamReader dataStream = new StreamReader(CommonFunctions.AssemblyDirectory + template))
            {
                string jsonBody = dataStream.ReadToEnd();
                jsonBody = jsonBody.Replace("~@GreaterThanOrEqual@~", startMilliSec.ToString());
                jsonBody = jsonBody.Replace("~@LowerThanOrEqual@~", endMilliSec.ToString());
                return jsonBody;
            }
        }
    }
   
}
