using System;

namespace SoftwareRouteGuideAPP.Models
{
    public class UserSessionInfo
    {
        public int user_id { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public DateTime registerDate { get; set; }
        public int userRoleId { get; set; }
        public string token { get; set; }
        public string userType { get; set; }
    }
}