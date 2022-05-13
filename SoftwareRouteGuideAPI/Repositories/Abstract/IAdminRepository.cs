using SoftwareRouteGuideAPI.Model.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface IAdminRepository
    {
        bool AdminLogin(Admin entity);

        public Admin getByEmail(string email);
    }
}
