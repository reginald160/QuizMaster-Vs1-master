using QuizMaster.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.ViewModels
{
    public class ExaminationViewModel 
    {
        [Display(Name = "Candidate")]
       
        public int Answer { get; set; }
        public int Option { get; set; }
        public int QuestionId { get; set; }














    }
}
