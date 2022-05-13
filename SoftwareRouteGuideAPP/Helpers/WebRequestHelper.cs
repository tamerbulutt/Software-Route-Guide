using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Helpers
{
    public class WebRequestHelper
    {
        public async Task<string> PostBuilder(string url, object query)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{Constants.apiBaseURL}{url}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    var requestContent = new JavaScriptSerializer().Serialize(query); //Json Convert

                    request.Content = new StringContent(requestContent);

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseRequest = await httpClient.SendAsync(request);
                    var response = await responseRequest.Content.ReadAsStringAsync();

                    return response;
                }
            }
        }

        public async Task<string> GetBuilder(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{Constants.apiBaseURL}{url}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    var response = await httpClient.SendAsync(request);
                    var responseDetails = response.Content.ReadAsStringAsync().Result;

                    return responseDetails;
                }
            }
        }

        public async Task<string> DeleteBuilder(string url)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), $"{Constants.apiBaseURL}{url}"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    var responseRequest = await httpClient.SendAsync(request);
                    var response = responseRequest.Content.ReadAsStringAsync().Result;

                    return response;
                }
            }
        }
    
    }
}
