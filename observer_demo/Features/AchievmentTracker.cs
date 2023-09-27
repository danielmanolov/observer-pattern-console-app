using Observer_Demo.Constants;
using Observer_Demo.Interfaces;

namespace Observer_Demo.Features
{
    internal class AchievmentTracker
    {
        private int correctAsnwersCount = 0;
        private int incorrectAsnwersCount = 0;
        private readonly IConsoleWriter _consoleWriter;
        private readonly IDocumentWriter _documentWriter;

        public AchievmentTracker(IConsoleWriter consoleWriter, IDocumentWriter documentWriter)
        {
            MathGenerator.correctlyAnsweredEvent += CorrectAnswerHandler;
            MathGenerator.incorrectlyAnsweredEvent += IncorrectAnswerHandler;
            _consoleWriter = consoleWriter;
            _documentWriter = documentWriter;
        }

        ~AchievmentTracker()
        {
            MathGenerator.correctlyAnsweredEvent -= CorrectAnswerHandler;
            MathGenerator.incorrectlyAnsweredEvent -= IncorrectAnswerHandler;
        }
        private async Task CorrectAnswerHandler()
        {
            correctAsnwersCount++;
            if (correctAsnwersCount == 3)
            {
                await WriteMessage(AchievementMessages.ThreeGoodAnswersMessage);
            }

            if (correctAsnwersCount % 5 == 0)
            {
                await WriteMessage(AchievementMessages.GreatStreakMessage);
            }
        }

        private async Task IncorrectAnswerHandler()
        {
            incorrectAsnwersCount++;
            if (incorrectAsnwersCount == 3)
            {
                await WriteMessage(AchievementMessages.ThreeBadAnswersMessage);
            }

            if (incorrectAsnwersCount % 5 == 0)
            {
                await WriteMessage(AchievementMessages.BadStreakMessage);
            }
        }

        private async Task WriteMessage(string mesage)
        {
            var writeToConsoleTask = _consoleWriter.Write(mesage);
            var writeToDocumentTask = _documentWriter.Write(mesage);

            Task.WhenAll(writeToConsoleTask, writeToDocumentTask);
        }
    }
}
