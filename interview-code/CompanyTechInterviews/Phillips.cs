using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;

namespace interview_code.CompanyTechInterviews
{
    public class Phillips
    {
        public string Reverse(string value)
        {
            var l = 0;
            var r = value.Length - 1;
            var chars = value.ToCharArray();
            while (l < r)
            {
                var temp = chars[r];
                chars[r] = chars[l];
                chars[l] = temp;

                l++;
                r--;
            }

            return new string(chars);
        }

        /*
         *  An edit between two strings is one of the following changes.
            Add a character
            Delete a character
            Change a character
            Given two string s1 and s2, find if s1 can be converted to s2 with exactly one edit. 
            Expected time complexity is O(m+n) where m and n are lengths of two strings.
        */
        public bool DistanceIsOne(string s1, string s2)
        {
            // Find lengths of given strings
            int m = s1.Length;
            int n = s2.Length;
            // If difference between lengths is more than 1, then strings can't be at one distance
            if (Math.Abs(m - n) > 1)
                return false;
            // Count of edits
            int count = 0;
            int i = 0;
            int j = 0;
            while (i < m && j < n)
            {
                // If current characters don't match
                if (s1[i] != s2[j])
                {
                    if (count == 1)
                        return false;
                    // If length of one string is more, then only possible edit is to remove a character
                    if (m > n)
                        i++;
                    else if (m < n)
                        j++;
                    // If lengths of both strings is same
                    else
                    {
                        i++;
                        j++;
                    }
                    // Increment count of edits 
                    count++;
                }
                // If current characters match
                else
                {
                    i++;
                    j++;
                }
            }

            // If last character is extra in any string
            if (i < m || j < n)
                count++;
            return count == 1;
        }

        // Returns if there is any word in the trie
        // that starts with the given prefix.
        public bool PhoneStartsWith(string word, Node root)
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
        /*
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
        */

        //Time Complexity: O(2^n) Space Complexity: O(n)
        public int GetFibNumberAtIndex(int index)
        {
            if (index == 0 || index == 1)
            {
                return 1;
            }

            var result = GetFibNumberAtIndex(index - 2) + GetFibNumberAtIndex(index - 1);

            return result;
        }

        public  int Fibonacci(int n)
        {
            return Fibonacci(n, new Hashtable());
        }

        //Time Complexity: O(n) Space Complexity: O(n)
        private int Fibonacci(int n, Hashtable memo)
        {
            if (n == 1 || n == 2)
                return 1;
            if (memo.ContainsKey(n)) // lookup memo
                return (int)memo[n];
            
            int result = Fibonacci(n - 1, memo) + Fibonacci(n - 2, memo);
            memo.Add(n, result); // insert memo
            
            return result;
        }
        
        public int Factorial(int n)
        {
            if (n == 0)
                return 1;
  
            return n * Factorial(n - 1);
        }
        
        public int FactorialInteractive(int n)
        {
            int res = 1;
  
            for (var i = 2; i <= n; i++)
                res *= i;
            return res;
        }

        /*
         * Code to find duplicate values is the array(number could be of any length).
         */
        public List<int> GetDuplicates(int[] nums)
        {
            var result = new List<int>();
            //Array.Sort(nums);
            var map = new HashSet<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                if (map.Contains(nums[i]))
                {
                    result.Add(nums[i]);
                }
                else
                {
                    map.Add(nums[i]);
                }
            }
            return result;
        }

        /*
         * no extra space, linear, from 1 to length of the array
         */
        public List<int> GetDuplicates1(int[] nums)
        {
            var result = new List<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                var index = Math.Abs(nums[i]) - 1;
                if (nums[index] < 0)
                {
                    result.Add(index + 1);
                }

                nums[index] = -1 * nums[index];
            }
            return result;
        }
        
        public int IsSubstring(string str, string target)
        {
            int t = 0;
            int len = str.Length;
            int i;
 
            // Iterate from 0 to len - 1
            for (i = 0; i < len; i++) {
                if (t == target.Length)
                    break;
                if (str[i] == target[t])
                    t++;
                else
                    t = 0;
            }
            return t < target.Length ? -1 : i - t;
        }
    }
    
    public class ImplementStackUsingQueues
    {
        private Queue<int> q1;
        private Queue<int> q2;

        public ImplementStackUsingQueues()
        {
            q1 = new Queue<int>();
            q2 = new Queue<int>();
        }
        
        public void Push(int val)   // 1234
        {
            while (q1.Count != 0)
            {
                q2.Enqueue(q1.Dequeue());
            }
            q1.Enqueue(val);
            while (q2.Count != 0)
            {
                q1.Enqueue(q2.Dequeue());
            }
        }
        
        public int Pop()
        {
            return q1.Dequeue();
        }
    }
}