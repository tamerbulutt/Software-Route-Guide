using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface IAdminService
    {
        IResult AdminLogin(AdminLoginDTO entity);
    }
}
