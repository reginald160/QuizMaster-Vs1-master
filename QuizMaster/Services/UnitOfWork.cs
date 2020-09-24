using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizMaster.Data;
using QuizMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddExam(Examination model)
        {
            _context.Examinations.Add(model);
        }

        public void AddQuestion(Question model)
        {
          var result = _context.Questions.Count() + 1;
            var question = new Question
            { Id = model.Id,
             CorrectAnswer = model.CorrectAnswer,
             OptionA = model.OptionA,
             OptionB = model.OptionB,
             OptionC = model.OptionC,
             OptionD = model.OptionD,
             QuestText = model.QuestText,
             QuestionNumber = result,
             Subject = model.Subject,
             SubjectId = model.SubjectId

            };
            
            _context.Questions.Add(question);
        }

       
        public void AddSubject(Subject model)
        {
            _context.Subjects.Add(model);
        }

        public IEnumerable<SelectListItem> GetSubjects()
        {
            var result = _context.Subjects.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return result;
        }
        public IEnumerable<Question> GetQuestions()
        {
            return _context.Questions.Include(x => x.Subject).ToList();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
