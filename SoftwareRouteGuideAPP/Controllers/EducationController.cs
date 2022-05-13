using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPP.Helpers;
using SoftwareRouteGuideAPP.Models;
using SoftwareRouteGuideAPP.Models.Educations;
using SoftwareRouteGuideAPP.Models.Teacher;
using Microsoft.AspNetCore.Http;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class EducationController : Controller
    {
        WebRequestHelper webRequestHelper;
        public EducationController()
        {
            webRequestHelper = new();
        }

        [HttpPost]
        public async Task<IActionResult> AddEducation(EducationAdd education)
        {
            
            var query = new { education.Title, education.Type, education.Photo1, education.Paragraph1, education.Paragraph2, education.Paragraph3, education.Subtitle1, education.Paragraph4, education.Paragraph5, education.Photo2, education.Paragraph6, education.Paragraph7, education.Paragraph8, education.Subtitle2, education.Paragraph9, education.Paragraph10 , education.userID};
            var response = await webRequestHelper.PostBuilder("Education/Add", query); //HttpRequest

            return RedirectToAction("AllEducations");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AllEducations()
        {
            var educations = await webRequestHelper.GetBuilder("Education/AllEducations");
            List<Education> responseData = new();
            var responseDetails = JsonConvert.DeserializeObject<ResponseModel<List<Education>>>(educations);

            if (responseDetails.success)
                responseData = JObject.Parse(educations).SelectToken("data").ToObject<List<Education>>();

            return View(responseData);
        }

        [HttpGet]
        public async Task<IActionResult> EducationDetails(int id)
        {
            EducationDetailsViewModel educationDetails = new();

            var educations = await webRequestHelper.GetBuilder($"Education/GetEducationById?id={id}");
            var comments = await webRequestHelper.GetBuilder($"Comment/GetCommentsByEducationId?id={id}");

            var responseData = JsonConvert.DeserializeObject<ResponseModel<Education>>(educations);

            if(!responseData.success)
                return RedirectToAction("AllEducations");

            //var teacher =  await webRequestHelper.GetBuilder("Teacher/GetTeacherByID?id="+ responseData.data.teacherID);

            IEnumerable<Comments> responseDataComments;
            //IEnumerable<Teachers> responseDataTeacher;

            //responseDataTeacher = JObject.Parse(teacher).SelectToken("data").ToObject<List<Teachers>>();

            if (JObject.Parse(comments).SelectToken("data") != null)
                responseDataComments = JObject.Parse(comments).SelectToken("data").ToObject<List<Comments>>();
            else
                responseDataComments = null;

            if(HttpContext.Session.GetInt32("userID") != null)
            {
                var userId = HttpContext.Session.GetInt32("userID");
                var examStatus = await webRequestHelper.GetBuilder($"Education/GetExamStatus?userId={userId}&educationId={id}");
                educationDetails.examStatus = Convert.ToBoolean(examStatus);
            }
            

            educationDetails.comments = responseDataComments;
            educationDetails.education = responseData.data;
            //educationDetails.teacher = responseDataTeacher;

            return View(educationDetails);
        }

        [HttpGet]
        public IActionResult EducationProposal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EducationProposal(EducationAdd education)
        {
            var query = new { education.Title, education.Type, education.Photo1, education.Paragraph1, education.Paragraph2, education.Paragraph3, education.Subtitle1, education.Paragraph4, education.Paragraph5, education.Photo2, education.Paragraph6, education.Paragraph7, education.Paragraph8, education.Subtitle2, education.Paragraph9, education.Paragraph10 , education.userID};
            var response = await webRequestHelper.PostBuilder("Education/Suggest", query); //HttpRequest

            return RedirectToAction("AllEducations");
        }

        [HttpGet]
        public async Task<IActionResult> ProposalList()
        {
            var educations = await webRequestHelper.GetBuilder("Education/AllSuggests");
            var responseData = JsonConvert.DeserializeObject<ResponseModel<List<Education>>>(educations);
            var viewData = new List<Education>();

            if (responseData.success)
                viewData = JObject.Parse(educations).SelectToken("data").ToObject<List<Education>>();

            return View(viewData);
        }

        [HttpGet]
        public async Task<IActionResult> Educations()
        {
            var last2educations = await webRequestHelper.GetBuilder($"Education/GetLast2Education");
            var first6educations = await webRequestHelper.GetBuilder($"Education/GetFirst6Education");

            List<Education> responseDataLast2 = new();
            List<Education> responseDataFirst6 = new();

            var responseDetails = JsonConvert.DeserializeObject<ResponseModel<List<Education>>>(last2educations);
            var responseDetails2 = JsonConvert.DeserializeObject<ResponseModel<List<Education>>>(first6educations);

            if(responseDetails.success)
                responseDataLast2 = JObject.Parse(last2educations).SelectToken("data").ToObject<List<Education>>();
            
            if(responseDetails2.success)
                responseDataFirst6 = JObject.Parse(first6educations).SelectToken("data").ToObject<List<Education>>();

            EducationViewModel education = new();
            education.educationFirst6 = responseDataFirst6;
            education.educationLast2 = responseDataLast2;

            return View(education);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await webRequestHelper.GetBuilder($"Education/GetEducationById?id={id}");
            if (JObject.Parse(response).SelectToken("data") != null)
            {
                var education = JObject.Parse(response).SelectToken("data").ToObject<Education>();
                return View(education);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EducationUpdate education)
        {
            await webRequestHelper.PostBuilder("Education/Update", education); //HttpRequest
            return RedirectToAction("AllEducations");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comments comments)
        {
            var query = new { comments.educationID, comments.content, comments.fullname }; //Query i�in string
            var response = await webRequestHelper.PostBuilder("Comment/Add", query); //HttpRequest

            return RedirectToAction("EducationDetails", new { id = comments.educationID });
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), "https://localhost:5001/api/Education/Delete?id="+id))
                {
                    request.Headers.TryAddWithoutValidation("accept", "text/plain");

                    var response = await httpClient.SendAsync(request);
                    var result = response.Content.ReadAsStringAsync().Result;
                    
                }
            }

            return RedirectToAction("AllEducations");
        }

        public async Task<IActionResult> Approval(int id)
        {
            SuggestStatusDto suggestStatus = new();
            suggestStatus.educationID = id;
            suggestStatus.status = "Active";

            var response = await webRequestHelper.PostBuilder("Education/ChangeSuggestStatus", suggestStatus); //HttpRequest

            return RedirectToAction("ProposalList");
        }

        public async Task<IActionResult> Rejected(int id)
        {
            SuggestStatusDto suggestStatus = new();
            suggestStatus.educationID = id;
            suggestStatus.status = "Rejected";

            var response = await webRequestHelper.PostBuilder("Education/ChangeSuggestStatus", suggestStatus); //HttpRequest

            try
            {
                return RedirectToAction("ProposalList");
            }
            catch
            {
                return RedirectToAction("AllEducations");
            }
        }
    }
}