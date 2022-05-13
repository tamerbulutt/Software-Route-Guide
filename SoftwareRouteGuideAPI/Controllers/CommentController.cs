using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;

namespace SoftwareRouteGuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public IResult Add([FromBody] CommentAddDto comment){
            return _commentService.add(comment);
        }

        [HttpDelete]
        public IResult Delete(int id){
            return _commentService.delete(id);
        } //Veritabanındaki bir satırı silmek için kullanılan fonksiyon (Örnek) (DELETE)

        [HttpGet]
        public IResult GetCommentsByEducationId(int id){
            return _commentService.getCommentsByEducationId(id);
        }



    }
}