using Xunit;
using InvertedIndex.Source;
namespace InvertedIndex.Test
{
    public class DocumentTests
    {
        [Fact]
        public void Document_Equals_ShouldReturnTrueForEqualDocuments()
        {
            // Given
            var doc1 = new Document("Doc1", ["word1", "word2"]);
            var doc2 = new Document("Doc1", ["word1", "word2"]);

            // When

            // Then
            Assert.True(doc1.Equals(doc2));
        }

        [Fact]
        public void Document_Equals_ShouldReturnFalseForUnequalDocuments()
        {
            // Given
            var doc1 = new Document("Doc1", ["word1", "word2"]);
            var doc2 = new Document("Doc2", ["word1", "word2"]);

            // When

            // Then
            Assert.False(doc1.Equals(doc2));
        }

        [Fact]
        public void Document_GetHashCode_ShouldReturnSameHashCodeForEqualDocuments()
        {
            // Given
            var doc1 = new Document("Doc1", ["word1", "word2"]);
            var doc2 = new Document("Doc1", ["word1", "word2"]);

            // When

            // Then
            Assert.Equal(doc1.GetHashCode(), doc2.GetHashCode());
        }
    }
}
