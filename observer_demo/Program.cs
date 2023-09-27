using Microsoft.Extensions.DependencyInjection;
using Observer_Demo.ExceptionHandling;
using Observer_Demo.Features;
using Observer_Demo.Interfaces;

public class Program
{
    public static async Task Main(string[] args)
    {
        //setup an example of DI
        var serviceProvider = new ServiceCollection()
            .AddSingleton(typeof(IConsoleWriter), typeof(ConsoleWriter))
            .AddSingleton(typeof(IDocumentWriter), typeof(DocumentWriter))
            .AddSingleton(typeof(IExceptionHandler), typeof(ExceptionHandler))
            .BuildServiceProvider();

        //need these as it's just an example console app
        var consoleWriter = serviceProvider.GetRequiredService<IConsoleWriter>();
        var documentWriter = serviceProvider.GetRequiredService<IDocumentWriter>();
        var exceptionHandler = serviceProvider.GetRequiredService<IExceptionHandler>();

        //create the program's actors
        var consoleReader = new ConsoleReader(consoleWriter, exceptionHandler);
        var mathGenerator = new MathGenerator(consoleWriter);
        var achievmentTracker = new AchievmentTracker(consoleWriter, documentWriter);

        //attach the observer
        consoleReader.Subscribe(mathGenerator);

        //start
        try
        {
            mathGenerator.GenerateNewTask();
            await consoleReader.Start();
        }
        catch (Exception e)
        {
            exceptionHandler.HandleException(e);
            throw;
        }
    }
}