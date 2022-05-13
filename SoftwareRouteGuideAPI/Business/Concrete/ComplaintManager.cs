using System.Collections.Generic;
using AutoMapper;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class ComplaintManager : IComplaintService
    {
        private IMapper _mapper;
        private IComplaintRepository _complaintRepository;
        public ComplaintManager(IMapper mapper, IComplaintRepository complaintRepository)
        {
            _mapper = mapper;
            _complaintRepository = complaintRepository;
        }

        public IResult add(ComplaintAddDto entity)
        {
                Complaint complaint = _mapper.Map<Complaint>(entity);
                if(_complaintRepository.Add(complaint))
                    return new Result(true, "Şikayet başarıyla eklendi!");
                else
                    return new Result(false, "Şikayet eklenirken bir hata oluştu!");
        }

        public IResult delete(int id)
        {
            if(id > 0){
                if(_complaintRepository.Delete(id)){
                    return new Result(true, "Şikayet başarıyla silindi!");
                }
                return new Result(false,"Şikayet silinirken bir hata oluştu!");
            }
            return new Result(false,"Geçersiz ID değeri yolladınız!");
        }

        public IResult getAll()
        {
            var complaintList = _complaintRepository.GetAll();
            if(complaintList.Count > 0)
                return new DataResult<List<Complaint>>(complaintList,true,"Şikayetler başarıyla listelendi.");
            else
                return new Result(false,"Hiç şikayet bulunamadı!");
        }

        public IResult getById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IResult update(Complaint entity)
        {
            throw new System.NotImplementedException();
        }
    }
}