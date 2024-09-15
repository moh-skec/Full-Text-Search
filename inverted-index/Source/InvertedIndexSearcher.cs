namespace InvertedIndex.Source
{
    public class InvertedIndexSearcher(
        HashSet<Document> documents,
        Dictionary<string, HashSet<Document>> invertedIndex)
    {
        private readonly HashSet<Document> _documents = documents;
        private readonly Dictionary<string, HashSet<Document>> _invertedIndex = invertedIndex;

        public HashSet<Document> Search(
            List<string> mustHaveWords, List<string> optionalWords, List<string> mustNotHaveWords
        )
        {
            var AndDocuments = HandleMustHaveWords(mustHaveWords);
            var AndNotDocuments = HandleMustNotHaveWords(AndDocuments, mustNotHaveWords);
            var AndNotOrDocuments = HandleOptionalWords(AndNotDocuments, optionalWords);
            return AndNotOrDocuments;
        }

        private HashSet<Document> HandleMustHaveWords(
            List<string> mustHaveWords
        )
        {
            HashSet<Document> _resultDocuments = new(_documents);
            foreach (var word in mustHaveWords)
            {
                if (word.StartsWith('\"') && word.EndsWith('\"') && word.Length > 1)
                {
                    var phrasalDocuments = HandlePhrasalWord(word);
                    _resultDocuments!.IntersectWith(phrasalDocuments);
                }
                else if (_invertedIndex.TryGetValue(word, out var docs))
                {
                    _resultDocuments!.IntersectWith(docs);
                }
                else
                {
                    return [];
                }
            }

            return _resultDocuments;
        }

        private HashSet<Document> HandleOptionalWords(
            HashSet<Document> resultDocuments, List<string> optionalWords
        )
        {
            HashSet<Document> _resultDocuments = new(resultDocuments);
            if (optionalWords.Count > 0)
            {
                _resultDocuments.ExceptWith(HandleMustNotHaveWords(resultDocuments, optionalWords));
            }
            return _resultDocuments;
        }

        private HashSet<Document> HandleMustNotHaveWords(
            HashSet<Document> resultDocuments, List<string> mustNotHaveWords
        )
        {
            HashSet<Document> _resultDocuments = new(resultDocuments);
            foreach (var word in mustNotHaveWords)
            {
                if (word.StartsWith('\"') && word.EndsWith('\"') && word.Length > 1)
                {
                    var phrasalDocuments = HandlePhrasalWord(word);
                    _resultDocuments!.ExceptWith(phrasalDocuments);
                }
                else if (_invertedIndex.TryGetValue(word, out var docs))
                {
                    _resultDocuments!.ExceptWith(docs);
                }
            }

            return _resultDocuments;
        }

        private HashSet<Document> HandlePhrasalWord(string word)
        {
            string phrasalWord = word[1..^1];
            var phrasalWords = phrasalWord.Split(" ").ToList();
            var phrasalDocuments = HandleMustHaveWords(phrasalWords);
            foreach (var document in phrasalDocuments)
            {
                if (!string.Join(" ", document.Words).Contains(phrasalWord))
                {
                    phrasalDocuments.Remove(document);
                }
            }

            return phrasalDocuments;
        }
    }
}
