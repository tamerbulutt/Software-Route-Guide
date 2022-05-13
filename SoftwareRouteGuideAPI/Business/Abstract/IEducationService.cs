using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface IEducationService:IBaseService<Education,EducationAddDto>
    {
        //IResult rateEducation(int point, int educationId);
        IResult Suggest(EducationAddDto entity);
        IResult AllSuggests();
        IResult ChangeSuggestStatus(SuggestStatusDto suggestStatus);            
        IResult GetLast2Education();
        IResult GetFirst6Education();
        IResult UserEducations(int id); 
        IResult getByTeacherId(int id);
        IResult GetUserEducationsCount(int id); 
        IResult GetUserEducationsMonthlyCount(int id); 
        bool GetExamStatus(int userId,int educationId); 
    }
}