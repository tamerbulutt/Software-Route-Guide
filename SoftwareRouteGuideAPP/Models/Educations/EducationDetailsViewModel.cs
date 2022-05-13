using SoftwareRouteGuideAPP.Models.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Models.Educations
{
    public class EducationDetailsViewModel
    {

        public Education education { get; set; }
        public IEnumerable<Comments> comments { get; set; }
        public IEnumerable<Teachers> teacher { get; set; }
        
        public bool examStatus { get; set; }

    }
}
