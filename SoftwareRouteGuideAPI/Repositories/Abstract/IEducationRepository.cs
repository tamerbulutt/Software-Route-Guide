using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using System.Collections.Generic;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface IEducationRepository:IBaseRepository<Education>
    {
        
        bool Suggest(Education entity);
  
        List<Education> AllSuggests();

        bool ChangeSuggestStatus(SuggestStatusDto education);
        List<Education> GetByTeacherId(int id);
        //List<Exam> GetExam(int id);
        List<Education> GetLast2Education();
        List<Education> GetFirst6Education();
        List<Education> UserEducations(int id); 
        int GetUserEducationsCount(int id); 
        int GetUserEducationsMonthlyCount(int id);
        bool GetExamStatus(int userId, int educationId);
    }
}