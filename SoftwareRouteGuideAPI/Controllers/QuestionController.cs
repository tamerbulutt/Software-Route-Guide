using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Exam;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase {
        private IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        
        [HttpPost]
        public IResult AddQuestions(List<QuestionDto> questions){
            return _questionService.AddQuestions(questions);
        }

        [HttpGet]
        public IResult GetQuestions(int id){
            return _questionService.getQuestions(id);
        }

        [HttpPost]
        public IResult FinishExam(CalculateModel model){
            return _questionService.FinishExam(model);
        }
    }
}