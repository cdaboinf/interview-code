using System;
using System.Collections.Generic;
using System.Linq;

namespace interview_code
{
    public class HackerRankArrayProblems
    {
        /*
         * 2D Array - DS
         *
         * Given a 6x6 2D Array, : We define an hourglass in  to be a subset of values with indices falling in this pattern in 's graphical representation:
         * a b c
         *   d
         *  e f g
         */
        public int HourglassSum(int[][] arr)
        {
            var maxSum = new List<int>();
            var sum = 0;
            var row_index = 0;
            var col_index = 0;
            for (var row = 0; row < arr.Length - 2; row++)
            {
                for (var col = 0; col < arr[row].Length - 2; col++)
                {
                    sum = arr[col][row] + arr[col][row + 1] + arr[col][row + 2]
                          + arr[col + 1][row + 1]
                          + arr[col + 2][row] + arr[col + 2][row + 1] + arr[col + 2][row + 2];
                    maxSum.Add(sum);
                }
            }

            return maxSum.Max(s => s);
        }

        /*
         * Minimum Swaps 2
         *
         * You are given an unordered array consisting of consecutive integers  [1, 2, 3, ..., n] without any duplicates.
         * You are allowed to swap any two elements. You need to find the minimum number of swaps required to sort the array in ascending order.
         */
        public int MinimumSwaps(int[] arr)
        {
            var n = arr.Length - 1;
            var minSwaps = 0;
            var visited = new Dictionary<int, bool>();
            for (var i = 0; i < arr.Length; i++)
            {
                visited.Add(i, false);
            }

            for (var i = 0; i < arr.Length; i++)
            {
                if (!visited[i])
                {
                    var a = i;
                    var b = arr[i] - 1; // where it wants to be, 1,4,3,2 => arr[2] => 4 - 1 = 3, then arr[3] = should be 4 
                    var length = 1;
                    visited[i] = true;

                    while (b != i)
                    {
                        visited[b] = true;
                        a = b; // move to index that wants to be
                        b = arr[b] - 1; // where it wants to be
                        length++;
                    }

                    minSwaps += length - 1;
                }
            }

            return minSwaps;
        }

        public int MinimumSwapsRecursive(int[] arr, int start, int sum)
        {
            if (start == arr.Length)
            {
                return sum;
            }

            var min = int.MaxValue;
            var swapIndex = 0;
            for (var i = start; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    swapIndex = i;
                }
            }

            if (swapIndex == start)
            {
                Console.WriteLine("no swap");
                return MinimumSwapsRecursive(arr, start + 1, sum);
            }

            Console.WriteLine("swap");
            var temp = arr[start];
            arr[start] = min;
            arr[swapIndex] = temp;
            sum += 1;
            return MinimumSwapsRecursive(arr, start + 1, sum);
        }
        
        public long ArrayManipulation(int n, int[][] queries)
        {
            return 0;
        }
        
        /*
         * O(d*n)
         */
        static int[] RotateLeft(int[] a, int d) {        
            for(var j=0; j<d; j++){
                var temp = a[0];
                for(var i=0; i<a.Length - 1; i++){
                    a[i] = a[i+1];
                }
                a[a.Length-1] = temp;
            }
            return a;
        }
        
        /*
         * O(n)
         */
        public int[] RotLeft(int[] a, int d) {        
            d %= a.Length;
            Reverse(a, 0, d-1);
            Reverse(a, d, a.Length-1);
            Reverse(a, 0, a.Length-1);
            return a;
        }
        private void Reverse(int[] a, int s, int e) {                    
            while(s<e){
                var temp = a[s];
                a[s] = a[e];
                a[e] = temp;
                s++;
                e--;
            }
        }
    }
}