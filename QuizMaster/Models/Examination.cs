using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class Examination : BaseModel
    {
        [Display(Name = "Candidate")]
        public int CandidateId { get; set; }
        [JsonIgnore]
        [ForeignKey("CandidateId")]
        public virtual Candidate Candidate { get; set; }
        [Display(Name= "Questions")]
        public int QuestionId { get; set; }
        [JsonIgnore]
        [ForeignKey("QuestionId")]
        public virtual Question Questions { get; set; }
        public string ExamCode { get; set; }
        public Score Score { get; set; }
        public int Answer { get; set; }
        public int Option { get; set; }
        
     

    }
}
