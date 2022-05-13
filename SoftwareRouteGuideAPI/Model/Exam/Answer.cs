namespace SoftwareRouteGuideAPI.Model.Exam
{
    public class Answer
    {
        public int answerID { get; set; }
        public string answer { get; set; }
        public bool isCorrect { get; set; }
        public int questionID { get; set; }
    }
}