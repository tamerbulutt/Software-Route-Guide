using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface ITeacherRepository
    {
        List<Teachers> allTeachers();
        List<ApplicationDetails> AllApplications();
        List<Teachers> GetTeacherByID(int id);
        bool TeacherApplication(TeacherApplicationDTO teacher);
        bool ApproveApplication(int id);
        bool RejectApplication(int id);

    }
}
