using AutoMapper;
using Microsoft.Extensions.Configuration;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.DBO;
using SoftwareRouteGuideAPI.Helpers.Functions;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.Admin;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Identity;
using SoftwareRouteGuideAPI.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class AdminManager : IAdminService
    {
        IMapper _mapper;
        IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        HelperFunctions helperFunctions;
        DBO dbo;

        public AdminManager(IMapper mapper, IAdminRepository adminRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
            _configuration = configuration;
            dbo = new(_configuration);
            helperFunctions = new(_configuration);
        }


        public IResult AdminLogin(AdminLoginDTO login)
        {
            //Tüm verilerin eksiksiz girilme kontrolünü sağlıyoruz
            if (!string.IsNullOrEmpty(login.email) && !string.IsNullOrEmpty(login.password))
            {
                Admin admin = _mapper.Map<Admin>(login);
                if (_adminRepository.AdminLogin(admin)) //Girilen kullanıcı adı ile şifre eşleşiyor mu kontrolü
                {
                    User mapUser = new();
                    mapUser.email = login.email;
                    var token = helperFunctions.CreateToken(mapUser); //Token oluşturuyoruz
                    var saveToken = dbo.SaveAdminToken(admin.Email, token); //Token'ı veritabanına kaydediyoruz çünkü kişiye özgü olacak
                    var adminInfo = _adminRepository.getByEmail(admin.Email);

                    if (admin != null)
                    {
                        AdminLoginReturnDTO loginReturnDto = _mapper.Map<AdminLoginReturnDTO>(adminInfo);
                        return new DataResult<AdminLoginReturnDTO>(loginReturnDto, true, "Giriş için gerekli bilgiler gönderildi.");
                    }
                    return new Result(false, "Bir hata oluştu!");

                }
                else
                    return new Result(false, "Kullanıcı adı ya da parola hatalı");
            }
            else
                return new Result(false, "Tüm bilgileri eksiksiz giriniz.");
        }
    }
}
