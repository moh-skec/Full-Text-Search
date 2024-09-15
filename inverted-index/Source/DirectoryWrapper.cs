using InvertedIndex.Source.Interface;
namespace InvertedIndex.Source
{
    public class DirectoryWrapper : IDirectoryWrapper
    {
        public string[] GetFiles(string path) => Directory.GetFiles(path);
    }
}
