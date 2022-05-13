using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Models.User
{
    public class Register
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string Telephone { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
    }
}
