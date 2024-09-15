namespace InvertedIndex.Source
{
    public class Document(string name, string[] words)
    {
        public string Name { get; set; } = name;
        public string[] Words { get; set; } = words;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Document other = (Document)obj;
            return Name == other.Name && Words.SequenceEqual(other.Words);
        }

        public override int GetHashCode()
        {
            int hash = Name.GetHashCode();
            foreach (var word in Words)
            {
                hash = hash * 31 + word.GetHashCode();
            }
            return hash;
        }
    }
}