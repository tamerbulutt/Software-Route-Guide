using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SoftwareRouteGuideAPP.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using SoftwareRouteGuideAPP.Helpers;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPP.Models.Educations;
using Newtonsoft.Json;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        WebRequestHelper webRequestHelper;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            webRequestHelper = new();
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var first6educations = await webRequestHelper.GetBuilder($"Education/GetFirst6Education");
            var responseDetails = JsonConvert.DeserializeObject<ResponseModel<List<Education>>>(first6educations);

            List<Education> responseDataFirst6 = new();
            if (responseDetails.success)
                responseDataFirst6 = JObject.Parse(first6educations).SelectToken("data").ToObject<List<Education>>();

            return View(responseDataFirst6);
        }

        void test(){
            


            /*

            //GET DATA FROM DB WITH PARAMETERS ORNEK //
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:5001/api/Identity/GetDataFromDbWithParameters"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    request.Content = new StringContent("{\"user_id\":1,\"userFirstName\":\"string\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseRequest = await httpClient.SendAsync(request);
                    var response = await responseRequest.Content.ReadAsStringAsync();

                    Console.WriteLine("GET DATA WITH PRM=> " + response);
                }
            }
            //GET DATA FROM DB WITH PARAMETERS ORNEK //

            //INSERT TO DATABASE ORNEK
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://localhost:5001/api/Identity/InsertDatabase"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    request.Content = new StringContent("{\"user_id\":0,\"userFirstName\":\"ExampleInsert\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseRequest = await httpClient.SendAsync(request);
                    var response = await responseRequest.Content.ReadAsStringAsync();

                    Console.WriteLine("INSERT=>" + response);
                }
            }
            //INSERT TO DATABASE ORNEK

            //UPDATE DATABASE ORNEK//
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PUT"), "https://localhost:5001/api/Identity/UpdateDatabase"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    request.Content = new StringContent("{\"user_id\":6,\"userFirstName\":\"UPDATE ORNEK\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseRequest = await httpClient.SendAsync(request);
                    var response = await responseRequest.Content.ReadAsStringAsync();

                    Console.WriteLine("UPDATE=> " + response);
                }
            }
            //UPDATE DATABASE ORNEK//

            //DELETE FROM DATABASE ORNEK//
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), "https://localhost:5001/api/Identity/DeleteRowFromDatabase"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    request.Content = new StringContent("{\"user_id\":9,\"userFirstName\":\"string\"}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var responseRequest = await httpClient.SendAsync(request);
                    var response = await responseRequest.Content.ReadAsStringAsync();

                    Console.WriteLine("DELETE=> " + response);
                }
            }
            //DELETE FROM DATABASE ORNEK//
            */
            //return View();
        }
    
    }
}
