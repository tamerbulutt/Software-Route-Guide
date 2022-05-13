using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftwareRouteGuideAPP.Helpers;
using SoftwareRouteGuideAPP.Models;
using SoftwareRouteGuideAPP.Models.Admin;
using SoftwareRouteGuideAPP.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPP.Controllers
{
    public class AdminController : Controller
    {
        WebRequestHelper webRequestHelper;
        public AdminController()
        {
            webRequestHelper = new();
        }

        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdmin(Login login)
        {
            if (!string.IsNullOrEmpty(login.email) && !string.IsNullOrEmpty(login.password))
            {
                var query = new { login.email, login.password }; //Query için string

                var response = await webRequestHelper.PostBuilder("Admin/AdminLogin", query); //HttpRequest

                var responseObject = JsonConvert.DeserializeObject<ResponseModel<AdminGlobal>>(response);
                bool isSuccess = false;

                //Eğer apiden cevap döndüyse
                if (!string.IsNullOrEmpty(responseObject.ToString()))
                    isSuccess = responseObject.success;

                if (isSuccess)
                {
                    //Session'a değerleri kayıt ediyoruz
                    HttpContext.Session.SetString("token", responseObject.data.token);
                    HttpContext.Session.SetString("fullName", responseObject.data.Name+ " "+responseObject.data.Surname);
                    HttpContext.Session.SetInt32("userID", responseObject.data.adminID);
                    HttpContext.Session.SetString("userType", "Admin");

                    return RedirectToAction("index", "home");
                }

            }
            return View();
        }
    }
}
