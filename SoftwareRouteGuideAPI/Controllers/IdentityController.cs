using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Model.Identity;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using Microsoft.AspNetCore.Authorization;
using SoftwareRouteGuideAPI.Model.DTOs;
using System;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost][AllowAnonymous]
        public IResult Login([FromBody] UserLoginDto login)
        {
            return _userService.UserLogin(login);
        }

        [HttpGet]
        public IResult AllUsers(){
            return _userService.getAll();
        } 

       
        [HttpGet]
        public IResult UserInfo(int id){
            return _userService.getById(id);
        }

        [HttpPut]
        public IResult Update([FromBody] User user) {
            return _userService.update(user);
        } //Veritabanındaki bir satırı güncellemek için kullanılıyor (Örnek) (UPDATE)

        [HttpDelete]
        public IResult Delete(int id){
            return _userService.delete(id);
        } //Veritabanındaki bir satırı silmek için kullanılan fonksiyon (Örnek) (DELETE)
    }
}
