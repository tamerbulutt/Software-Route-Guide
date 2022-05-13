using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface ITeacherService 
    {
        IResult allTeachers();
        IResult AllApplications();
        IResult GetTeacherByID(int id);
        IResult TeacherApplication(TeacherApplicationDTO teacher);
        IResult ApproveApplication(int id);
        IResult RejectApplication(int id);
    }
}
