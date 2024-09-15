namespace InvertedIndex.Source.Interface
{
    public interface IDocumentWrapper
    {
        HashSet<Document> GetDocuments(string path);
    }
}