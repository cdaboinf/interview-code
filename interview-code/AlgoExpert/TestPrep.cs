using System.Collections.Generic;

namespace interview_code.AlgoExpert
{
    public class TestPrep
    {
        public List<List<int> > Powerset(List<int> array) {
            var powerset = new List<List<int>> {new List<int>()};
            
            foreach (var t in array)
            {
                var subset = powerset.Count;
                for(var j=0; j<subset; j++) {
                    var list = new List<int>(powerset[j]);
                    list.Add(t);
                    powerset.Add(list);
                }
            }
		
            return powerset;
        }
    }

    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
    }

    public class SuffixTrie
    {
        public TrieNode root = new TrieNode();
        public char endSymbol = '*';

        public SuffixTrie(string str)
        {
            PopulateSuffixTrieFrom(str);
        }

        public void PopulateSuffixTrieFrom(string str)
        {
            for(var x=0; x<str.Length; x++)
            {
                var node = root;
                for (var y = x; y < str.Length; y++)
                {
                    if (!node.Children.ContainsKey(str[y]))
                    {
                        node.Children.Add(str[y], new TrieNode());
                    }
                    node = node.Children[str[y]];
                }
                node.Children.Add(endSymbol, new TrieNode());
            }
        }

        public bool Contains(string str)
        {
            var node = root;
            for (var x = 0; x < str.Length; x++)
            {
                if (node.Children.ContainsKey(str[x]))
                {
                    node = node.Children[str[x]];
                }
                else
                {
                    return false;
                }
            }

            return node.Children.ContainsKey(endSymbol);
        }
      
    }
}