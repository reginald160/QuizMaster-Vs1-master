using Microsoft.EntityFrameworkCore;
using QuizMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizMaster.Data
{
    public static class SeedData
    {
        public static void seed(this ModelBuilder builder)
        {
            builder.Entity<Subject>().HasData(
             new Subject
             {
                 Id = 1,
                 Name = "English"
             },
             new Subject
             {
                 Id = 2,
                 Name = "Mathematics"
             });
            builder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    SubjectId = 2,
                    QuestText = "What is the sum of 2 and 3",
                    QuestionNumber = 1,
                    OptionA = "5",
                    OptionB = "2",
                    OptionC = "5",
                    OptionD = "7",
                    CorrectAnswer = 1,
                },
                 new Question
                 {
                     Id = 2,
                     SubjectId = 2,
                     QuestText = "What is the product of 2 and 3",
                     QuestionNumber = 2,
                     OptionA = "5",
                     OptionB = "6",
                     OptionC = "5",
                     OptionD = "7",
                     CorrectAnswer = 2,
                 });

        }
    }
}
