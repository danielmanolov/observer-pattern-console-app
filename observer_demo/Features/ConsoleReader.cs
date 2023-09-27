using Observer_Demo.Constants;
using Observer_Demo.Interfaces;

namespace Observer_Demo.Features
{
    internal class ConsoleReader : IObservable<int>, IDisposable
    {
        private string input = string.Empty;
        private const string programStopKeyword = "stop";
        private const string programExceptionKeyword = "exception";
        private readonly IConsoleWriter _consoleWriter;
        private readonly IExceptionHandler _exceptionHandler;

        public ConsoleReader(IConsoleWriter consoleWriter, IExceptionHandler exceptionHandler)
        {
            _consoleWriter = consoleWriter;
            _exceptionHandler = exceptionHandler;
        }

        public IList<IObserver<int>> answerObservers = new List<IObserver<int>>();

        public void Dispose()
        {
            answerObservers.Clear();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            answerObservers.Add(observer);
            return this;
        }

        public async Task Start()
        {
            do
            {
                input = Console.ReadLine();

                if(input.ToLower() == programStopKeyword)
                {
                    break;
                }

                if (input.ToLower() == programExceptionKeyword)
                {
                    //in an api this would either be handled internally for the given context or in a middleware
                    try
                    {
                        throw new Exception("random exception");
                    }
                    catch (Exception e)
                    {
                        //handle
                        _exceptionHandler.HandleException(e);
                        continue;
                    }
                }

                int answer;
                int.TryParse(input, out answer);
                
                if(answer == 0)
                {
                    //maybe throw exception/log error
                    await _consoleWriter.Write(ConsoleMessages.RetryMessage);
                    continue;
                }

                foreach (var observer in answerObservers)
                {
                    observer.OnNext(answer);
                }
            }
            while (true);
        }
    }
}
