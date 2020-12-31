using System;

namespace interview_code
{
    public class HackerRankDynamicProblems
    {
        public int MaxSubsetSum(int[] arr)
        {
            var sums = new int[arr.Length];
            sums[0] = arr[0];
            sums[1] = Math.Max(arr[1], sums[0]);

            for (var i = 2; i < arr.Length; i++)
            {
                var csum = arr[i] + arr[i - 2];
                sums[i] = Math.Max(csum, sums[i - 1]);
            }
            
            return sums[arr.Length-1];
        }
    }
}