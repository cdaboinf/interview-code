using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    public class LeetCodeTest
    {
        public LeetCodeTest()
        {
        }

        /*
         * Remove Duplicates from Sorted Array
         * 
         * Given a sorted array nums, remove the duplicates in-place such that each element appear only once and return the new length.
         * Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.
         */
        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length == 0)
                return 0;

            int i = 0;

            for (int j = 1; j < nums.Length; j++)
            {
                if (nums[j] != nums[i])
                {
                    i++;
                    nums[i] = nums[j];
                }
            }

            return i + 1;
        }

        /*
         * Best Time to Buy and Sell Stock II
         *
         * Say you have an array prices for which the ith element is the price of a given stock on day i.
         * Design an algorithm to find the maximum profit. You may complete as many transactions as you like
         * (i.e., buy one and sell one share of the stock multiple times).
         */
        public int MaxProfit(int[] prices)
        {
            int maxprofit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] > prices[i - 1])
                    maxprofit += prices[i] - prices[i - 1];
            }

            return maxprofit;
        }

        /*
         * Rotate Array
         * 
         * Given an array, rotate the array to the right by k steps, where k is non-negative.

         * Follow up:
         * Try to come up as many solutions as you can, there are at least 3 different ways to solve this problem.
         * Could you do it in-place with O(1) extra space?
         * After reversing all numbers     : 7 6 5 4 3 2 1
         * After reversing first k numbers : 5 6 7 4 3 2 1
         * After revering last n-k numbers : 5 6 7 1 2 3 4 --> Result
         */
        public void Rotate(int[] nums, int k)
        {
            k %= nums.Length;
            reverse(nums, 0, nums.Length - 1);
            reverse(nums, 0, k - 1);
            reverse(nums, k, nums.Length - 1);
        }
        
        private void reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                int temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }
        }

        /*
         * Contains Duplicate
         *
         * Given an array of integers, find if the array contains any duplicates.
         *
         * Your function should return true if any value appears at least twice in the array,
         * and it should return false if every element is distinct.
         */
        public bool ContainsDuplicate(int[] nums)
        {
            var set = new HashSet<int>(nums.Length);
            for (int x = 0; x < nums.Length; x++)
            {
                if (set.Contains(x))
                    return true;
                set.Add(x);
            }

            return false;
        }

        /*
         * Single Number
         *
         * Given a non-empty array of integers, every element appears twice except for one. Find that single one.
         * Your algorithm should have a linear runtime complexity. Could you implement it without using extra memory?
         */
        public int SingleNumber(int[] nums)
        {
            var hash_table = new Hashtable();

            for (int i = 0; i < nums.Length; i++)
            {
                if (hash_table.ContainsKey(nums[i]))
                {
                    hash_table[nums[i]] = (int) hash_table[nums[i]] + 1;
                }

                hash_table.Add(i, 1);
            }

            for (int j = 0; j < nums.Length; j++)
                if ((int) hash_table[j] == 1)
                {
                    return j;
                }

            return 0;
        }

        /*
         * Intersection of Two Arrays II
         *
         * Given two arrays, write a function to compute their intersection.            
         */
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            var num1Values = new Hashtable();
            var instersection = new List<int>();

            for (var i = 0; i < nums1.Length; i++)
            {
                if (num1Values.ContainsKey(nums1[i]))
                {
                    num1Values[nums1[i]] = (int) num1Values[nums1[i]] + 1;
                }
                else
                {
                    num1Values.Add(nums1[i], 1);
                }
            }

            for (var i = 0; i < nums2.Length; i++)
            {
                if (num1Values.ContainsKey(nums2[i]))
                {
                    var count = (int) num1Values[nums2[i]];
                    if (count > 0)
                    {
                        num1Values[nums2[i]] = count - 1;
                        instersection.Add(nums2[i]);
                    }
                }
            }

            return instersection.ToArray();
        }

        /*
         * Plus One
         *
         * Given a non-empty array of digits representing a non-negative integer, plus one to the integer.
         * The digits are stored such that the most significant digit is at the head of the list, and each element in the array contain a single digit.
         * You may assume the integer does not contain any leading zero, except the number 0 itself.
         */
        public int[] PlusOne(int[] digits)
        {
            var chars = new char[digits.Length];
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i] = digits[i] + 1;
                    return digits;
                }

                digits[i] = 0;
            }

            var newDigits = new int[digits.Length + 1];
            newDigits[0] = 1;

            return newDigits;
        }

        /*
         * Move Zeroes
         *
         * Given an array nums, write a function to move all 0's to the end of it while maintaining the relative order of the non-zero elements.
         */
        public void MoveZeroes(int[] nums)
        {
            var nonz = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    nums[nonz++] = nums[i];
                }
            }

            for (var i = nonz; i < nums.Length; i++)
            {
                nums[i] = 0;
            }
            /* brute force
            for (var i = 0; i < nums.Length; i++)
            {  
                if(nums[i] == 0)
                {
                    for (var j = i+1; i < nums.Length; i++)
                    {
                        if(nums[i] != 0)
                        {
                            var temp = nums[i];
                            nums[i] = nums[j];
                            nums[j] = temp;
                            break;
                        }                        
                    }
                }
            }
            */

            /* brute force
            for (var i = 0; i < nums.Length; i++)
            {  
                if(nums[i] == 0)
                {
                    for (var j = i+1; i < nums.Length; i++)
                    {
                        if(nums[i] != 0)
                        {
                            var temp = nums[i];
                            nums[i] = nums[j];
                            nums[j] = temp;
                            break;
                        }                        
                    }
                }
            }
            */

            /* most efficient
            var write = 0; 1,2,0,4
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    var temp = nums[i];
                    nums[i] = nums[write];
                    nums[write] = temp;
                    write++;
                }
            }
            */
        }

        /*
         * Two Sum
         *
         * Given an array of integers, return indices of the two numbers such that they add up to a specific target.
         * You may assume that each input would have exactly one solution, and you may not use the same element twice.
         */
        public int[] TwoSum(int[] nums, int target)
        {
            var keys = new Hashtable();
            for (var i = 0; i < nums.Length; i++)
            {
                var eval = target - nums[i];
                if (keys.Count != 0 && keys.ContainsKey(eval))
                {
                    return new int[] {(int) keys[eval], i};
                }
                else
                {
                    if (keys.ContainsKey(nums[i]))
                    {
                        keys[nums[i]] = nums[i];
                    }
                    else
                    {
                        keys.Add(nums[i], i);
                    }
                }
            }

            return new int[0];
        }
    }
}