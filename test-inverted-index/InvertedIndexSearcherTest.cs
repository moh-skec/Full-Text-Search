using Xunit;
using InvertedIndex.Source;

namespace InvertedIndex.Test
{
    public class InvertedIndexSearcherTests
    {
        [Fact]
        public void Search_MustHaveWordsSetIsNotEmpty_ReturnDocumentsContainingMustHaveWords()
        {
            // Given
            var doc1 = new Document("Doc1", ["apple", "banana"]);
            var doc2 = new Document("Doc2", ["apple", "cherry"]);
            var documents = new HashSet<Document> { doc1, doc2 };
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);
            var searcher = new InvertedIndexSearcher(documents, index);

            // When
            var result = searcher.Search(["apple"], [], []);

            // Then
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Search_MustNotHaveWordsSetIsNotEmpty_ReturnDocumentsNotContainongMustNotHaveWords()
        {
            // Given
            var doc1 = new Document("Doc1", ["apple", "banana"]);
            var doc2 = new Document("Doc2", ["apple", "cherry"]);
            var documents = new HashSet<Document> { doc1, doc2 };
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);
            var searcher = new InvertedIndexSearcher(documents, index);

            // When
            var result = searcher.Search(["apple"], [], ["banana"]);

            // Then
            Assert.Single(result);
            Assert.Contains(doc2, result);
        }

        [Fact]
        public void Search_OptionalWordsSetIsNotEmpty_ReturnDocumentsContainingOptionalWords()
        {
            // Given
            var doc1 = new Document("Doc1", ["apple", "banana"]);
            var doc2 = new Document("Doc2", ["apple", "cherry"]);
            var documents = new HashSet<Document> { doc1, doc2 };
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);
            var searcher = new InvertedIndexSearcher(documents, index);

            // When
            var result = searcher.Search([], ["cherry"], []);

            // Then
            Assert.Single(result);
            Assert.Contains(doc2, result);
        }

        [Fact]
        public void Search_PhrasalSearchWhenPhraseNotInDocument_ReturnEmpty()
        {
            // Given
            var doc1 = new Document("Doc1", ["apple", "banana", "cherry"]);
            var doc2 = new Document("Doc2", ["apple", "cherry", "banana"]);
            var documents = new HashSet<Document> { doc1, doc2 };
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);
            var searcher = new InvertedIndexSearcher(documents, index);

            // When
            var result = searcher.Search(["\"banana apple\""], [], []);

            // Then
            Assert.Empty(result);
        }

        [Fact]
        public void Search_PhrasalSearchWhenPhraseNotInDocument_ReturnDoucumentCorrectly()
        {
            // Given
            var doc1 = new Document("Doc1", ["banana", "apple", "cherry"]);
            var doc2 = new Document("Doc2", ["apple", "banana", "cherry", "orange"]);
            var documents = new HashSet<Document> { doc1, doc2 };
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);
            var searcher = new InvertedIndexSearcher(documents, index);

            // When
            var result = searcher.Search(["\"banana cherry\""], [], []);

            // Then
            Assert.Single(result);
            Assert.Contains(doc2, result);
        }
    }
}
