using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPP.Helpers;
using SoftwareRouteGuideAPP.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareRouteGuideAPP.Models;
using System.Net.Http;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class ContactController : Controller
    {
        WebRequestHelper webRequestHelper;
        public ContactController()
        {
            webRequestHelper = new();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactAdd contact)
        {
            var response = await webRequestHelper.PostBuilder("Complaint/Add",contact);
            return RedirectToAction("Contact");
        }

        [HttpGet]
        public IActionResult ContactTeacher()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var educations = await webRequestHelper.GetBuilder("Complaint/GetAll");
            var responseData = JObject.Parse(educations).SelectToken("data").ToObject<List<Contacts>>();
            
            return View(responseData);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), "https://localhost:5001/api/Complaint/Delete?id=" + id))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    var response = await httpClient.SendAsync(request);
                    var result = response.Content.ReadAsStringAsync().Result;

                }
            }

            return RedirectToAction("ContactList");
        }
    }
}
