using System.Collections.Generic;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Exam;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface IQuestionService
    {
        IResult getQuestions(int educationID);
        IResult AddQuestions(List<QuestionDto> questions);
        IResult UpdateQuestions(List<QuestionDto> questions);
        IResult FinishExam(CalculateModel model);
    }
}