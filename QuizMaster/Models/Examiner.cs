using QuizMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class Examiner : BaseModel
    {
        public Guid ExaminationId { get; set; }
        [JsonIgnore]
        [ForeignKey("ExaminationId")]
        public List<Examination> Exam { get; set; }
    }
}
