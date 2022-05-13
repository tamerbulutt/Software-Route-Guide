using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IResult AllTeachers()
        {
            return _teacherService.allTeachers();
        }

        [HttpGet]
        public IResult GetTeacherByID(int id)
        {
            return _teacherService.GetTeacherByID(id);
        }

        [HttpPost]
        public IResult TeacherApplication(TeacherApplicationDTO teacher)
        {
            return _teacherService.TeacherApplication(teacher);
        }

        [HttpGet]
        public IResult AllApplications()
        {
            return _teacherService.AllApplications();
        }

        [HttpPost]
        public IResult ApproveApplication(int id)
        {
            return _teacherService.ApproveApplication(id);
        }

        [HttpPost]
        public IResult RejectApplication(int id)
        {
            return _teacherService.RejectApplication(id);
        }
    }
}
