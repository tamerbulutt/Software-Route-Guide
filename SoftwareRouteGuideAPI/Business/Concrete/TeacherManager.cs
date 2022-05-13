using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Teacher;
using SoftwareRouteGuideAPI.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class TeacherManager : ITeacherService
    {
        private ITeacherRepository _teacherRepository;

        public TeacherManager(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public IResult allTeachers()
        {
            var allTeachers = _teacherRepository.allTeachers();
            if (allTeachers.Count > 0)
                return new DataResult<List<Teachers>>(allTeachers, true, "Tüm öğretmenler");
            else
                return new Result(false, "Hiç bir öğretmen bulunmamaktadır");
        }

        public IResult AllApplications()
        {
            var allApplications = _teacherRepository.AllApplications();
            if (allApplications.Count > 0)
                return new DataResult<List<ApplicationDetails>>(allApplications, true, "Tüm başvurular");
            else
                return new Result(false, "Hiç bir başvuru bulunmamaktadır");
        }

        public IResult GetTeacherByID(int id)
        {
            var teacher = _teacherRepository.GetTeacherByID(id);

            if (teacher.Count > 0)
                return new DataResult<List<Teachers>>(teacher, true, "Tüm öğretmenler");
            else
                return new Result(false, "Hiç bir öğretmen bulunmamaktadır");
        }

        public IResult TeacherApplication(TeacherApplicationDTO teacher)
        {
            if (_teacherRepository.TeacherApplication(teacher))
                return new Result(true, "Başvuru başarılı!");
            else
                return new Result(false, "Başvuru esnasında hata oluştu!");
        }

        public IResult ApproveApplication(int id)
        {
            if (_teacherRepository.ApproveApplication(id))
                return new Result(true, "Başvuru onaylandı");
            else
                return new Result(false, "Başvurunun onaylanması esnasında hata oluştu!");
        }
        public IResult RejectApplication(int id)
        {
            if (_teacherRepository.RejectApplication(id))
                return new Result(true, "Başvuru reddedildi");
            else
                return new Result(false, "Başvurunun reddedilmesi esnasında hata oluştu!");
        }
        
    }
}
