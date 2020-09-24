using Microsoft.AspNetCore.Mvc.Rendering;
using QuizMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Services
{
   public interface IUnitOfWork
    {
        void AddSubject(Subject model);
        void AddQuestion(Question model);
        void AddExam(Examination model);
        IEnumerable<SelectListItem> GetSubjects();
        IEnumerable<Question> GetQuestions();
        Task Save();
       
    }
}
