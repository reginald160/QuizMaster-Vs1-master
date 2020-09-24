using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class Score : BaseModel
    {
        public string ExamCode { get; set; }
        public DateTime TestDate { get; set; }
        public int CandidateScore  { get; set; }
        public string CandidateName { get; set; }
       
    }
}
