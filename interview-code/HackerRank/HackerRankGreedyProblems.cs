using System;

namespace interview_code
{
    /*
     * for array problems try to sort them
     */
    public class HackerRankGreedyProblems
    {
        public int MinimumAbsoluteDifference(int[] arr)
        {
            if (arr.Length < 2 || arr.Length > Math.Pow(10, 5))
            {
                return 0;
            }

            Array.Sort(arr);
            int min = arr[arr.Length - 1] - arr[0];
            for (var i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] < Math.Pow(-10, 9) || arr[i] > Math.Pow(10, 9))
                {
                    continue;
                }

                if (arr[i] != arr[i + 1])
                {
                    var calc = Math.Abs(arr[i] - arr[i + 1]);
                    if (calc < min)
                    {
                        min = calc;
                    }
                }
            }

            return min;
        }

        /*
         * You will be given a list of integers, arr , and a single integer k.
         * You must create an array of length  from elements of arr such that its unfairness is minimized.
         * Call that array subarr. Unfairness of an array is calculated as
         *
         * max(subarr) - min(subarr)
         */
        public int MaxMin(int k, int[] arr)
        {
            if (arr.Length < 2 || arr.Length > Math.Pow(10, 5))
            {
                return 0;
            }

            if (k < 2 || k > arr.Length)
            {
                return 0;
            }

            Array.Sort(arr);
            var min = int.MaxValue;
            for (var i = 0; i + k - 1 < arr.Length; i++)
            {
                if (arr[i] < 0 || arr[i] > Math.Pow(10, 9))
                {
                    continue;
                }

                var calc = arr[(i + k) - 1] - arr[i];
                if (calc < min)
                {
                    min = calc;
                }
            }

            return min;
        }
    }
}