using System;

namespace SoftwareRouteGuideAPI.Model.Education
{
    public class Complaint
    {
        public int complaintID { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Message { get; set; }
        public DateTime date { get; set; }
        public int userID { get; set; }    
    }
}