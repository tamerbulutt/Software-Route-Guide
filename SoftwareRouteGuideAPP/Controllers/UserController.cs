using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoftwareRouteGuideAPP.Helpers;
using SoftwareRouteGuideAPP.Models;
using SoftwareRouteGuideAPP.Models.Educations;
using SoftwareRouteGuideAPP.Models.User;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class UserController : Controller
    {

        WebRequestHelper webRequestHelper;
        UserSessionInfo session;
        public UserController()
        {
            webRequestHelper = new();
            session = new();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Register(Register register)
        {
            //Boş data olmasını engelliyoruz
            if (!string.IsNullOrEmpty(register.email) && !string.IsNullOrEmpty(register.firstName) && !string.IsNullOrEmpty(register.lastName) && !string.IsNullOrEmpty(register.password) && !string.IsNullOrEmpty(register.passwordConfirm))
            {
                if (register.password == register.passwordConfirm) //Girilen parolaların eşleşmesi kontrolünü sağlıyoruz
                {
                 
                    var query = new { register.firstName, register.lastName, register.email, register.Telephone,register.password }; //Query için string
                    var response = await webRequestHelper.PostBuilder("User/Register", query); //HttpRequest
                    return Json(response);
                }
            }
            return Json(null);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!string.IsNullOrEmpty(login.email) && !string.IsNullOrEmpty(login.password))
            {
                var query = new { login.email, login.password }; //Query için string

                var response = await webRequestHelper.PostBuilder("Identity/Login", query); //HttpRequest

                var responseObject = JsonConvert.DeserializeObject<ResponseModel<UserSessionInfo>>(response);
                bool isSuccess = false;

                //Eğer apiden cevap döndüyse
                if (!string.IsNullOrEmpty(responseObject.ToString()))
                    isSuccess = responseObject.success;

                if (isSuccess)
                {
                    //Session'a değerleri kayıt ediyoruz
                    HttpContext.Session.SetString("token", responseObject.data.token);
                    HttpContext.Session.SetString("fullName", responseObject.data.fullname);
                    HttpContext.Session.SetInt32("userID", responseObject.data.user_id);
                    HttpContext.Session.SetString("userType", responseObject.data.userType);

                    return RedirectToAction("index", "home");
                }

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto passwordModel)
        {
            if (HttpContext.Session.GetInt32("userID") == passwordModel.userID)
            {
                var response = await webRequestHelper.PostBuilder("User/ChangePassword", passwordModel); //HttpRequest
                return Json(response);
            }
            return Json("");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(DeleteAccountDto model)
        {
            if (HttpContext.Session.GetInt32("userID") == model.userID)
            {
                var response = await webRequestHelper.PostBuilder("User/DeleteAccount", model); //HttpRequest
                bool check = JObject.Parse(response).SelectToken("success").ToObject<bool>();
                if(check) {
                    Console.WriteLine("BAŞARILI");
                    HttpContext.Session.Clear();
                }                
                return Json(response);
            }
            else{
                Console.WriteLine("///"+HttpContext.Session.GetInt32("userID"));
                Console.WriteLine("///"+model.userID);
                return Json(new Result{Success=false,Message="Bu alana giriş yetkiniz yok!"});
            }
                
            
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            if (HttpContext.Session.GetInt32("userID") == id)
            {
                var educations = await webRequestHelper.GetBuilder($"Education/UserEducations?id={id}");
                var educationCount = await webRequestHelper.GetBuilder($"Education/GetUserEducationsCount?id={id}");
                var educationMonthlyCount = await webRequestHelper.GetBuilder($"Education/GetUserEducationsMonthlyCount?id={id}");

                EducationCounts educationCounts = new();
                ProfileViewModel profileViewModel = new();

                int educationCountResponse = 0;
                int educationMountlyCountResponse = 0;

                List<Education> responseData;
                if(JObject.Parse(educations).SelectToken("data") != null)
                {
                    responseData = JObject.Parse(educations).SelectToken("data").ToObject<List<Education>>();
                    profileViewModel.education = responseData;

                    educationCountResponse = Convert.ToInt32(JObject.Parse(educationCount).SelectToken("message").ToString());
                    educationMountlyCountResponse = Convert.ToInt32(JObject.Parse(educationMonthlyCount).SelectToken("message").ToString());

                    educationCounts.educationCount = educationCountResponse;
                    educationCounts.educationMonthlyCount = educationMountlyCountResponse;
                    educationCounts.educationScore = educationCountResponse * 10;
                    profileViewModel.educationCounts = educationCounts;

                }
                else
                {
                    educationCounts.educationCount = 0;
                    educationCounts.educationMonthlyCount = 0;
                    educationCounts.educationScore = 0;
                    profileViewModel.educationCounts = educationCounts;

                }

                return View(profileViewModel);
            }
            else
                return RedirectToAction("login");
        }

        [HttpGet]
        public IActionResult ProfileSettings()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}
