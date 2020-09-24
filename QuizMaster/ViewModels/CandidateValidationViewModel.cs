using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.ViewModels
{
    public class CandidateValidationViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required/*, MaxLength(8)*/]
        public string ExamCode { get; set; }
    }
}
