using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPP.Helpers;
using SoftwareRouteGuideAPP.Models.Educations;
using SoftwareRouteGuideAPP.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoftwareRouteGuideAPP.Models;
using Newtonsoft.Json;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class TeacherController : Controller
    {
        WebRequestHelper webRequestHelper;
        public TeacherController()
        {
            webRequestHelper = new();
        }

        [HttpGet]
        public async Task<IActionResult> AllTeachers()
        {
            var educations = await webRequestHelper.GetBuilder("Teacher/AllTeachers");
            var responseData = JObject.Parse(educations).SelectToken("data").ToObject<List<Teachers>>();

            return View(responseData);
        }

        [HttpGet]
        public async Task<IActionResult> AllApplications()
        {
            var educations = await webRequestHelper.GetBuilder("Teacher/AllApplications");
            var responseData = JObject.Parse(educations).SelectToken("data").ToObject<List<ApplicationDetails>>();

            return View(responseData);
        }

        [HttpGet]
        public async Task<IActionResult> Educations(int id)
        {
            if (HttpContext.Session.GetInt32("userID") == id)
            {
                var educations = await webRequestHelper.GetBuilder($"Education/GetByTeacherId?id={id}");
                var responseData = JsonConvert.DeserializeObject<ResponseModel<List<Education>>>(educations);
                var viewData = new List<Education>();
                
                if (responseData.success)
                    viewData = JObject.Parse(educations).SelectToken("data").ToObject<List<Education>>();

                return View(viewData);
            }
            else
            {
                return View(null);
            }

        }

        [HttpGet]
        public IActionResult TeacherApplication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeacherApplication(TeacherApplication teacher)
        {
            var query = new { teacher.Message, teacher.userID };
            var response = await webRequestHelper.PostBuilder("Teacher/TeacherApplication", query); //HttpRequest

            return RedirectToAction("TeacherApplication");
        }

        public async Task<IActionResult> Approval(int id)
        {
            var response = await webRequestHelper.PostBuilder("Teacher/ApproveApplication?id="+id, ""); //HttpRequest

            return RedirectToAction("AllApplications");
        }

        public async Task<IActionResult> Rejected(int id)
        {
            var response = await webRequestHelper.PostBuilder("Teacher/RejectApplication?id=" + id, ""); //HttpRequest

            return RedirectToAction("AllApplications");
        }
    }
}
