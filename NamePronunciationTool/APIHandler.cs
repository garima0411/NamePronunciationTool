using System;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NamePronunciationTool
{
    
    public class APIHandler
    {
        readonly HttpClient httpClient;
        public APIHandler()
        {
            httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            var apiUrl = APIConfiguration.GetConfigString(QueryHelper.apiConfig, QueryHelper.apiUrl);
            httpClient.BaseAddress = new Uri(apiUrl);
            
        }

        internal async Task<string> RecordYourName(string employeeId)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/recording?employeeId=" + employeeId + "");
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var httpsResponse = await httpClient.SendAsync(httpRequestMessage);
            httpsResponse.EnsureSuccessStatusCode();
            var content = await httpsResponse.Content.ReadAsStringAsync();
            return content;
        }
    }
}
