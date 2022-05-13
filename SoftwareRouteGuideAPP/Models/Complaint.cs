using System;

namespace SoftwareRouteGuideAPP.Models
{
    public class Complaint
    {
        public int complaintID { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
        public int userID { get; set; }
    }
}