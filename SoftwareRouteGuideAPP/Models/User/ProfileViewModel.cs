using SoftwareRouteGuideAPP.Models.Educations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Models.User
{
    public class ProfileViewModel
    {

        public EducationCounts educationCounts { get; set; }
        public IEnumerable<Education> education { get; set; }
    }
}
