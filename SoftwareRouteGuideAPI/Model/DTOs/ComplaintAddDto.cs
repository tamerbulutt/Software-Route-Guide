namespace SoftwareRouteGuideAPI.Model.DTOs
{
    public class ComplaintAddDto
    {
        public string Fullname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string message { get; set; }
        public int userID { get; set; }
    }
}