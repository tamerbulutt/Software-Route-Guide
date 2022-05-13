using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Models.Educations
{
    public class EducationViewModel
    {

        public IEnumerable<Education> educationFirst6 { get; set; }
        public IEnumerable<Education> educationLast2 { get; set; }
    }
}
