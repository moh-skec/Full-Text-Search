using InvertedIndex.Source.Interface;
namespace InvertedIndex.Source
{
    public class FileWrapper : IFileWrapper
    {
        public string ReadAllText(string path) => File.ReadAllText(path);
    }
}
