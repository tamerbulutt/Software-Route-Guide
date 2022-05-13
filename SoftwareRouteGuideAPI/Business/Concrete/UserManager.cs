using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.DBO;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.Identity;
using SoftwareRouteGuideAPI.Repositories.Abstract;
using SoftwareRouteGuideAPI.Helpers.Functions;
using SoftwareRouteGuideAPI.Model.DTOs;
using AutoMapper;
using System;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class UserManager : IUserService
    {
        IMapper _mapper;
        IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        DBO dbo;
        HelperFunctions helperFunctions;

        public UserManager(IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
            dbo = new(_configuration);
            helperFunctions = new(_configuration);
        }

        public IResult add(UserRegisterDto user)
        {
            //Boş veri olup olmadığını kontrol ediyoruz
            if (user.email != null && user.firstName != null && user.lastName != null && user.password != null)
            {
                if (!dbo.EmailCheck(user.email))
                {
                    User userr = _mapper.Map<User>(user);
                    //Kullanıcı ekleme işleminin başarılı olup olmadığını kontrol ediyoruz
                    if (!_userRepository.Add(userr)) //Veritabanına ekleme işlemi
                        return new Result(false, "Kullanıcı eklenirken bir hata oluştu!");
                    else
                        return new Result(true, "Kullanıcı başarıyla eklendi!");
                }
                else
                    return new Result(false, "Girilen eposta adresi başkası tarafından kullanılmaktadır!");
            }
            else
                return new Result(false, "Lütfen tüm verileri eksiksiz giriniz!");
        }

        public IResult update(User entity)
        {
            var check = false;

            //Boş veri olup olmadığını kontrol ediyoruz
            if (entity.user_id != 0 && entity.email != null && entity.firstName != null && entity.lastName != null && entity.password != null && entity.userRoleId != null)
            {
                if (dbo.CheckUserToken(entity.email, entity.token))
                {
                    check = _userRepository.Update(entity); //Güncelleme işlemi
                }
                else
                    return new Result(false, "401 Unauthorized");

            }
            else
                return new Result(false, "Lütfen tüm verileri eksiksiz giriniz!");

            //Kullanıcı ekleme işleminin başarılı olup olmadığını kontrol ediyoruz
            if (!check)
                return new Result(false, "Kullanıcı güncellenirken bir hata oluştu!");
            else
                return new Result(true, "Kullanıcı başarıyla güncellendi!");
        }

        public IResult delete(int id)
        {
            if (id > 0)
            {
                if (_userRepository.Delete(id))
                {
                    return new Result(true, "Kullanıcı başarıyla silindi!");
                }
                return new Result(false, "Kullanıcı silinirken bir hata oluştu!");
            }
            return new Result(false, "Geçersiz ID değeri yolladınız!");
        }

        public IResult getAll()
        {
            var userList = _userRepository.GetAll();
            if (userList.Count > 0)
                return new DataResult<List<User>>(userList, true, "Kullanıcılar başarıyla listelendi.");
            else
                return new Result(false, "Hiç kullanıcı bulunamadı!");
        }

        public IResult getById(int id)
        {

            List<User> user = _userRepository.GetById(id);
            if (user.Count > 0)
                return new DataResult<User>(user[0], true, "Kullanıcı bulundu.");
            else
                return new Result(false, "Kullanıcı bulunamadı!");
        }

        public IResult UserLogin(UserLoginDto login)
        {
            //Tüm verilerin eksiksiz girilme kontrolünü sağlıyoruz
            if (!string.IsNullOrEmpty(login.email) && !string.IsNullOrEmpty(login.password))
            {
                User user = _mapper.Map<User>(login);
                if (_userRepository.UserLogin(user)) //Girilen kullanıcı adı ile şifre eşleşiyor mu kontrolü
                {
                    var token = helperFunctions.CreateToken(user); //Token oluşturuyoruz
                    var saveToken = dbo.SaveUserToken(user.email, token); //Token'ı veritabanına kaydediyoruz çünkü kişiye özgü olacak
                    user = _userRepository.getByEmail(user.email);
                    if (user != null)
                    {
                        LoginReturnDto loginReturnDto = _mapper.Map<LoginReturnDto>(user);
                        return new DataResult<LoginReturnDto>(loginReturnDto, true, "Giriş için gerekli bilgiler gönderildi.");
                    }
                    return new Result(false, "Bir hata oluştu!");

                }
                else
                    return new Result(false, "Kullanıcı adı ya da parola hatalı");
            }
            else
                return new Result(false, "Tüm bilgileri eksiksiz giriniz.");
        }

        public IResult ChangePassword(ChangePasswordDto entity)
        {
            User user = _userRepository.GetById(entity.userID)[0];
            if (user.password == entity.currentPassword)
            {
                user.password = entity.password;
                if (_userRepository.Update(user))
                    return new Result(true, "Şifreniz başarıyla değiştirildi.");
                else
                    return new Result(false, "Şifreniz değiştirilirken bir hata meydana geldi.");
            }
            else
                return new Result(false, "Mevcut şifrenizi yanlış girdiniz!"); 
        }

        public IResult DeleteAccount(DeleteAccountDto entity)
        {
            User user = _userRepository.GetById(entity.userID)[0];
            if (entity.password == user.password)
            {
                if(_userRepository.Delete(entity.userID))
                    return new Result(true,"Hesap ve tüm veriler başarıyla kaldırıldı.");
                else
                    return new Result(false,"Verileriniz silinirken bir hata meydana geldi.");
            }
            return new Result(false,"Yanlış şifre girdiniz!");
        }
    }
}