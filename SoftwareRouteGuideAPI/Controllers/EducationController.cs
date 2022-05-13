using System;
using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private IEducationService _educationService;
        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public IResult AllEducations(){
            return _educationService.getAll();
        } 

        [HttpPost]
        public IResult Add([FromBody] EducationAddDto educationAddDto){
            return _educationService.add(educationAddDto);
        }

        [HttpGet]
        public IResult GetEducationById(int id){
            return _educationService.getById(id);
        }

        [HttpPost]
        public IResult Update([FromBody] Education education) {
            return _educationService.update(education);
        } //Veritabanındaki bir satırı güncellemek için kullanılıyor (Örnek) (UPDATE)
    
        [HttpDelete]
        public IResult Delete(int id){
            return _educationService.delete(id);
        } //Veritabanındaki bir satırı silmek için kullanılan fonksiyon (Örnek) (DELETE)
        
       /* [HttpPost]
        public IResult RateEducation([FromForm] int point,[FromForm] int educationId)
        {
            return _educationService.rateEducation(point,educationId); 
        }*/

        [HttpPost]
        public IResult Suggest([FromBody] EducationAddDto educationAdd)
        {
            return _educationService.Suggest(educationAdd);
        }

        [HttpGet]
        public IResult AllSuggests()
        {
            return _educationService.AllSuggests();
        }

        [HttpPost]
        public IResult ChangeSuggestStatus([FromBody] SuggestStatusDto suggestStatus)
        {
            return _educationService.ChangeSuggestStatus(suggestStatus);
        }

        [HttpGet]
        public IResult GetLast2Education()
        {
            return _educationService.GetLast2Education();
        }

        [HttpGet]
        public IResult GetFirst6Education()
        {
            return _educationService.GetFirst6Education();
        }

        [HttpGet]
        public IResult UserEducations(int id)
        {
            return _educationService.UserEducations(id);
        }

        [HttpGet]
        public IResult GetByTeacherId(int id)
        {
            return _educationService.getByTeacherId(id);
        }

        [HttpGet]
        public IResult GetUserEducationsCount(int id)
        {
            return _educationService.GetUserEducationsCount(id);
        }

        [HttpGet]
        public IResult GetUserEducationsMonthlyCount(int id)
        {
            return _educationService.GetUserEducationsCount(id);
        }

        [HttpGet]
        public bool GetExamStatus(int userId, int educationId)
        {
            return _educationService.GetExamStatus(userId, educationId);

        }

    }
}