using System.Collections.Generic;
using SoftwareRouteGuideAPI.Model.Exam;

namespace SoftwareRouteGuideAPI.Model.DTOs
{
    public class QuestionDto
    {
        public int questionID { get; set; }
        public string question { get; set; }
        public List<Answer> answers { get; set; }
        public int educationID { get; set; }
    }
}