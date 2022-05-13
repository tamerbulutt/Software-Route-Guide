using System.Collections.Generic;

namespace SoftwareRouteGuideAPP.Models.Exam
{
    public class QuestionDto
    {
        public int questionID { get; set; }
        public string question { get; set; }
        public List<Answer> answers { get; set; }
        public int educationID { get; set; }
    }
}