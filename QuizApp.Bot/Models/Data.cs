using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Bot.Models
{
    public static class Data
    {
        public static List<QuestionModel> GetQuestions = new List<QuestionModel>()
        {
            new QuestionModel()
            {
                Id = 1,
                Question = "Ikki kara ikki nechchi?",
                AnswerA = "4",
                AnswerB = "8",
                AnswerC = "5",
                CorrectAnswer = "A"
            },
            new QuestionModel()
            {
                Id = 2,
                Question = "Sinfda nechta odam bor?",
                AnswerA = "10 ta",
                AnswerB = "12 ta",
                AnswerC = "11",
                CorrectAnswer = "C",
            },
            new QuestionModel()
            {
                Id = 3,
                Question = "Jonibekning yoshi nechida?",
                AnswerA = "16 da",
                AnswerB = "15 da",
                AnswerC = "18 da",
                CorrectAnswer = "B",
            }
        };
    }
}
