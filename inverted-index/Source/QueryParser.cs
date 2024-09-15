using System.Text;

namespace InvertedIndex.Source
{
    static public class QueryParser
    {
        static public void ParseQuery(
            string query,
            out List<string> mustHaveWords,
            out List<string> optionalWords,
            out List<string> mustNotHaveWords
        )
        {
            mustHaveWords = [];
            optionalWords = [];
            mustNotHaveWords = [];

            string[] words = query.Split(' ');
            StringBuilder phrasalWord = new();
            bool isInPhrase = false;
            for (int i = 0; i < words.Length; i++)
            {
                string? resultWord = words[i];
                if (words[i].StartsWith('\"') || words[i][1] == '\"' && (
                    words[i].StartsWith('+') || words[i].StartsWith('-')
                    ) || isInPhrase)
                {
                    isInPhrase = true;
                    phrasalWord.Append(words[i] + " ");
                    Console.WriteLine(phrasalWord);
                    if (words[i].EndsWith('\"'))
                    {
                        isInPhrase = false;
                        resultWord = phrasalWord.ToString().TrimEnd().TrimEnd();
                        phrasalWord.Clear();
                    }
                }
                if (!isInPhrase || i == words.Length - 1)
                {
                    if (resultWord.StartsWith('+'))
                    {
                        optionalWords.Add(resultWord[1..].ToUpper());
                    }
                    else if (resultWord.StartsWith('-'))
                    {
                        mustNotHaveWords.Add(resultWord[1..].ToUpper());
                    }
                    else
                    {
                        mustHaveWords.Add(resultWord.ToUpper());
                    }
                }
            }
        }
    }
}
