using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizMaster.Models
{
    public class Question : BaseModel
    {
        [Display(Name ="Subject")]
        public int SubjectId { get; set; }
        [JsonIgnore]
        [Required]
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Question Text")]
        public string QuestText { get; set; }
        [Display(Name = "Question Number")]
        public int QuestionNumber { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Option A")]
        public string OptionA { get; set; }
        [Required]
        [Display(Name = "Option B")]
        [DataType(DataType.MultilineText)]
        public string OptionB { get; set; }
        [Required]
        [Display(Name = "Option C")]
        [DataType(DataType.MultilineText)]
        public string OptionC { get; set; }
        [Required]
        [Display(Name = "Option D ")]
        [DataType(DataType.MultilineText)]
        public string OptionD { get; set; }
        [Required]
        [Display(Name = "Correct Answer", Prompt ="Enter 1 for A, 2 for B, 3 for C and 4 for D")]
        [DataType(DataType.MultilineText)]
        public int CorrectAnswer { get; set; }
    }
}
