using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Models.Contact
{
    public class Contacts
    {
        public int complaintID { get; set; }
        public string Fullname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string message { get; set; }
        public int userID { get; set; }
    }
}
