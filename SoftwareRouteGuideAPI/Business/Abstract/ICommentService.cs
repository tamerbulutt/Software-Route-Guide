using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface ICommentService:IBaseService<Comment,CommentAddDto>
    {
        IResult getCommentsByEducationId(int id);
    }
}