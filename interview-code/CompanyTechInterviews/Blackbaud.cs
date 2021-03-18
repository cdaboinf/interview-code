using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace interview_code.CompanyTechInterviews
{
    public class Blackbaud
    {
        public bool IsPalindrome(string str)
        {
            if (str.Length == 1)
            {
                return true;
            }

            var lidx = 0;
            var ridx = str.Length - 1;

            while (lidx < ridx)
            {
                if (str[lidx] != str[ridx])
                {
                    return false;
                }

                lidx++;
                ridx--;
            }

            return true;
        }

        public bool IsPalindromeWithSpaces(string str)
        {
            if (str.Length == 1)
            {
                return true;
            }

            var lidx = 0;
            var ridx = str.Length - 1;

            while (lidx < ridx)
            {
                while (str[lidx] == ' ' || Char.IsWhiteSpace(str[lidx]))
                {
                    lidx++;
                }

                while (str[ridx] == ' ' || Char.IsWhiteSpace(str[ridx]))
                {
                    ridx--;
                }

                if (str[lidx] != str[ridx])
                {
                    return false;
                }

                lidx++;
                ridx--;
            }

            return true;
        }

        public string LongestPalindrome(string str)
        {
            if (str.Length == 1)
            {
                return str;
            }

            var longest = string.Empty;
            var maxLenght = Int32.MinValue;

            for (var i = 0; i < str.Length; i++)
            {
                for (var j = 0; j < str.Length - i; j++)
                {
                    var sub = str.Substring(i, j + 1);
                    if (IsPalindrome(sub))
                    {
                        if (sub.Length > maxLenght)
                        {
                            maxLenght = sub.Length;
                            longest = sub;
                        }
                    }
                }
            }

            return longest;
        }

        public string LongestPalindromeSubstring(string str)
        {
            if (str.Length == 1)
            {
                return str;
            }

            var start = 0;
            var end = 0;

            for (var i = 0; i < str.Length; i++)
            {
                var oddResult = LengthFromMiddle(str, i, i);
                var evenResult = LengthFromMiddle(str, i, i + 1);

                var length = Math.Max(oddResult, evenResult);

                if (length > end - start)
                {
                    start = i - (length - 1) / 2;
                    // cbbd => 1 - 2/2 = 0 -- 1 - ((2-1)/2) = 1 - 0 = 1
                    // ccbbcc => 2 - 6/2 = 2 - 3 = - 1
                    end = i + (length / 2);

                    // l,abcba,r = 2 - 5/2 = 2 - 2 = 0
                    // 2 + (2) = 4 - 0 + 1
                }
            }

            return str.Substring(start, end - start + 1);
        }

        private static int LengthFromMiddle(string str, int left, int right) // i = 2
        {
            while (left >= 0 && right < str.Length && str[left] == str[right])
            {
                left--;
                right++;
            }

            // l = 1, 0, -1
            // r = 3, 4, 5
            // 5 - -1 = 6 - 1 = 5
            return right - left - 1;
        }

        public List<string> DependencyList(Dictionary<string, string[]> appDependencies)
        {
            var buildOrder = new List<string>();
            var visiting = new List<string>();
            var visited = new List<string>();

            foreach (var app in appDependencies)
            {
                if (!visited.Contains(app.Key))
                {
                    DFSBuilds(app.Key, visiting, visited, buildOrder, appDependencies);
                }
            }

            return buildOrder;
        }

        private void DFSBuilds(
            string app,
            List<string> visiting,
            List<string> visited,
            List<string> buildOrder,
            Dictionary<string, string[]> appDependencies)
        {
            visiting.Add(app);
            var dependencies = appDependencies[app];
            foreach (var dep in dependencies)
            {
                /*if (visiting.Contains(dep))
                {
                    return;
                }*/
                if (!visited.Contains(dep))
                {
                    DFSBuilds(dep, visiting, visited, buildOrder, appDependencies);
                }
            }

            visiting.Remove(app);
            visited.Add(app);
            buildOrder.Add(app);
        }

        private List<string> GetMissingDeps(List<string> deps, List<string> sortedDependencies)
        {
            return deps.Except(sortedDependencies).ToList();
        }

        public List<int> FindModifications()
        {
            List<ValueItem> before = new List<ValueItem>();
            before.Add(new ValueItem(1, "foo"));
            before.Add(new ValueItem(2, "bar"));

            List<ValueItem> after = new List<ValueItem>();
            after.Add(new ValueItem(1, "fooEdited"));
            after.Add(new ValueItem(3, "baz"));

            //your code here
            var changes = 0;
            var additions = 0;
            var deletes = 0;

            var keyMap1 = new Hashtable();
            var keyMap2 = new Hashtable();

            foreach (var bitem in before)
            {
                keyMap1.Add(bitem.Id, bitem.Value);
            }

            foreach (var aitem in after)
            {
                keyMap2.Add(aitem.Id, aitem.Value);
            }

            foreach (var item in after)
            {
                if (keyMap1.ContainsKey(item.Id))
                {
                    if (keyMap1[item.Id] != item.Value)
                    {
                        changes++;
                    }
                }
                else if (!keyMap1.ContainsKey(item.Id))
                {
                    additions++;
                }
            }

            foreach (var item in before)
            {
                if (!keyMap2.ContainsKey(item.Id))
                {
                    deletes++;
                }
            }

            return new List<int> {additions, deletes, changes};
        }

        public class ValueItem
        {
            public int Id { get; }
            public string Value { get; }

            public ValueItem(int id, string value)
            {
                this.Id = id;
                this.Value = value;
            }
        }
    }
}