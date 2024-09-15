namespace InvertedIndex.Source
{
    static public class InvertedIndexBuilder
    {
        static public Dictionary<string, HashSet<Document>> BuildInvertedIndex(HashSet<Document> documents)
        {
            var invertedIndex = new Dictionary<string, HashSet<Document>>();
            foreach (var document in documents)
            {
                foreach (var word in document.Words)
                {
                    if (!invertedIndex.TryGetValue(word, out var docs))
                    {
                        docs = [];
                        invertedIndex[word] = docs;
                    }

                    docs.Add(document);
                }
            }

            return invertedIndex;
        }
    }
}
