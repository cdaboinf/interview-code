using System;
using System.Collections.Generic;

namespace interview_code
{
    public class SearchProblems
    {
        public void InsertWordWithTrie(string word, Trie words)
        {
            foreach (var character in word)
            {
                if (words.Children[character] != null)
                {
                    var child = new Trie {Value = character};
                    words.Children.Add(character, child);

                    words = child;
                }
                else
                {
                    words = words.Children[character];
                }
            }

            words.IsWord = true;
        }

        public bool FindWordWithTrie(string word, Trie words)
        {
            foreach (var character in word)
            {
                if (words.Children[character] == null)
                {
                    return false;
                }
                else
                {
                    words = words.Children[character];
                }
            }

            return true;
        }

        /*
         * only remove uniques suffixes of word
         */
        public void DeleteWordWithTrie(string word, Trie words)
        {
            var suffixes = new List<Trie>();

            // case: no part of word can be removed from trie(words)
            for (var i = 0; i < word.Length; i++)
            {
                if (words.Children[word[i]] != null)
                {
                    words = words.Children[word[i]];

                    if (i == word.Length && words.Children.Keys.Count == word.Length)
                    {
                        throw new Exception($"suffixes in trie depend on {word}");
                    }
                }
            }
            
            // case: some part of word can be removed from trie(words)
            for (var j = 0; j < suffixes.Count; j++)
            {
                var parent = suffixes[j];
                var child = suffixes[suffixes.Count - j];

            }
        }
    }

    public class Trie
    {
        public Dictionary<char, Trie> Children { get; set; }

        public char Value { get; set; }

        public bool IsWord { get; set; }
    }
}