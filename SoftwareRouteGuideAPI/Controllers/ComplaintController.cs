using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private IComplaintService _complaintService;
        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet]
        public async Task<string> test()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://localhost:5001/api/Education/AllEducations"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    var response = await httpClient.SendAsync(request);
                    var responseDetails = response.Content.ReadAsStringAsync().Result;

                    var x = JObject.Parse(responseDetails).SelectToken("data")[0];
                    return x.ToString();
                }
            }
        }

        [HttpPost]
        public IResult Add(ComplaintAddDto complaint){
            return _complaintService.add(complaint);
        }

        [HttpDelete]
        public IResult Delete(int id){
            return _complaintService.delete(id);
        }

        [HttpGet]
        public IResult GetAll(){
            return _complaintService.getAll();
        }
    }
}