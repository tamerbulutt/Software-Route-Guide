using System.Collections.Generic;
using SoftwareRouteGuideAPI.Model.Exam;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface IQuestionRepository
    {
        List<Question> getQuestions(int educationID);
        long AddQuestion(Question question);
        bool UpdateQuestions(List<Question> questions);
        int CheckAnswers(List<int> ids);
        bool FinishExam(CalculateModel model, int score);
        
    }
}