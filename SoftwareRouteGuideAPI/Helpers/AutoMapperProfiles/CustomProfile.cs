using AutoMapper;
using SoftwareRouteGuideAPI.Model.Admin;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Model.Exam;
using SoftwareRouteGuideAPI.Model.Identity;

namespace SoftwareRouteGuideAPI.Helpers.AutoMapperProfiles
{
    public class CustomProfile:Profile
    {
        public CustomProfile()
        {
            CreateMap<UserLoginDto,User>();
            CreateMap<User,UserLoginDto>();

            CreateMap<AdminLoginDTO, Admin>();
            CreateMap<Admin, AdminLoginDTO>();

            CreateMap<UserRegisterDto,User>();
            CreateMap<User,UserRegisterDto>();

            CreateMap<AdminLoginReturnDTO, Admin>();
            CreateMap<Admin, AdminLoginReturnDTO>();

            CreateMap<LoginReturnDto,User>();
            CreateMap<User,LoginReturnDto>()
                .ForMember(x => x.fullname, opt => opt.MapFrom(z => z.firstName+" "+z.lastName));

            CreateMap<EducationAddDto,Education>();
            CreateMap<Education,EducationAddDto>();

            CreateMap<EducationSuggestDto, Education>();
            CreateMap<Education, EducationSuggestDto>();

            CreateMap<SuggestStatusDto, Education>();
            CreateMap<Education, SuggestStatusDto>();

            CreateMap<CommentAddDto,Comment>();
            CreateMap<Comment,CommentAddDto>();

            CreateMap<ComplaintAddDto,Complaint>();
            CreateMap<Complaint,ComplaintAddDto>();

            CreateMap<QuestionDto,Question>();
            CreateMap<Question,QuestionDto>();
        }
    }
}