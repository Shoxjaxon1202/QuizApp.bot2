using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Bot.Models
{
    public enum Step
    {
        Start,
        Menu,
        Profile,
        Questions,

        AddingQuestion,
        AnswerA,
        AnswerB,
        AnswerC,
        CorrecAnswer,
    }
}
