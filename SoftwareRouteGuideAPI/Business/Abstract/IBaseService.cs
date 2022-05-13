using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.Identity;

namespace SoftwareRouteGuideAPI.Business.Abstract
{
    public interface IBaseService<T,A>
    {
        IResult add(A entity);
        IResult update(T entity);
        IResult delete(int id);
        IResult getById(int id);
        IResult getAll();
    }
}