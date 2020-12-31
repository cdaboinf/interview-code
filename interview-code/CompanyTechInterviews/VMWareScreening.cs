using System;
using System.Collections;

namespace interview_code.CompanyTechInterviews
{
    public class VMWareScreening
    {
        public VMWareScreening()
        {
        }

        public int IntExe(int x)
        {
            return x;   
        }

        public bool BoolExe(int x)
        {
            var myHT = new Hashtable();
            myHT.Add("one", 10);
            myHT.Add("two", 2);
            myHT.Add("three", 3);
            myHT.Add("four", 4);

            var max = int.MinValue;
            foreach(var val in myHT.Values)
            {
                var num = (int)val;
                if (max < num)
                {
                    max = num;
                }
            }

            return true;
        }

        public int FindShortestSubArray(int[] nums)
        {
            var left = new Hashtable();
            var right = new Hashtable();
            var count = new Hashtable();
            var ans = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                var val = nums[i];
                // left
                if (!left.ContainsKey(val))
                {
                    left.Add(val, i);
                }
                // right
                if (right.ContainsKey(val))
                {
                    right[val] = i;
                }
                else
                {
                    right.Add(val, i);
                }
                // count of value repetititon
                if (count.ContainsKey(val))
                {
                    count[val] = (int)count[val] + 1;
                }
                else
                {
                    count.Add(val, 1);
                }
            }
            // get max frequency
            int max = GetMaxFrequency(count);
            // get shortest sub-array
            ans = ShortestSubArray(count, left, right, nums.Length, max);
            return ans;
        }

        private int GetMaxFrequency(Hashtable frequencies)
        {
            var max = int.MinValue;
            foreach (var val in frequencies.Values)
            {
                var num = (int)val;
                if (max < num)
                {
                    max = num;
                }
            }
            return max;
        }

        private int ShortestSubArray(
            Hashtable frequencies,
            Hashtable left,
            Hashtable right,
            int ans,
            int max)
        {
            foreach (var val in frequencies.Values)
            {
                var num = (int)val;
                if (num == max)
                {
                    return Math.Min(ans, ((int)right[num] - (int)left[num]) + 1);
                }
            }
            return ans;
        }

    }
}
