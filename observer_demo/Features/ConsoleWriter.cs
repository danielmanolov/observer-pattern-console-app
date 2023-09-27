using Observer_Demo.Interfaces;

namespace Observer_Demo.Features
{
    public class ConsoleWriter : IConsoleWriter
    {
        public async Task Write(string value)
        {
            Console.WriteLine(value);
        }
    }
}
