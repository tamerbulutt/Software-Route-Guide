using SoftwareRouteGuideAPI.Model.Identity;
using System.Collections.Generic;

namespace SoftwareRouteGuideAPI.Repositories
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(T entity);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<T> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

    }
}