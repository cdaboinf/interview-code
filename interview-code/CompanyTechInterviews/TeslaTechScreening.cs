using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace interview_code.CompanyTechInterviews
{
    public class TeslaTechScreening
    {
        public TeslaTechScreening()
        {
        }

        public int MimNumber(string word)
        {
            var counts = new Hashtable();
            var deletes = 0;

            for (var i = 0; i < word.Length; i++)
            {
                if (counts.ContainsKey(word[i]))
                {
                    counts[word[i]] = (int)counts[word[i]] + 1;
                }
                else
                {
                    counts.Add(word[i], 1);
                }
            }

            var uniqueCounts = new HashSet<int>();
            foreach (var key in counts.Keys)
            {
                var val = (int)counts[key];
                if (uniqueCounts.Contains(val) && val != 0)
                {
                    while (uniqueCounts.Contains(val) && val != 0)
                    {
                        deletes++;
                        val = val - 1;
                    }
                    if (val != 0)
                    {
                        uniqueCounts.Add(val);
                    }
                }
                else
                {
                    uniqueCounts.Add(val);
                }
            }

            return deletes;
        }

        public int NumberOfRepeats(int num)
        {
            int decVal = 0, baseVal = 1, rem;

            while (num > 0)
            {
                rem = num % 10;
                decVal = decVal + rem * baseVal;
                num = num / 10;
                baseVal = baseVal * 2;
            }

            var changes = 0;
            while (decVal != 0)
            {
                if (decVal % 2 == 0)
                {
                    decVal = decVal / 2;
                }
                else
                {
                    decVal -= 1;
                }
                changes++;
            }

            return changes;
        }

        public int Convert(string str1)
        {
            if (str1 == "")
                throw new Exception("Invalid input");
            int val = 0, res = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                try
                {
                    val = Int32.Parse(str1[i].ToString());
                    if (val == 1)
                        res += (int)Math.Pow(2, str1.Length - 1 - i);
                    else if (val > 1)
                        throw new Exception("Invalid!");
                }
                catch
                {
                    throw new Exception("Invalid!");
                }
            }
            return res;
        }

        /// <summary>
        /// "abbbab" number of swaps to not get 3 of the same a or b
        /// </summary>
        /// <param name="S"></param>
        /// <returns></returns>
        public int solution1Revised(string S)
        {
            var swaps = 0; // total swaps
            var countMap = new Hashtable(); // keep track of character ocurrance

            // if less than 3 there are no swaps needed
            if (S == null || S.Length < 3)
            {
                return 0;
            }

            // linear, one pass traversal
            for (var i = 0; i < S.Length; i++) // O(n)
            {
                var key = S[i];
                if (countMap.ContainsKey(key))
                {
                    countMap[key] = (int)countMap[key] + 1;

                    /*var count = (int)countMap[key] + 1;
                    if(count % 3 == 0)
                    {
                        swaps++;
                        count = 0;
                    }
                    countMap[key] = count;*/
                }
                else
                {
                    countMap.Add(key, 1); // add to map to track it
                }
            }
            // process per number of swaps, how collection of 3 chars per total count,
            // for each 3 char count, one swap will be needed
            foreach (var chkey in countMap.Keys)
            {
                swaps += (int)countMap[chkey] / 3;
            }
            return swaps;
        }

        /// <summary>
        /// Cost of removing a character to preent two of the same to be
        /// contiguos, C determines the cost, pick the smallest cost of characters
        /// </summary>
        /// <param name="S"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public int solution2(string S, int[] C)
        {
            if (S == null || S.Length == 0 || C == null || C.Length == 0)
            {
                return 0;
            }
            if (S.Length == 1)
            {
                return C[0];
            }

            var cost = 0;
            for (var i = 0; i < S.Length - 1; i++) // O(n-1) => O(n)
            {
                if (S[i] == S[i + 1])
                {
                    var minCost = Math.Min(C[i], C[i + 1]);
                    cost += minCost;
                }
            }
            return cost;
        }

        /// <summary>
        /// Test process array of tests gouprs and tests results, and calculates the
        /// average of passing groups, # of groups passing * 100 / total number of groups
        /// NOTE: a group is considered passed if all test results are: "OK"
        /// </summary>
        /// <param name="T"></param>
        /// <param name="R"></param>
        /// <returns></returns>
        public int solution3Revised(string[] T, string[] R)
        {
            var totalPassing = 0;
            var groupCount = 0;
            var groupsMap = new Hashtable();

            for (var i = 0; i < T.Length; i++) // O(T) => O(n)
            {
                string pattern = @"[^[a-zA-Z]";
                var match = Regex.Match(T[i], pattern);
                var group = match.ToString();

                // if not 'OK' default to fail, guarante that all results have to be 'OK'
                if (groupsMap.ContainsKey(group))
                {
                    if (R[i] != "OK" && groupsMap[group] != "-1") // only mark as failed once
                    {
                        groupsMap[group] = "-1";
                        totalPassing--;
                    }
                }
                else
                {
                    if (R[i] != "OK")
                    {
                        groupsMap.Add(group, "-1"); // mark as failed
                    }
                    else
                    {
                        groupsMap.Add(group, R[i]);
                        totalPassing++;
                    }
                    groupCount++; // keep track of total of groups
                }
            }

            var score = totalPassing * 100 / groupCount;
            return score;
        }

        public void RotateMatrix(int[,] matrix, int n)
        {
            for (var layer = 0; layer < n / 2; layer++) // n size, n=3, 3/2= 1
            {
                var start = layer;        // 0, 1
                var end = n - layer - 1;  // 2

                for (var i = start; i < end; i++) // 0 -> 4, 1 -> 3
                {
                    var offset = i - start;
                    // 0, 1, 2, 3 ---- 0 - 0[0], 1 - 0[1], 2 - 0[2], 3 - 0[3]
                    // 0, 1       ---- 1 - 1[0], 2 - 1[1]

                    // save top
                    var top = matrix[start, i];
                    
                    // left to top
                    matrix[start, i] = matrix[end - offset, start];
                    
                    // bottom to left
                    matrix[end - offset, start] = matrix[end, end - offset];
                    
                    // right to bottom
                    matrix[end, end - offset] = matrix[i, end]; // i => changes

                    // top to right
                    matrix[i, end] = top; // i => moves from (start to end column), end => last column
                }
            }
        }
        
        public void RotateMatrixLeft(int[,] matrix, int n)
        {
            for (var layer = 0; layer < n / 2; layer++) // n size, n=3, 3/2= 1
            {
                var start = layer;        // 0, 1
                var end = n - layer - 1;  // 2

                for (var i = start; i < end; i++)
                {
                    var offset = i - start;
                    // 0, 1, 2, 3 ---- 0 - 0[0], 1 - 0[1], 2 - 0[2], 3 - 0[3]
                    // 0, 1       ---- 1 - 1[0], 2 - 1[1]

                    // save top
                    var top = matrix[start, i];
                    
                    // right to top
                    matrix[start, i] = matrix[i, end];
                    
                    // bottom to right
                    matrix[i, end] = matrix[end, end - offset];
                    
                    // left to bottom
                    matrix[end, end - offset] = matrix[end - offset, start];

                    // top to left
                    matrix[end - offset, start] = top;
                }
            }
        }

        public int[,] CreateMatrix(int rows, int columns)
        {
            var matrix = new int[rows, columns];
            var value = 0;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    matrix[i, j] = value++;
                }
            }
            return matrix;
        }

        public void PrintMatrix(int[,] matrix)
        {
            int bound0 = matrix.GetUpperBound(0); // rows
            int bound1 = matrix.GetUpperBound(1); // columns

            for (var i = 0; i <= bound0; i++)
            {
                for (var j = 0; j <= bound1; j++)
                {
                    Console.Write($"{matrix[i, j]}|");
                }
                Console.WriteLine();
            }
        }
    }
}