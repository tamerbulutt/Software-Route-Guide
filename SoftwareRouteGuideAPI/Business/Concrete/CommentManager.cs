using System;
using System.Collections.Generic;
using AutoMapper;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private IMapper _mapper;
        private ICommentRepository _commentRepository;
        public CommentManager(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }
        public IResult add(CommentAddDto entity)
        {
            Console.WriteLine("dsfsd"+entity.fullname);
            Console.WriteLine("dsfsd2" + entity.content);
            Console.WriteLine("dsfsd3" + entity.educationID);
            if(entity.fullname != null && entity.content != null && entity.educationID != 0)
            {
                Comment comment = _mapper.Map<Comment>(entity);
                if(_commentRepository.Add(comment))
                    return new Result(true, "Yorum başarıyla eklendi!");
                else
                    return new Result(false, "Yorum eklenirken bir hata oluştu!");
            }
            else
                return new Result(false, "Lütfen tüm verileri eksiksiz giriniz!");
        }

        public IResult delete(int id)
        {
            if(id > 0){
                if(_commentRepository.Delete(id)){
                    return new Result(true, "Yorum başarıyla silindi!");
                }
                return new Result(false,"Yorum silinirken bir hata oluştu!");
            }
            return new Result(false,"Geçersiz ID değeri yolladınız!");
        }

        public IResult getCommentsByEducationId(int id)
        {
            var commentList = _commentRepository.getCommentsByEducationId(id);
            if(commentList.Count > 0)
                return new DataResult<List<Comment>>(commentList,true,"Yorumlar başarıyla listelendi.");
            else
                return new Result(false,"Hiç yorum bulunamadı!");
        }

        public IResult update(Comment entity)
        {
            throw new System.NotImplementedException();
        }
        
        public IResult getAll()
        {
            throw new System.NotImplementedException();
        }

        public IResult getById(int id)
        {
            throw new System.NotImplementedException();
        }

    }
}