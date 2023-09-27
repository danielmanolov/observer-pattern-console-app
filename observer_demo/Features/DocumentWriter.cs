using Observer_Demo.Interfaces;

namespace Observer_Demo.Features
{
    internal class DocumentWriter : IDocumentWriter
    {
        public async Task Write(string value)
        {
            // Set a variable to the Documents path.
            var currentDirectory = Directory.GetCurrentDirectory();

            // Get the project folder path
            var docPath = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

            //Write to the specified file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Document.txt")))
            {
                await outputFile.WriteAsync(value);
            }
        }

        public async Task Write(string fileName, string value)
        {
            // Set a variable to the Documents path.
            var currentDirectory = Directory.GetCurrentDirectory();

            // Get the project folder path
            var docPath = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

            //Write to the specified file
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName)))
            {
                await outputFile.WriteAsync(value);
            }
        }
    }   
}
