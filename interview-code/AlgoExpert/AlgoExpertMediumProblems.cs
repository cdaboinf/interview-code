using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace interview_code.AlgoExpert
{
    public class AlgoExpertMediumProblems
    {
        /*
         * find target sum of triplet and return sorted triplet items, and sorted triplet
         * if the sum is found
         */
        public List<int[]> ThreeNumberSum(int[] array, int targetSum)
        {
            Array.Sort(array);
            var triplets = new List<int[]>();
            for (var i = 0; i < array.Length - 2; i++)
            {
                var left = i + 1;
                var right = array.Length - 1;
                while (left < right)
                {
                    var currentSum = array[i] + array[left] + array[right];
                    if (currentSum == targetSum)
                    {
                        var value = new int[] {array[i], array[left], array[right]};
                        triplets.Add(value);
                        left++;
                        right--;
                    }
                    else if (currentSum > targetSum)
                    {
                        right--;
                    }
                    else
                    {
                        left++;
                    }
                }
            }

            return triplets;
        }

        /*
         * smallest difference between the substraction of one element of each array
         */
        public int[] SmallestDifference(int[] arrayOne, int[] arrayTwo)
        {
            var result = new int[2];
            var min = int.MaxValue;

            Array.Sort(arrayOne);
            Array.Sort(arrayTwo);

            var startOne = 0;
            var startTwo = 0;

            while (startOne < arrayOne.Length && startTwo < arrayTwo.Length)
            {
                var operation = Math.Abs(arrayOne[startOne] - arrayTwo[startTwo]);
                var one = arrayOne[startOne];
                var two = arrayTwo[startTwo];
                if (one == two)
                {
                    result[0] = arrayOne[startOne];
                    result[1] = arrayTwo[startTwo];
                    break;
                }
                else if (one < two)
                {
                    startOne++;
                }
                else
                {
                    startTwo++;
                }

                if (min > operation)
                {
                    min = operation;
                    result[0] = one;
                    result[1] = two;
                }
            }

            return result;
        }

        /*
         * move number to back in place
         */
        public List<int> MoveElementToEnd(List<int> array, int toMove)
        {
            // Write your code here.
            var boundary = 0;

            for (var i = 0; i < array.Count; i++)
            {
                if (array[i] != toMove)
                {
                    var temp = array[i];
                    array[i] = array[boundary];
                    array[boundary] = temp;
                    boundary++;
                }
            }

            return array;
        }

        /*
         * monotonic array only increases or decreases
         */
        public bool IsMonotonic(int[] array)
        {
            if (array.Length == 1 || array.Length == 2 || array.Length == 0)
                return true;

            var direction = array[1] - array[0];

            for (var i = 1; i < array.Length - 1; i++)
            {
                if (direction == 0)
                {
                    direction = array[i + 1] - array[i];
                    continue;
                }

                // increasing direction
                if (direction > 0 && array[i + 1] < array[i])
                {
                    return false;
                }

                // decreasing direction
                if (direction < 0 && array[i + 1] > array[i])
                {
                    return false;
                }
            }

            return true;
        }

        public List<int> SpiralTraverse(int[,] array)
        {
            var rowStart = 0;
            var rowEnd = array.GetUpperBound(0); // rows
            var colStart = 0;
            var colEnd = array.GetUpperBound(1); // cols

            var result = new List<int>();
            SpiralTraverse(array, rowStart, rowEnd, colStart, colEnd, result);

            return result;
        }

        /*
         * traverse 2d array as a spiral
         *
         */
        private List<int> SpiralTraverse(
            int[,] array,
            int rowStart,
            int rowEnd,
            int colStart,
            int colEnd,
            List<int> result)
        {
            if (rowStart > rowEnd || colStart > colEnd)
            {
                return result;
            }

            for (int j = colStart; j <= colEnd; j++)
            {
                result.Add(array[rowStart, j]);
            }

            for (int z = rowStart + 1; z <= rowEnd; z++)
            {
                result.Add(array[z, colEnd]);
            }

            for (int y = colEnd - 1; y >= colStart; y--)
            {
                if (rowStart == rowEnd)
                {
                    break;
                }

                result.Add(array[rowEnd, y]);
            }

            for (int k = rowEnd - 1; k > rowStart; k--)
            {
                if (colStart == colEnd)
                {
                    break;
                }

                result.Add(array[k, colStart]);
            }

            rowStart++;
            rowEnd--;
            colStart++;
            colEnd--;
            //Console.WriteLine($"r1: {rowStart} r2: {rowEnd} c1: {colStart} c2: {colEnd}");
            return SpiralTraverse(array, rowStart, rowEnd, colStart, colEnd, result);
        }

        /*
         * find longest peak, 
         * A peak is defined as adjacent integers in the array that are strictly 
         * increasing until they reach a tip (the highest value in the peak), at which
         * point they become  decreasing. At least three integers are required to
         * form a peak.
         */
        public int LongestPeak(int[] array)
        {
            var maxLength = 0;

            for (var i = 1; i < array.Length - 1; i++)
            {
                var isPeak = array[i - 1] < array[i] && array[i] > array[i + 1];
                if (!isPeak)
                {
                    continue;
                }

                var leftPeak = 0;
                var rightPeak = 0;
                var index = i;
                while (index > 0 && array[index - 1] < array[index])
                {
                    leftPeak++;
                    index--;
                }

                index = i;
                while (index < array.Length - 1 && array[index] > array[index + 1])
                {
                    rightPeak++;
                    index++;
                }

                maxLength = Math.Max(maxLength, (leftPeak + rightPeak) + 1);
                i = index;
            }

            return maxLength;
        }

        /*          
         * Write a function that takes in an array of positive integers and returns the
         * maximum sum of non-adjacent elements in the array. (dynamic programming)
         */
        public int MaxSubsetSumNoAdjacent(int[] array)
        {
            // edge cases
            if (array.Length == 0 || array == null)
            {
                return 0;
            }

            if (array.Length == 1)
            {
                return array[0];
            }

            var maxSums = new int[array.Length];
            //base cases
            maxSums[0] = array[0];
            maxSums[1] = Math.Max(array[0], array[1]);

            for (var i = 2; i < array.Length; i++)
            {
                var sum1 = maxSums[i - 1];
                var sum2 = maxSums[i - 2] + array[i];
                maxSums[i] = Math.Max(sum1, sum2);
            }

            return maxSums[array.Length - 1];
        }

        /*
         * Given an array of positive integers representing coin denominations and a
         * single non-negative integer,  representing a target amount of
         * money, write a function that returns the number of ways to make change for
         * that target amount using the given coin denominations.
         * 
         */
        public int NumberOfWaysToMakeChange(int n, int[] denoms)
        {
            // zero index
            var ways = new int[n + 1];
            ways[0] = 1;

            for (var j = 0; j < denoms.Length; j++)
            {
                for (var i = 1; i < ways.Length; i++)
                {
                    if (denoms[j] <= i)
                    {
                        ways[i] = ways[i] + ways[i - denoms[j]];
                    }
                }
            }

            return ways[ways.Length - 1];
        }

        /*         
         * Given an array of positive integers representing coin denominations and a
         * single non-negative integer representing a target amount of
         * money, write a function that returns the smallest number of coins needed to
         * make change for that target amount using the given coin denominations.
         */
        public int MinNumberOfCoinsForChange(int n, int[] denoms)
        {
            // zero index
            var nums = new int[n + 1];
            for (var z = 0; z < nums.Length; z++)
            {
                nums[z] = int.MaxValue;
            }

            nums[0] = 0;

            for (var j = 0; j < denoms.Length; j++)
            {
                for (var i = 1; i < nums.Length; i++)
                {
                    if (denoms[j] <= i)
                    {
                        // only process the value when its has been set 
                        // by denom index(1 + nums[i - denoms[j]] happened before)
                        var coins = nums[i - denoms[j]] != int.MaxValue ? 1 + nums[i - denoms[j]] : nums[i - denoms[j]];
                        nums[i] = Math.Min(nums[i], coins);
                    }
                }
            }

            return nums[nums.Length - 1] != int.MaxValue ? nums[nums.Length - 1] : -1;
        }

        /*
         * Levenshtein Distance
         *
         * Write a function that takes in two strings and returns the minimum number of
         * edit operations that need to be performed on the first string to obtain the
         * second string.
         */
        public int LevenshteinDistance(string str1, string str2)
        {
            // response
            var edits = new int[str2.Length + 1, str1.Length + 1];

            // build 2d array
            /*
             * 0,1,2,3,4,....
             * 1
             * 2
             * 3
             * 4
             * .
             * .
             * .
             */
            for (var i = 0; i < str2.Length + 1; i++)
            {
                for (var j = 0; j < str1.Length + 1; j++)
                {
                    edits[i, j] = j;
                }

                edits[i, 0] = i;
            }

            // solve
            for (var i = 1; i < str2.Length + 1; i++)
            {
                for (var j = 1; j < str1.Length + 1; j++)
                {
                    if (str2[i - 1] == str1[j - 1])
                    {
                        edits[i, j] = edits[i - 1, j - 1];
                    }
                    else
                    {
                        edits[i, j] = 1 + Math.Min(
                            edits[i - 1, j - 1],
                            Math.Min(edits[i - 1, j], edits[i, j - 1]));
                    }
                }
            }

            return edits[str2.Length, str1.Length];
        }

        /*
         * Kadanes Algorithm
         *
         * Write a function that takes in a non-empty array of integers and returns the
         * maximum sum that can be obtained by summing up all of the integers in a non-empty
         * subarray of the input array. A subarray must only contain adjacent numbers.
         */
        public int KadanesAlgorithm(int[] array)
        {
            var localMax = array[0];
            var globalMax = array[0];
            for (var i = 1; i < array.Length; i++)
            {
                var current = array[i];
                localMax = Math.Max(current, localMax + current);
                globalMax = Math.Max(globalMax, localMax);
            }

            return globalMax;
        }

        /*
         * HasSingleCycle extra-memory
         */
        public static bool HasSingleCycle(int[] array)
        {
            var n = array.Length;
            var jumps = 0;
            var start = 0;
            var visited = new int[n];
            for (var i = 0; i < n; i++)
            {
                visited[i] = 0;
            }

            visited[start] = 1;
            var index = 0;

            while (jumps < array.Length)
            {
                if (jumps > 0 && index == 0)
                {
                    return false;
                }

                // movement
                var move = array[index];
                var target = (move + index) % array.Length;
                if (target < 0)
                {
                    target = target + array.Length;
                }

                index = target;

                if (visited[index] > 1)
                {
                    return false;
                }

                visited[index] += 1;

                jumps++;
            }

            return visited[start] == 2;
        }

        /*
         * HasSingleCycle O(n) - O(1)
         */
        public static bool HasSingleCycle1(int[] array)
        {
            var jumps = 0;
            var index = 0;
            var start = 0;

            while (jumps < array.Length)
            {
                if (jumps > 0 && index == 0)
                {
                    return false;
                }

                jumps++;
                // movement
                var move = array[index];
                var target = (move + index) % array.Length;
                if (target < 0)
                {
                    target = target + array.Length; // (will be a subtraction);
                }

                index = target;
            }

            return index == 0;
        }

        /*         
         * You're given a two-dimensional array (a matrix) of potentially unequal height
         * and width containing only 0s and 1s. Each 0 represents land, and each 1  represents part of a
         * river. A river consists of any number of 1 s that are either
         * horizontally or vertically adjacent (but not diagonally adjacent). The number
         * of adjacent  1s forming a river determine its size. 
         * Write a function that returns an array of the sizes of all rivers represented
         * in the input matrix. The sizes don't need to be in any particular order.
         */
        public List<int> RiverSizes(int[,] matrix)
        {
            var visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            var rivers = new List<int>();
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (visited[row, col])
                    {
                        continue;
                    }
                    else
                    {
                        CalculateRivers(row, col, matrix, visited, rivers);
                    }
                }
            }

            return rivers;
        }

        private static void CalculateRivers(int row1, int col1, int[,] matrix, bool[,] visited, List<int> rivers)
        {
            int riverLength = 0;
            //var rivers = new List<int>();
            var neighbors = new Queue<int[]>();
            neighbors.Enqueue(new[] {row1, col1});
            while (neighbors.Count != 0)
            {
                var element = neighbors.Dequeue();
                var row = element[0];
                var col = element[1];
				  
                Console.WriteLine($"{row} -- {col}");

                if (visited[row,col])
                {
                    continue;
                }

                visited[row, col] = true;

                if (matrix[row, col] == 0)
                {
                    continue;
                }

                riverLength++;

                var elementNeighbors = GetUnvisitedNeighbors(row, col, matrix, visited);
                foreach (var neighbor in elementNeighbors)
                {
                    neighbors.Enqueue(neighbor);						
                }
            }
		
            if(riverLength > 0) {
                rivers.Add(riverLength);
            }
		  
            Console.WriteLine($"riverLength {riverLength}");
            //return riverLength;
        }
	
        private static List<int[]> GetUnvisitedNeighbors(int row, int col, int[,] matrix, bool[,] visited)
        {
            var neighbors = new List<int[]>();
            if (row > 0 && !visited[row - 1, col])
            {
                neighbors.Add(new[] {row - 1, col});
            }
		
            if (row < matrix.GetLength(0) - 1 && !visited[row + 1, col])
            {
                neighbors.Add(new[] {row + 1, col});
            }

            if (col < matrix.GetLength(1) - 1 && !visited[row, col + 1])
            {
                neighbors.Add(new[] {row, col + 1});
            }

            if (col > 0 && !visited[row, col - 1])
            {
                neighbors.Add(new[] {row, col - 1});
            }

            return neighbors;
        }
    }
}