using System.Collections;

namespace interview_code
{
    public class TrieProblems
    {
        private readonly Node root;

        public TrieProblems()
        {
            root = new Node('#');
        }

        public void Insert(string word)
        {
            Node start = root;
            foreach (var ch in word)
            {
                if (!start.Neighbors.ContainsKey(ch))
                {
                    var newNode = new Node(ch);
                    start.Neighbors.Add(ch, newNode);
                }

                start = (Node) start.Neighbors[ch];
            }

            start.IsWord = true;
        }

        // Returns if the word is in the trie.
        public bool Search(string word)
        {
            Node start = root;
            foreach (var ch in word)
            {
                if (!start.Neighbors.ContainsKey((ch)))
                {
                    return false;
                }

                start = (Node) start.Neighbors[ch];
            }

            return start.IsWord;
        }

        // Returns if there is any word in the trie
        // that starts with the given prefix.
        public bool StartsWith(string word)
        {
            Node start = root;
            foreach (var ch in word)
            {
                if (!start.Neighbors.ContainsKey((ch)))
                {
                    return false;
                }

                start = (Node) start.Neighbors[ch];
            }

            return true;
        }
    }

    public class Node
    {
        public Node(char data)
        {
            IsWord = false;
            Data = data;
            Neighbors = new Hashtable();
        }

        public bool IsWord { get; set; }
        public char Data { get; set; }
        public Hashtable Neighbors { get; set; }
    }
}