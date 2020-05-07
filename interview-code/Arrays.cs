using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace interview_code
{
    public class Arrays
    {
        /*
            reverse traversal of array
        */
        public int[] ReplaceEvenWithDuplicate(int[] array)
        {
            var delimeter = GetIndexOfDelimeter(array);
            var end = array.Length - 1;
            var target = delimeter;

            while (delimeter >= 0)
            {
                if (array[delimeter] % 2 == 0)
                {
                    array[--end] = array[delimeter];
                }
                array[--end] = array[delimeter];
                delimeter--;
            }

            return array;
        }

        private int GetIndexOfDelimeter(int[] array)
        {
            int i = array.Length - 1;
            while (i >= 0 && array[i] == -1)
            {
                i--;
            }
            return i;
        }

        public string ReverseWords(string sentence)
        {
            /*
            var words = sentence.Split(' ');

            var end = words.Length - 1;
            var start = 0;

            while (start < end)
            {
                var temp = words[end];
                words[end] = words[start];
                words[start] = temp;

                start++;
                end--;
            }
            */

            var end = sentence.Length;
            StringBuilder result = new StringBuilder();

            for (var i = end - 1; i >= 0; i--)
            {
                if (sentence[i] == ' ')
                {
                    result.Append(sentence.Substring(i + 1, end - (i + 1)) + " ");
                    end = i;
                }
            }

            return result.ToString();
        }

        /*
            traversal of array from both ends
        */
        public int[] ReversElements(int[] array)
        {
            var end = array.Length - 1;
            var start = 0;

            while (start < end)
            {
                var temp = array[end];
                array[end] = array[start];
                array[start] = temp;

                start++;
                end--;
            }

            return array;
        }

        /*
            traversal of array from both ends: sorted array
        */
        public Tuple<int, int> FindTwoSumNumbers(int[] array, int sum)
        {
            var start = 0;
            var end = array.Length - 1;

            while (start < end)
            {
                if (array[start] + array[end] == sum)
                {
                    return new Tuple<int, int>(start, end);
                }
                if (array[start] + array[end] > sum)
                {
                    end--;
                }
                if (array[start] + array[end] < sum)
                {
                    start++;
                }
            }

            return null;
        }

        /*
            partitioning arrays
        */
        public int[] MoveNumberToFront(int[] array, int number)
        {
            var boundary = 0;

            for (var i = 0; i < array.Length; i++) // TODO: array.Length (i < array.Length)
            {
                if (array[i] == number)
                {
                    var temp = array[boundary];
                    array[boundary] = array[i];
                    array[i] = temp;

                    boundary++;
                }
            }

            return array;
        }

        public int[] MoveNumberToEnd(int[] array, int number)
        {
            var boundary = array.Length - 1;

            for (var i = array.Length - 1; i >= 0; i--) // TODO: array.Length (i >= 0)
            {
                if (array[i] == number)
                {
                    var temp = array[boundary];
                    array[boundary] = array[i];
                    array[i] = temp;

                    boundary--;
                }
            }

            return array;
        }

        public int[] PartionOnPivot(int[] array, int pivot)
        {
            var lb = 0;
            var hb = array.Length - 1;
            var i = 0;
            while (lb <= hb)
            {
                if (array[i] < pivot)
                {
                    var temp = array[lb];
                    array[lb] = array[i];
                    array[i] = temp;
                    lb++;

                    i++;
                }
                else if (array[i] > pivot)
                {
                    var temp = array[hb];
                    array[hb] = array[i];
                    array[i] = temp;

                    hb--;
                }
                else
                {
                    i++;
                }
            }
            return array;
        }

        /*
            sub-array: sliding window
        */
        public Tuple<int, int> FindSubArraySum(int[] array, int target)
        {
            int s = 0;
            int e = 0;
            int sum = array[0];

            while (s < array.Length)
            {
                if (s > e)
                {
                    e = s;
                    sum = array[s];
                }
                if (sum == target)
                {
                    return new Tuple<int, int>(s, e);
                }
                else if (sum < target)
                {
                    if (e == array.Length - 1)
                    {
                        break;
                    }
                    e++;
                    sum = sum + array[e];
                }
                else
                {
                    sum = sum - array[s];
                    s++;
                }
            }
            return null;
        }

        /*
            sub-array: sliding window
        */
        public Tuple<int, int> FindLongestSubstring(string val)
        {
            int s = 0;
            int e = 0;
            int sum = 1;
            var result = new Tuple<int, int>(0, 0);
            var uniques = new Hashtable();
            uniques.Add(val[0], 0);

            while (e < val.Length - 1)
            {
                e++;
                if (uniques.Contains(val[e]))
                {
                    // move until after index at position
                    var index = uniques[val[e]];
                    s = (int)index + 1;
                }

                uniques.Add(val[e], e);

                if (e - s + 1 > sum)
                {
                    sum = e - s + 1;
                    result = new Tuple<int, int>(s, e);
                }
            }
            return null;
        }


        /*
            sub-array: prefix-sum
        */
        public Tuple<int, int> FindContiguosSubArrayThatSumsToZero(int[] a, int n)
        {
            if (a == null)
            {
                return null;
            }
            var sum = 0;
            var pair = new Tuple<int, int>(0, 0);
            var sums = new Hashtable();
            for (var i = 0; i < a.Length; i++)
            {
                sum = sum + a[i];
                if (sum == 0)
                {
                    pair = new Tuple<int, int>(0, i);
                    return pair;
                }
                if (sums.ContainsKey(sum))
                {
                    pair = new Tuple<int, int>((int)sums[sum] + 1, i);
                    return pair;
                }
                sums.Add(sum, i);
            }
            return null;
        }

        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length < 2)
                return nums.Length;

            int index = 1;

            for (var i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] != nums[i + 1])
                {
                    nums[index++] = nums[i + 1];
                }
            }

            return index;
        }

        public int MaxProfitConsecutiveDays(int[] prices)
        {
            if (prices == null || prices.Length == 1)
            {
                return 0;
            }

            int profit = 0;

            for (var i = 0; i < prices.Length - 1; i++)
            {
                if (prices[i] < prices[i + 1])
                {
                    profit += prices[i + 1] - prices[i];
                }
            }
            return profit;
        }

        public int MaxSingleProfit(int[] prices)
        {
            int minprice = int.MaxValue;
            int maxprofit = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < minprice)
                    minprice = prices[i];
                else if (prices[i] - minprice > maxprofit)
                    maxprofit = prices[i] - minprice;
            }
            return maxprofit;
        }

        public class Solution {
    public void Rotate(int[] nums, int k) {
        for(var i=0; i<k; i++){
            RotateRight(nums, 0, nums.Length);
        }
    }
    
    private void RotateRight(int[]nums, int start, int end){
        for(var i=0; i < nums.Length; i++){
            var temp = nums[end];
            nums[end] = nums[start];
            nums[start] = temp;
        }
    }
}
    }
}