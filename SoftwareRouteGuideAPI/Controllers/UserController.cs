using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost][AllowAnonymous]
        public IResult Register([FromBody] UserRegisterDto register)
        {
            return _userService.add(register);
        }

        [HttpPost]
        public IResult ChangePassword([FromBody] ChangePasswordDto model)
        {
            return _userService.ChangePassword(model);
        }

        [HttpPost]
        public IResult DeleteAccount([FromBody] DeleteAccountDto model)
        {
            return _userService.DeleteAccount(model);
        }
    }
}
