using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Identity;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface IUserRepository:IBaseRepository<User>
    {
        bool UserLogin(User entity);
        User getByEmail(string email);
    }
}