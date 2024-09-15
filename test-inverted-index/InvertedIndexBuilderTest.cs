using Xunit;
using InvertedIndex.Source;

namespace InvertedIndex.Test
{
    public class InvertedIndexBuilderTests
    {
        [Fact]
        public void BuildInvertedIndex_DocumentsSetIsEmpty_ReturnEmpty()
        {
            // Given
            HashSet<Document> documents = [];

            // When
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);

            // Then
            Assert.Empty(index);
        }

        [Fact]
        public void BuildInvertedIndex_DocumentsSetIsNotEmpty_ReturnCorrectIndex()
        {
            // Given
            var doc1 = new Document("Doc1", ["apple", "banana"]);
            var doc2 = new Document("Doc2", ["apple", "cherry"]);
            HashSet<Document> documents = [doc1, doc2];

            // When
            var index = InvertedIndexBuilder.BuildInvertedIndex(documents);

            // Then
            Assert.Equal(2, index["apple"].Count);
            Assert.Single(index["banana"]);
            Assert.Single(index["cherry"]);
        }
    }
}
