using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace interview_code.CompanyTechInterviews
{
    public class Facebook
    {
        public static bool IsAlienSorted(string[] words, string order)
        {
            var dictionary = new Hashtable();
            for (var i = 0; i < order.Length; i++)
            {
                if (!dictionary.ContainsKey(order[i]))
                {
                    dictionary.Add(order[i], i);
                }
            }

            for (var z = 0; z < words.Count() - 1; z++)
            {
                for (var j = 0; j < words[z].Length; j++)
                {
                    var w1 = words[z][j];
                    if (j >= words[z + 1].Length)
                    {
                        return false;
                    }

                    var w2 = words[z + 1][j];
                    if (w1 != w2)
                    {
                        if ((int) dictionary[w1] > (int) dictionary[w2])
                        {
                            return false;
                        }

                        break;
                    }
                }
            }

            return true;
        }

        public string MinRemoveToMakeValid(string s)
        {
            var value = new StringBuilder("");

            if (s == "")
            {
                return s;
            }

            var oParenthesis = 0;
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    oParenthesis++;
                }
                else if (s[i] == ')')
                {
                    if (oParenthesis == 0)
                    {
                        continue;
                    }

                    oParenthesis--;
                }

                value.Append(s[i]);
            }

            var result = new StringBuilder("");
            for (var i = value.Length - 1; i >= 0; i--)
            {
                if (value[i] == '(' && oParenthesis > 0)
                {
                    oParenthesis--;
                    continue;
                }

                result.Append(value[i]);
            }

            var characters = result.ToString().ToCharArray();
            Array.Reverse(characters);
            return new string(characters);
        }

        public int LeftMostColumnWithOne(BinaryMatrix binaryMatrix)
        {
            var size = binaryMatrix.Dimensions();
            var result = -1;
            var row = size[0] - 1;
            var col = size[1] - 1;

            while (row >= 0 && col >= 0)
            {
                if (binaryMatrix.Get(row, col) == 0)
                {
                    row--;
                }
                else if (binaryMatrix.Get(row, col) == 1)
                {
                    result = col;
                    col--;
                }
            }

            return result;
        }

        // O(log n)
        static int binarySearch(int[] arr, int l, int r, int x)
        {
            if (r >= l)
            {
                int mid = l + (r - l) / 2;

                // If the element is present at the
                // middle itself
                if (arr[mid] == x)
                    return mid;

                // If element is smaller than mid, then
                // it can only be present in left subarray
                if (arr[mid] > x)
                    return binarySearch(arr, l, mid - 1, x);

                // Else the element can only be present
                // in right subarray
                return binarySearch(arr, mid + 1, r, x);
            }

            // We reach here when element is not present
            // in array
            return -1;
        }

        public int[][] KClosest(int[][] points, int K)
        {
            // get distances
            int N = points.Length;
            int[] dists = new int[N];
            for (int i = 0; i < N; ++i)
                dists[i] = dist(points[i]);

            // sort distances
            Array.Sort(dists);

            // set max/limit value
            int distK = dists[K - 1];

            // create response object 
            int[][] ans = new int[K][];
            for (int i = 0; i < K; ++i)
            {
                ans[i] = new int[2];
            }

            // set response object
            int t = 0;
            for (int i = 0; i < N; ++i)
                if (dist(points[i]) <= distK)
                    ans[t++] = points[i];
            return ans;
        }

        private int dist(int[] point)
        {
            return point[0] * point[0] + point[1] * point[1];
        }

        public bool ValidPalindrome(string s)
        {
            var left = 0;
            var right = s.Length - 1;
            while (left < right)
            {
                if (s[left] != s[right])
                {
                    var temp = s;
                    var r1 = temp.Remove(left, 1);
                    temp = s;
                    var r2 = temp.Remove(right, 1);
                    return IsValid(r1) || IsValid(r2);
                }

                left++;
                right--;
            }

            return true;
        }

        private bool IsValid(string s)
        {
            var left = 0;
            var right = s.Length - 1;
            while (left < right)
            {
                if (s[left] != s[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        public string AddStrings(string num1, string num2)
        {
            var result = new StringBuilder("");
            var int1 = num1.Length - 1;
            var int2 = num2.Length - 1;
            var res = 0;

            while (int1 >= 0 || int2 >= 0)
            {
                var sum = res;
                if (int1 >= 0)
                {
                    sum += num1[int1] - '0';
                }

                if (int2 >= 0)
                {
                    sum += num2[int2] - '0';
                }

                int1--;
                int2--;

                result.Append(sum % 10);
                res = sum / 10;
            }

            if (res != 0)
            {
                result.Append(res);
            }

            var characters = result.ToString().ToCharArray();
            Array.Reverse(characters);
            return new string(characters);
        }

        public string AddBinary(string a, string b)
        {
            var result = new StringBuilder();
            var carry = 0;
            var numsa = a.Length - 1;
            var numsb = b.Length - 1;

            while (numsa >= 0 || numsb >= 0)
            {
                var sum = carry;
                if (numsa >= 0)
                {
                    sum += a[numsa] - '0';
                }

                if (numsb >= 0)
                {
                    sum += b[numsb] - '0';
                }

                result.Insert(0, sum % 2);
                carry = sum / 2;

                numsa--;
                numsb--;
            }

            if (carry > 0)
            {
                result.Insert(0, carry);
            }

            return result.ToString();
        }

        public int SubarraySum(int[] nums, int k)
        {
            var result = 0;
            var sum = 0;
            var map = new Hashtable();
            map.Add(0, 1);

            for (var i = 0; i < nums.Length; i++)
            {
                sum = sum + nums[i];
                if (map.ContainsKey(sum - k))
                {
                    result += (int) map[sum - k];
                }

                if (map.ContainsKey(sum))
                {
                    map[sum] = (int) map[sum] + 1;
                }
                else
                {
                    map.Add(sum, 1);
                }
            }

            return result;
        }

        public int[] ProductExceptSelf1(int[] nums)
        {
            var result = new int[nums.Length];
            if (nums.Length < 2 || nums.Length > 105)
                return result;

            for (var i = 0; i < nums.Length; i++)
            {
                var prod1 = 1;
                var prod2 = 1;
                for (var j = i - 1; j >= 0; j--)
                {
                    prod1 *= nums[j];
                }

                for (var z = i + 1; z < nums.Length; z++)
                {
                    prod2 *= nums[z];
                }

                result[i] = prod1 * prod2;
            }

            return result;
        }

        public int[] ProductExceptSelf2(int[] nums)
        {
            var result = new int[nums.Length];
            var prod1 = new int[nums.Length];
            prod1[0] = 1;
            var prod2 = new int[nums.Length];
            prod2[nums.Length - 1] = 1;

            if (nums.Length < 2 || nums.Length > Math.Pow(10, 5))
                return result;

            for (var i = 1; i < nums.Length; i++)
            {
                prod1[i] = prod1[i - 1] * nums[i - 1];
            }

            for (var i = nums.Length - 2; i >= 0; i--)
            {
                prod2[i] = prod2[i + 1] * nums[i + 1];
            }

            for (var i = 0; i < nums.Length; i++)
            {
                result[i] = prod1[i] * prod2[i];
            }

            return result;
        }

        public int[] ProductExceptSelf3(int[] nums)
        {
            var result = new int[nums.Length];
            result[0] = 1;

            if (nums.Length < 2 || nums.Length > Math.Pow(10, 5))
                return result;

            for (var i = 1; i < nums.Length; i++)
            {
                result[i] = result[i - 1] * nums[i - 1];
            }

            var right = 1;
            for (var i = nums.Length - 1; i >= 0; i--)
            {
                result[i] = result[i] * right;
                right = right * nums[i];
            }

            return result;
        }

        public int RangeSumBST(TreeNode root, int low, int high)
        {
            int ans = 0;
            dfs(root, low, high, ref ans);
            return ans;
        }

        private void dfs(TreeNode node, int L, int R, ref int ans)
        {
            if (node != null)
            {
                if (L <= node.val && node.val <= R)
                    ans += node.val;
                if (L < node.val)
                    dfs(node.left, L, R, ref ans);
                if (node.val < R)
                    dfs(node.right, L, R, ref ans);
            }
        }

        public bool IsPalindrome(string s)
        {
            var left = 0;
            var right = s.Length - 1;
            while (left < right)
            {
                while (right > 0 && !IsAlphanumeric(s[right]))
                {
                    right--;
                }

                while (left < s.Length - 1 && !IsAlphanumeric(s[left]))
                {
                    left++;
                }

                if (IsAlphanumeric(s[right])
                    && IsAlphanumeric(s[left])
                    && s[left].ToString().ToLower() != s[right].ToString().ToLower())
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        private bool IsAlphanumeric(char c)
        {
            return Regex.IsMatch(c.ToString(), "^[a-zA-Z0-9]*$");
        }

        public int SubarraySum(int[] nums)
        {
            var sum = 0;

            for (var i = 0; i < nums.Length; i++) // (n-i) + (n-i)*i // var subSum = nums[i] * (i+1) * (n-i);
            {
                var subSum = 0;
                for (var j = i; i < nums.Length; i++)
                {
                    subSum += nums[j];
                    sum += subSum;
                }
            }

            return sum;
        }

        public int AverageSubarray(int[] nums)
        {
            var avg = 0;
            var indexes = new List<List<int>>();
            for (var i = 0; i < nums.Length; i++)
            {
                var subAvg = 0;
                var subSum = 0;
                var count = 1;
                for (var j = i; j < nums.Length; j++)
                {
                    indexes.Add(new List<int> {i, j});
                    subSum += nums[j];
                    subAvg = subSum / count++;
                    avg += subAvg;
                }
            }

            return avg;
        }

        public List<List<int>> AboveAverageSubarray(int[] nums)
        {
            var result = new List<List<int>>();
            var avg = 0;
            var indexes = new List<List<int>>();
            for (var i = 0; i < nums.Length; i++)
            {
                var subAvg = 0;
                var subSum = 0;
                var count = 1;
                for (var j = i; j < nums.Length; j++)
                {
                    indexes.Add(new List<int> {i, j});

                    subSum += nums[j];
                    subAvg = subSum / count++;
                    var leftAvg = i - 1 < 0 ? 0 : GetAverageSubarray(nums, 0, i - 1);
                    var rightAvg = j + 1 >= nums.Length ? 0 : GetAverageSubarray(nums, j + 1, nums.Length);
                    if (subAvg >= leftAvg && subAvg >= rightAvg)
                    {
                        result.Add(new List<int> {i, j});
                    }
                }
            }

            return result;
        }

        public int GetAverageSubarray(int[] nums, int start, int end)
        {
            var avg = new List<int>();
            for (var i = start; i < end; i++)
            {
                var subAvg = 0;
                var subSum = 0;
                var count = 1;
                for (var j = i; j < end; j++)
                {
                    subSum += nums[j];
                    subAvg = subSum / count++;
                    avg.Add(subAvg);
                }
            }

            avg.Sort();
            avg.Reverse();
            return avg.Any() ? avg[0] : 0;
        }

        /*
            * given an array of positive numbers, *without duplicate,* and a target number
            * find unique combinations that sum to the target
            * number can be used multiple times
            Example:
            candidates = [2,3], target = 8, 
            solution is  [[2,2,2,2], [2,3,3]],
         */
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> result = new List<IList<int>>();
            GetResultFromTarget(target, candidates, 0, new List<int>(), result);
            return result;
        }

        private void GetResultFromTarget(
            int target,
            int[] candidates,
            int index,
            List<int> partialResult,
            IList<IList<int>> result)
        {
            if (target < 0)
            {
                return;
            }

            if (target == 0)
            {
                var newList = new List<int>(partialResult);
                result.Add(newList);
                return;
            }

            for (var x = index; x < candidates.Length; x++)
            {
                partialResult.Add(candidates[x]);
                GetResultFromTarget(target - candidates[x], candidates, x, partialResult, result);
                partialResult.RemoveAt(partialResult.Count - 1);
            }
        }
    }
}

public class BinaryMatrix
{
    public int Get(int row, int col)
    {
        return 0;
    }

    public List<int> Dimensions()
    {
        return new List<int>();
    }
}