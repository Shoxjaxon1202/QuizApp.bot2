using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Bot.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question{ get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }     
        public string AnswerC { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
