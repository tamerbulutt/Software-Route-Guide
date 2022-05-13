using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Identity;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface IUserService:IBaseService<User,UserRegisterDto>
    {
        IResult UserLogin(UserLoginDto entity);
        IResult ChangePassword(ChangePasswordDto entity);
        IResult DeleteAccount(DeleteAccountDto entity);

    }
}