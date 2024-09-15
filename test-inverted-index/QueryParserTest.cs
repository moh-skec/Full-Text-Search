using System.Reflection;
using Xunit;
using InvertedIndex.Source;

namespace InvertedIndex.Test
{
    public class QueryParserTest
    {
        private static readonly string[] ExpectedMustHave1 = ["GET", "HELP", "\"STAR ACADEMY\""];
        private static readonly string[] ExpectedOptional1 = ["ILLNESS", "DISEASE"];
        private static readonly string[] ExpectedMustNotHave1 = ["COUGH"];

        private static readonly string[] ExpectedMustHave2 = ["HELLO", "WORLD"];
        private static readonly string[] ExpectedOptional2 = ["FOO", "BAR"];
        private static readonly string[] ExpectedMustNotHave2 = ["BAZ", "\"STAR ACADEMY\""];

        [Theory]
        [InlineData(
            "get help +illness +disease -cough \"star academy\"", 
            nameof(ExpectedMustHave1), nameof(ExpectedOptional1), nameof(ExpectedMustNotHave1)
        )]
        [InlineData(
            "hello world +foo +bar -baz -\"star academy\"",
            nameof(ExpectedMustHave2), nameof(ExpectedOptional2), nameof(ExpectedMustNotHave2)
        )]
        public void ParseQuery_ShouldParseCorrectly(
            string query, string expectedMustHave, string expectedOptional, string expectedMustNotHave
        )
        {
            QueryParser.ParseQuery(query, out var mustHaveWords, out var optionalWords, out var mustNotHaveWords);

            var expectedMustHaveArray = (string[])GetType().GetField(
                expectedMustHave, BindingFlags.NonPublic | BindingFlags.Static
            )!.GetValue(null)!;
            var expectedOptionalArray = (string[])GetType().GetField(
                expectedOptional, BindingFlags.NonPublic | BindingFlags.Static
            )!.GetValue(null)!;
            var expectedMustNotHaveArray = (string[])GetType().GetField(
                expectedMustNotHave, BindingFlags.NonPublic | BindingFlags.Static
            )!.GetValue(null)!;

            Assert.Equal(expectedMustHaveArray, mustHaveWords);
            Assert.Equal(expectedOptionalArray, optionalWords);
            Assert.Equal(expectedMustNotHaveArray, mustNotHaveWords);
        }
    }
}
