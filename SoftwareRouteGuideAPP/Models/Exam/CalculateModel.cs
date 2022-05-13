using System.Collections.Generic;

namespace SoftwareRouteGuideAPP.Models.Exam
{
    public class CalculateModel
    {
        public int educationID { get; set; }
        public int userID { get; set; }
        public List<int> answers { get; set; }
    }
}