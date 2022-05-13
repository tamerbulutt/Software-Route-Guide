using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPP.Helpers;
using SoftwareRouteGuideAPP.Models.Exam;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class ExamController : Controller
    {
        WebRequestHelper webRequestHelper;
        public ExamController()
        {
            webRequestHelper = new();
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            string userType = HttpContext.Session.GetString("userType");
            if (userType == "Admin" || userType == "Teacher")
            {
                return View(id);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        public async Task<IActionResult> Add(JsonStrPost data)
        {
            var questions = JsonConvert.DeserializeObject<List<QuestionDto>>(data.json);
            var response = await webRequestHelper.PostBuilder("Question/AddQuestions", questions); //HttpRequest
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (HttpContext.Session.GetInt32("userID").HasValue)
            {
                var response = await webRequestHelper.GetBuilder($"Question/GetQuestions?id={id}");
                List<QuestionDto> questions = JObject.Parse(response).SelectToken("data").ToObject<List<QuestionDto>>();
                return View(questions);
            }
            return RedirectToAction("Login", "User");

        }

        [HttpPost]
        public async Task<IActionResult> Calc(CalculateModel model)
        {
            var response = await webRequestHelper.PostBuilder("Question/FinishExam", model);
            return Json(response);
        }

    }
}