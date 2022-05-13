using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoftwareRouteGuideAPI.Business.Abstract;
using SoftwareRouteGuideAPI.Helpers.Results;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Exam;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Business.Concrete
{
    public class QuestionManager : IQuestionService
    {
        IMapper _mapper;
        IQuestionRepository _questionRepository;
        IAnswerRepository _answerRepository;

        public QuestionManager(IMapper mapper, IQuestionRepository questionRepository, 
                                                IAnswerRepository answerRepository)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }
        
        public IResult AddQuestions(List<QuestionDto> questions)
        {
            List<Question> mappedQuestions = new();
            questions.ForEach(q => { mappedQuestions.Add(_mapper.Map<Question>(q)); });

            for (int i = 0; i < questions.Count; i++)
            {
                long lastId = _questionRepository.AddQuestion(mappedQuestions[i]);
                questions[i].answers.ForEach(a => { a.questionID = (int)lastId; });
                _answerRepository.addAnswers(questions[i].answers);
            }
            return new Result(true,"Tüm sorular ve cevapları başarıyla eklendi.");
        }

        public IResult FinishExam(CalculateModel model)
        {
            int totalScore = _questionRepository.CheckAnswers(model.answers);
            if (_questionRepository.FinishExam(model,totalScore))
            {
                return new Result(true, $"Sınavınız kaydedildi. Puanınız: {totalScore}");
            }
            return new Result(false, "Sınavınız kaydedilirken bir hata oluştu.");
        }

        public IResult getQuestions(int educationID)
        {
            List<QuestionDto> questions = new();
            var questionList = _questionRepository.getQuestions(educationID);
            questionList.ForEach(q => { questions.Add( _mapper.Map<QuestionDto>(q) ); });
            foreach (var q in questions){
                q.answers = _answerRepository.getAnswers(q.questionID);
            }
            return new DataResult<List<QuestionDto>>(questions,true,"Tüm sorular döndürüldü.");
        }

        public IResult UpdateQuestions(List<QuestionDto> questions)
        {
            throw new System.NotImplementedException();
        }
    }
}