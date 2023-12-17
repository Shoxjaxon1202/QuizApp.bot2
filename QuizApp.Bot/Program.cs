
using Microsoft.VisualBasic;
using QuizApp.Bot.Models;
using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

QuestionModel question = new QuestionModel();

string BotToken = "6722715817:AAECPxbgClpVM1NBrk-97N3k44_1iubZOTE";
TelegramBotClient bot = new(BotToken);


bot.StartReceiving(
              updateHandler: (client, update, token) => GetUpdate(update),
              pollingErrorHandler: (client, exception, token) => Task.CompletedTask);

Console.ReadKey();

async Task GetUpdate(Update update)
{
    if (update.Type is UpdateType.Message)
    {

        if (update.Message == null)
        {
            return;
        }

        var message = update.Message.Text;

        if (message == null)
        {
            return;
        }

        if (UserAction.step == Step.AddingQuestion)
        {
            switch (UserAction.step)
            {
                case Step.AddingQuestion:
                    question.Question = update.Message.Text;
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "A javobni kiriting", replyMarkup: BackButton());
                    UserAction.step = Step.AnswerA;
                    break;
                case Step.AnswerA:
                    question.AnswerA = update.Message.Text;
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "B javobni kiriting", replyMarkup: BackButton());
                    UserAction.step = Step.AnswerB;
                    break;
                case Step.AnswerB:
                    question.AnswerA = update.Message.Text;
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "C javobni kiriting", replyMarkup: BackButton());
                    UserAction.step = Step.AnswerC;
                    break;
                case Step.AnswerC:
                    question.AnswerC = update.Message.Text;
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "To'g'ri javobni kiriting", replyMarkup: BackButton());
                    UserAction.step = Step.CorrecAnswer;
                    break;
                case Step.CorrecAnswer:
                    question.CorrectAnswer = update.Message.Text;
                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "QuestionInfo was succsessfully added!");
                    Data.GetQuestions.Add(question);
                    UserAction.step = Step.Start;
                    break;
            }
        }


        var chatId = update.Message.Chat.Id;
        if (message == "/start" || message == "◀️back")
        {
            await bot.SendTextMessageAsync(chatId, "Choose one of these", replyMarkup: new ReplyKeyboardRemove());
            GetMenu(chatId);

            UserAction.step = Step.Start;
            return;
        }
    }

    // ------------------------------------------------------------------// CallBack

    if (update.Type == UpdateType.CallbackQuery)
    {
        if (update.CallbackQuery == null)
        {
            return;
        }

        var CallBackMessage = update.CallbackQuery.Data;

        if (CallBackMessage == null)
        {
            return;
        }

        var CallBackchatId = update.CallbackQuery.Message.Chat.Id;

        if (CallBackMessage == "Profile")
        {
            var firstName = update.CallbackQuery.From.FirstName;
            var lastName = update.CallbackQuery.From.LastName;
            var userName = update.CallbackQuery.From.Username;

            await bot.SendTextMessageAsync(CallBackchatId, $"Your Info: @{userName} - {firstName} {lastName}", replyMarkup: BackButton());
        }

        if (CallBackMessage == "Questions")
        {
            bot.SendTextMessageAsync(CallBackchatId, "Questions");

            var questions = Data.GetQuestions;

            for (int i = 0; i < questions.Count; i++)
            {
                var questionInfo = $@"
{i + 1}. Question
{questions[i].Question}

A) {questions[i].AnswerA}
B) {questions[i].AnswerB}
C) {questions[i].AnswerC}

Correct: {questions[i].CorrectAnswer}";

                bot.SendTextMessageAsync(CallBackchatId, questionInfo, replyMarkup: BackButton());

                UserAction.step = Step.Questions;
            }
        }

        if (CallBackMessage == "Insert Test")
        {
            await bot.SendTextMessageAsync(CallBackchatId, "Savol kiriting: ");
            UserAction.step = Step.AddingQuestion;
        }
        // ---------------------------------------------------------------------------------------------------------- Step



    }
}

async void GetMenu(long ChatId)
{
    await bot.SendTextMessageAsync(ChatId, text: "Menu", replyMarkup: InlineButtons());
}
InlineKeyboardMarkup InlineButtons()
    {
        InlineKeyboardButton button1 = InlineKeyboardButton.WithCallbackData("Profile", "Profile");
        InlineKeyboardButton button2 = InlineKeyboardButton.WithCallbackData("Test", "Test");
        InlineKeyboardButton button3 = InlineKeyboardButton.WithCallbackData("Questions", "Questions");
        InlineKeyboardButton button4 = InlineKeyboardButton.WithCallbackData("Statistics", "Statistics");
        InlineKeyboardButton button5 = InlineKeyboardButton.WithCallbackData("Insert Test", "Insert Test");

        List<InlineKeyboardButton> row1 = new List<InlineKeyboardButton>();
        row1.Add(button1);
        row1.Add(button2);
        row1.Add(button3);
        row1.Add(button4);
        row1.Add(button5);

        List<List<InlineKeyboardButton>> Rows = new List<List<InlineKeyboardButton>>();
        Rows.Add(row1);

        return new InlineKeyboardMarkup(Rows);
    }

    ReplyKeyboardMarkup BackButton()
    {
        KeyboardButton keyboardButton = new KeyboardButton("◀️back");

        List<KeyboardButton> row = new List<KeyboardButton>();
        row.Add(keyboardButton);

        List<List<KeyboardButton>> keyboardButtons = new List<List<KeyboardButton>>();
        keyboardButtons.Add(row);

        return new ReplyKeyboardMarkup(keyboardButton)
        {
            ResizeKeyboard = true
        };
    }
