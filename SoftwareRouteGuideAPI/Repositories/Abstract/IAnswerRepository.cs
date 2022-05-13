using System.Collections.Generic;
using SoftwareRouteGuideAPI.Model.Exam;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface IAnswerRepository
    {
        bool addAnswers(List<Answer> answers);
        List<Answer> getAnswers(int questionID);
        bool updateAnswers(List<Answer> answers);
    }
}