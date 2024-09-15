using InvertedIndex.Source.Interface;
namespace InvertedIndex.Source
{
    public class DocumentWrapper(
        IFileWrapper fileWrapper, IDirectoryWrapper directoryWrapper
        ) : IDocumentWrapper
    {
        private readonly IFileWrapper _fileWrapper = fileWrapper;
        private readonly IDirectoryWrapper _directoryWrapper = directoryWrapper;
        public HashSet<Document> GetDocuments(string path)
        {
            HashSet<Document> allDocuments = [];
            var allFiles = _directoryWrapper.GetFiles(path);

            char[] splitChars = [' ', '.', ',', '!', '?', ';', ':', '-', '+', '*', '\'', '\"', '\n', '\r'];
            foreach (var file in allFiles)
            {
                string fileName = Path.GetFileName(file);
                string[] fileWords = _fileWrapper.ReadAllText(file).ToUpper().Split(
                    splitChars, StringSplitOptions.RemoveEmptyEntries
                );
                allDocuments.Add(new Document(fileName, fileWords));
            }
            return allDocuments;
        }
    }
}
