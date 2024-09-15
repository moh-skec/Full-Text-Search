namespace InvertedIndex.Source
{
    static public class Program
    {
        public static void Main()
        {
            string directoryPath = @"mnt\data\EnglishData";

            var allDocuments = new DocumentWrapper(
                new FileWrapper(), new DirectoryWrapper()
            ).GetDocuments(directoryPath);

            var invertedIndex = InvertedIndexBuilder.BuildInvertedIndex(allDocuments);

            Console.WriteLine("Enter your search query:");
            string? query = Console.ReadLine();

            QueryParser.ParseQuery(
                query!,
                out List<string> mustHaveWords,
                out List<string> optionalWords,
                out List<string> mustNotHaveWords
            );

            var indexSearcher = new InvertedIndexSearcher(allDocuments, invertedIndex);
            var resultDocuments = indexSearcher.Search(
                mustHaveWords, optionalWords, mustNotHaveWords
            );
            if (resultDocuments!.Count > 0)
            {
                Console.WriteLine("Documents found:");
                foreach (var doc in resultDocuments)
                {
                    Console.WriteLine(doc.Name);
                }
            }
            else
            {
                Console.WriteLine("No documents match the search criteria.");
            }
        }
    }
}
