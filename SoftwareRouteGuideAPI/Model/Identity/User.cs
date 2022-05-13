using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Model.Identity
{
    public class User
    {
        public int user_id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string Telephone { get; set; }
        public string password { get; set; }
        public DateTime registerDate { get; set; }
        public int? userRoleId { get; set; }
        public string token { get; set; }
        public string userType { get; set; }
    }
}
