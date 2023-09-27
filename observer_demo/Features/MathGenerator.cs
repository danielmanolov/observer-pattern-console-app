using Observer_Demo.Constants;
using Observer_Demo.Interfaces;

namespace Observer_Demo.Features
{
    public class MathGenerator : IObserver<int>
    {
        private int expectedAnswer;
        public static event Func<Task> correctlyAnsweredEvent;
        public static event Func<Task> incorrectlyAnsweredEvent;
        private readonly IConsoleWriter _consoleWriter;

        internal MathGenerator(IConsoleWriter writer)
        {
            expectedAnswer = 0;
            _consoleWriter = writer;
        }

        public void OnCompleted()
        {
            _consoleWriter.Write("Completed");
        }

        public void OnError(Exception error)
        {
            _consoleWriter.Write("Error occurred:");
            _consoleWriter.Write(error.Message);
        }

        public void OnNext(int value)
        {
            CheckAnswer(value);
        }

        internal void CheckAnswer(int answer)
        {
            if (answer == expectedAnswer)
            {
                correctlyAnsweredEvent?.Invoke();
            }

            else
            {
                incorrectlyAnsweredEvent?.Invoke();
            }

            GenerateNewTask();
        }

        internal void GenerateNewTask()
        {
            var rnd = new Random();
            var firstParam = rnd.Next(1, 20);
            var secondParam = rnd.Next(1, 20);
            expectedAnswer = firstParam + secondParam;

            _consoleWriter.Write(String.Format(ConsoleMessages.SumQuestionMessage, firstParam, secondParam));
        }
    }
}
