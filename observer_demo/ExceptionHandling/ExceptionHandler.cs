using Observer_Demo.Interfaces;

namespace Observer_Demo.ExceptionHandling
{
    internal class ExceptionHandler  : IExceptionHandler
    {
        private readonly IDocumentWriter _writer;

        public ExceptionHandler(IDocumentWriter writer)
        {
            _writer = writer;
        }

        public void HandleException(Exception exception)
        {
            //we can handle multiple custom exception types here and handle them(log them) appropriately
            _writer.Write("ExceptionLog.txt", exception.Message);
        }
    }
}
