using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class EducationManager : IEducationService
    {
        private IMapper _mapper;
        private IEducationRepository _educationRepository;
        public EducationManager(IMapper mapper, IEducationRepository educationRepository)
        {
            _mapper = mapper;
            _educationRepository = educationRepository;
        }
        public IResult add(EducationAddDto entity)
        {
          
            Education education = _mapper.Map<Education>(entity);

            if(_educationRepository.Add(education))
                return new Result(true, "Eğitim başarıyla eklendi!");
            else
                return new Result(false, "Eğitim eklenirken bir hata oluştu!");
     
        }
        public IResult delete(int id)
        {
            if(id > 0){
                if(_educationRepository.Delete(id)){
                    return new Result(true, "Eğitim başarıyla silindi!");
                }
                return new Result(false,"Eğitim silinirken bir hata oluştu!");
            }
            return new Result(false,"Geçersiz ID değeri yolladınız!");
        }

        public IResult Suggest(EducationAddDto education)
        {
       
            Education educationSuggest = _mapper.Map<Education>(education);

            if (_educationRepository.Suggest(educationSuggest))
                return new Result(true, "Eğitim başarıyla eklendi!");
            else
                return new Result(false, "Eğitim eklenirken bir hata oluştu!");

        }
        
        public IResult AllSuggests()
        {
            var allSuggests = _educationRepository.AllSuggests();
            if (allSuggests.Count > 0)
                return new DataResult<List<Education>>(allSuggests, true, "Tüm eğitim önerileri");
            else
                return new Result(false, "Hiç bir eğitim önerisi bulunmamaktadır");
            
        }

        public IResult ChangeSuggestStatus(SuggestStatusDto educationSuggest)
        {
            if (!string.IsNullOrEmpty(educationSuggest.status))
            {
                if(_educationRepository.ChangeSuggestStatus(educationSuggest))
                    return new Result(true, "Eğitim önerisi tercihiniz doğrulanmıştır");
                else
                    return new Result(false, "Eğitim önerisi tercihiniz doğrulanırken bir takım hatalar oluştu.");
            }
            else
                return new Result(false, "Lütfen tüm bilgileri eksiksiz giriniz");


        }
        //update metodunun validation kısmını üşendiğim için yazmadım. yazılacak...
        public IResult update(Education entity)
        {
            _educationRepository.Update(entity);
            return new Result(true,"Kullanıcı başarıyla güncellendi");
        }
        public IResult getAll()
        {
            var educationList = _educationRepository.GetAll();
            if(educationList.Count > 0)
                return new DataResult<List<Education>>(educationList,true,"Eğitimler başarıyla listelendi.");
            else
                return new Result(false,"Hiç eğitim bulunamadı!");
        }
        public IResult getById(int id)
        {
            List<Education> education = _educationRepository.GetById(id);
            if(education.Count > 0)
                return new DataResult<Education>(education[0],true,"Eğitim bulundu.");
            else
                return new Result(false,"Eğitim bulunamadı!");
        }

        public IResult getByTeacherId(int id)
        {
            List<Education> education = _educationRepository.GetByTeacherId(id);
            if(education.Count > 0)
                return new DataResult<List<Education>>(education,true,"Eğitimler bulundu.");
            else
                return new Result(false,"Eğitim bulunamadı!");
        }

        /*public IResult rateEducation(int point, int educationId)
        {
            Education education = _educationRepository.GetById(educationId).FirstOrDefault();
            if(education != null){
                var average = education.point * education.pointCount;
                average += point;
                education.pointCount++;
                education.point = average / education.pointCount;
                _educationRepository.Update(education);
            }
            return new DataResult<float>(education.point,true,"Puanlama başarılı");
        }*/

        public IResult GetLast2Education()
        {
            var educationList = _educationRepository.GetLast2Education();
            if (educationList.Count > 0)
                return new DataResult<List<Education>>(educationList, true, "Eğitimler başarıyla listelendi.");
            else
                return new Result(false, "Hiç eğitim bulunamadı!");
        }
        public IResult GetFirst6Education()
        {
            var educationList = _educationRepository.GetFirst6Education();
            if (educationList.Count > 0)
                return new DataResult<List<Education>>(educationList, true, "Eğitimler başarıyla listelendi.");
            else
                return new Result(false, "Hiç eğitim bulunamadı!");
        }
        
        public IResult UserEducations(int id)
        {
            var educationList = _educationRepository.UserEducations(id);
            if (educationList.Count > 0)
                return new DataResult<List<Education>>(educationList, true, "Eğitimler başarıyla listelendi.");
            else
                return new Result(false, "Hiç eğitim bulunamadı!");
        }
        
        public IResult GetUserEducationsCount(int id)
        {
            var educationList = _educationRepository.GetUserEducationsCount(id);

            if (educationList > 0)
                return new Result(true, educationList.ToString());
            else
                return new Result(false, "Hiç eğitim bulunamadı!");
        }

        public IResult GetUserEducationsMonthlyCount(int id)
        {
            var educationList = _educationRepository.GetUserEducationsMonthlyCount(id);

            if (educationList > 0)
                return new Result(true, educationList.ToString());
            else
                return new Result(false, "Hiç eğitim bulunamadı!");
        }

        public bool GetExamStatus(int userId, int educationId)
        {
            bool examStatus = _educationRepository.GetExamStatus(userId,educationId);

            return examStatus;
        }

    }
}