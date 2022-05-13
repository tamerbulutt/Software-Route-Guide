using System.Collections.Generic;
using SoftwareRouteGuideAPI.Model.Education;

namespace SoftwareRouteGuideAPI.Repositories.Abstract
{
    public interface ICommentRepository:IBaseRepository<Comment>
    {
        List<Comment> getCommentsByEducationId(int commentId);
    }
}