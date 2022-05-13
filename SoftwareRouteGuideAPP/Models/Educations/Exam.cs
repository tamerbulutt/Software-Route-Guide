using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Models.Educations
{
    public class Exam
    {
        public int questionID { get; set; }
        public string Description { get; set; }
        public string choice1 { get; set; }
        public string choice2 { get; set; }
        public string choice3 { get; set; }
        public string trueAnswer { get; set; }
        public int educationID { get; set; }
    }
}
