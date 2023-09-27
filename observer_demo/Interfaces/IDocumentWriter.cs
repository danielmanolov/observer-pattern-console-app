namespace Observer_Demo.Interfaces
{
    public interface IDocumentWriter : IWriter
    {
        Task Write(string fileName, string value);
    }
}
