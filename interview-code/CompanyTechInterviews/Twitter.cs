using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;

namespace interview_code.CompanyTechInterviews
{
    public class Twitter
    {
    }

    public class TweetCounts
    {
        private readonly Hashtable _map;

        public TweetCounts()
        {
            _map = new Hashtable();
        }

        /// <summary>
        /// use a map map[tweetname] = Sorted List<int>(times)
        /// </summary>
        /// <param name="tweetName"></param>
        /// <param name="time"></param>
        public void RecordTweet(string tweetName, int time)
        {
            if (!_map.ContainsKey(tweetName))
            {
                _map.Add(tweetName, new List<int> {time});
            }
            else
            {
                var times = (List<int>) _map[tweetName];
                times.Add(time);
                times.Sort();

                _map[tweetName] = times;
            }
        }

        /// <summary>
        /// buckets => (endTime - startTime) / 60 + 1
        /// buckets tweets => buckets[(tweets[i] - startTime) / 60]++;
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="tweetName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public IList<int> GetTweetCountsPerFrequency(string freq, string tweetName, int startTime, int endTime)
        {
            if (freq == "minute")
            {
                var tweets = (List<int>) _map[tweetName];
                tweets = tweets.Where(t => t >= startTime && t <= endTime).ToList();

                var groups = (endTime - startTime) / 60 + 1;
                var buckets = new int [groups];

                for (var i = 0; i < tweets.Count(); i++)
                {
                    buckets[(tweets[i] - startTime) / 60]++;
                    // st: 30 sec  35 sec  [0-59,60-119,120-179,...]
                    // t-time - st = 5 sec / 60 sec [0], any under 60s = [0]
                }

                return buckets;
            }

            if (freq == "hour")
            {
                var tweets = (List<int>) _map[tweetName];
                tweets = tweets.Where(t => t >= startTime && t <= endTime).ToList();

                var groups = (endTime - startTime) / 3600 + 1;
                var buckets = new int [groups];

                for (var i = 0; i < tweets.Count(); i++)
                {
                    buckets[(tweets[i] - startTime) / 3600]++;
                }

                return buckets;
            }

            if (freq == "day")
            {
                var tweets = (List<int>) _map[tweetName];
                tweets = tweets.Where(t => t >= startTime && t <= endTime).ToList();

                var groups = (endTime - startTime) / (3600 * 24) + 1;
                var buckets = new int [groups];

                for (var i = 0; i < tweets.Count(); i++)
                {
                    if (tweets[i] >= startTime && tweets[i] <= endTime)
                    {
                        buckets[(tweets[i] - startTime) / (3600 * 24)]++;
                    }
                }

                return buckets;
            }

            return null;
        }
    }

    public class RandomizedSet
    {
        /*
         * HashMap<Integer, Integer> valueMap;
         * HashMap<Integer, Integer> idxMap;
         */
        Dictionary<int, int> valueMap;
        Dictionary<int, int> idxMap;

        /** Initialize your data structure here. */
        public RandomizedSet()
        {
            valueMap = new Dictionary<int, int>();
            idxMap = new Dictionary<int, int>();
        }

        /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
        public bool Insert(int val)
        {
            if (valueMap.ContainsKey(val))
            {
                return false;
            }

            valueMap.Add(val, valueMap.Count); // 4 : 1,  6 : 2
            idxMap.Add(idxMap.Count, val); // 1 : 4,  2 : 6

            return true;
        }

        /** Removes a value from the set. Returns true if the set contained the specified element. */
        public bool Remove(int val)
        {
            if (valueMap.ContainsKey(val))
            {
                int idx = valueMap[val];
                valueMap.Remove(val);
                idxMap.Remove(idx);

                var tailElem = idxMap.ContainsKey(idxMap.Count) ? idxMap[idxMap.Count] : (int?) null;
                if (tailElem != null)
                {
                    if (idxMap.ContainsKey(idx))
                    {
                        idxMap[idx] = tailElem.Value;
                    }
                    else
                    {
                        idxMap.Add(idx, tailElem.Value);
                    }

                    if (valueMap.ContainsKey(tailElem.Value))
                    {
                        valueMap[tailElem.Value] = idx;
                    }
                    else
                    {
                        valueMap.Add(tailElem.Value, idx);
                    }
                }

                return true;
            }

            return false;
        }

        /** Get a random element from the set. */
        public int GetRandom()
        {
            if (valueMap.Count == 0)
            {
                return -1;
            }

            if (valueMap.Count == 1)
            {
                return idxMap[0];
            }

            Random r = new Random();
            int idx = r.Next(valueMap.Count);

            return idxMap[idx];
        }
    }

    public class LogSystem
    {
        List<long[]> list;

        public LogSystem()
        {
            list = new List<long[]>();
        }

        public void Put(int id, string timestamp)
        {
            string[] vals = timestamp.Split(':');
            int[] st = vals.Select(v => Int32.Parse(v)).ToArray();
            list.Add(new long[] {Convert(st), id});
        }

        private long Convert(int[] st)
        {
            st[1] = st[1] - (st[1] == 0 ? 0 : 1); // months
            st[2] = st[2] - (st[2] == 0 ? 0 : 1); // days

            return (st[0] - 1999L) * (31 * 12) * 24 * 3600 // years
                   + st[1] * 31 * 24 * 3600 // months
                   + st[2] * 24 * 3600 // days
                   + st[3] * 3600 // hours
                   + st[4] * 60 // // minutes
                   + st[5]; // seconds
        }

        public IList<int> Retrieve(string s, string e, string granularity)
        {
            List<int> res = new List<int>();

            long start = Granularity(s, granularity, false);
            long end = Granularity(e, granularity, true);

            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i][0] >= start && list[i][0] < end)
                    res.Add((int) list[i][1]);
            }

            return res;
        }

        private long Granularity(string timeSpan, string granularity, bool end)
        {
            Dictionary<string, int> granularPrecision = new Dictionary<string, int>();
            granularPrecision.Add("Year", 0);
            granularPrecision.Add("Month", 1);
            granularPrecision.Add("Day", 2);
            granularPrecision.Add("Hour", 3);
            granularPrecision.Add("Minute", 4);
            granularPrecision.Add("Second", 5);

            string[] timeResponse = new String[] {"1999", "00", "00", "00", "00", "00"};
            string[] timeValues = timeSpan.Split(':');

            for (int i = 0; i <= granularPrecision[granularity]; i++)
            {
                timeResponse[i] = timeValues[i];
            }

            int[] secTimeVals = timeResponse.Select(v => Int32.Parse(v)).ToArray();

            if (end)
            {
                secTimeVals[granularPrecision[granularity]]++;
            }

            return Convert(secTimeVals);
        }
    }

    public class MyHashMap
    {
        // int % array.size = index location
        private int[] array = new int[1000001];

        /** Initialize your data structure here. */
        public MyHashMap()
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = -1;
            }
        }

        /** value will always be non-negative. */
        public void Put(int key, int value)
        {
            array[key] = value;
        }

        /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
        public int Get(int key)
        {
            return array[key];
        }

        /** Removes the mapping of the specified value key if this map contains a mapping for the key */
        public void Remove(int key)
        {
            array[key] = -1;
        }
    }

    public class InsertIntervalSolution
    {
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            var result = new List<int[]>();

            for (var i = 0; i < intervals.Length; i++)
            {
                if (intervals[i][1] < newInterval[0])
                {
                    result.Add(intervals[i]);
                }
                else if (newInterval[1] < intervals[i][0])
                {
                    result.Add(newInterval);
                    newInterval = intervals[i];
                }
                else
                {
                    newInterval[0] = Math.Min(intervals[i][0], newInterval[0]);
                    newInterval[1] = Math.Max(intervals[i][1], newInterval[1]);
                }
            }

            result.Add(newInterval);
            return result.ToArray();
        }

        public class MinIncrementForUniqueSolution
        {
            public int MinIncrementForUnique(int[] A)
            {
                var result = 0;
                Array.Sort(A);
                for (var i = 1; i < A.Length; i++)
                {
                    var pre = A[i - 1];
                    var current = A[i];

                    if (pre >= current)
                    {
                        result += pre - current + 1;
                        A[i] = pre + 1;
                    }
                }

                return result;
            }
        }
    }

    public class FindPairsSolution
    {
        public int FindPairs(int[] nums, int k)
        {
            var pairs = 0;
            var values = new Hashtable();
            // set the map
            for (var i = 0; i < nums.Length; i++)
            {
                if (!values.ContainsKey(nums[i]))
                {
                    values.Add(nums[i], 1);
                }
                else
                {
                    var val = (int) values[nums[i]];
                    values[nums[i]] = val + 1;
                }
            }

            if (k == 0)
            {
                // find pairs
                foreach (var value in values.Keys)
                {
                    var target = (int) value - k;
                    if (values.ContainsKey(target) && (int) values[target] > 1)
                    {
                        pairs++;
                    }
                }
            }
            else
            {
                // find pairs
                foreach (var value in values.Keys)
                {
                    var target = (int) value - k;
                    if (values.ContainsKey(target))
                    {
                        pairs++;
                    }
                }
            }

            return pairs;
        }
    }

    public class ReachingPoints
    {
        /*
         * Every parent point (x, y) has two children, (x, x+y) and (x+y, y).
         * However, every point (x, y) only has one parent candidate (x-y, y) if x >= y, else (x, y-x).
         * This is because we never have points with negative coordinates.
         *
         * we work backwards to find the answer, trying to transform the target point
         * to the starting point via applying the parent operation (x, y) -> (x-y, y) or (x, y-x)
         */
        // Time Complexity: O(max(tx,ty). If say ty = 1, we could be subtracting tx times.
        public bool AreReachingPoints(int sx, int sy, int tx, int ty)
        {
            while (tx >= sx && ty >= sy)
            {
                if (sx == tx && sy == ty)
                {
                    return true;
                }

                if (tx > ty)
                {
                    tx -= ty;
                }
                else
                {
                    ty -= tx;
                }
            }

            return false;
        }

        public bool AreReachingPoints1(int sx, int sy, int tx, int ty)
        {
            while (tx >= sx && ty >= sy)
            {
                if (tx == ty)
                {
                    break;
                }

                if (tx > ty)
                {
                    if (ty > sy)
                    {
                        tx %= ty;
                    }
                    else
                    {
                        return (tx - sx) % ty == 0;
                    }
                }
                else
                {
                    if (tx > sx)
                    {
                        ty %= tx;
                    }
                    else
                    {
                        return (ty - sy) % tx == 0;
                    }
                }
            }

            return (tx == sx && ty == sy);
        }
    }

    public class DegreeOfAnArray
    {
        public int FindShortestSubArray(int[] nums)
        {
            var degree = 0;
            var map = new Hashtable();
            var map1 = new Hashtable();
            var min = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                if (!map1.ContainsKey(nums[i]))
                {
                    map1.Add(nums[i], i);
                }

                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], 1);
                }
                else
                {
                    map[nums[i]] = (int) map[nums[i]] + 1;
                }

                if ((int) map[nums[i]] > degree)
                {
                    degree = (int) map[nums[i]];
                    min = i - ((int) map1[nums[i]]) + 1;
                }

                else if ((int) map[nums[i]] == degree) // 122343   2:2
                {
                    min = Math.Min(min, i - (int) map1[nums[i]] + 1);
                }
            }

            return min;
        }
    }

    public class MaximumNumberOfOcurrencesOfASubstring
    {
        public int MaxFreq(string s, int maxLetters, int minSize, int maxSize)
        {
            if (s.Length > Math.Pow(10, 5))
            {
                return 0;
            }

            if (maxLetters > 26 || maxLetters < 0)
            {
                return 0;
            }

            var max = 0;
            var map = new Hashtable();
            for (var i = 0; i < s.Length; i++)
            {
                for (var j = i + minSize - 1; j < Math.Min(i + maxSize, s.Length); j++)
                {
                    var subs = s[i..(j + 1)];
                    if (GetNumberOfUniqueChars(subs) <= maxLetters)
                    {
                        if (map.ContainsKey((subs)))
                        {
                            map[subs] = (int) map[subs] + 1;
                        }
                        else
                        {
                            map.Add(subs, 1);
                        }

                        if ((int) map[subs] > max)
                        {
                            max = (int) map[subs];
                        }
                    }
                }
            }

            return max;
        }

        public int MaxFreq1(string s, int maxLetters, int minSize, int maxSize)
        {
            if (s.Length > Math.Pow(10, 5))
            {
                return 0;
            }

            if (maxLetters > 26 || maxLetters < 0)
            {
                return 0;
            }

            var max = 0;
            var map = new Hashtable();
            for (var i = 0; i < s.Length - minSize + 1; i++)
            {
                var subs = s[i..(i + minSize)];
                if (GetNumberOfUniqueChars(subs) <= maxLetters)
                {
                    if (map.ContainsKey((subs)))
                    {
                        map[subs] = (int) map[subs] + 1;
                    }
                    else
                    {
                        map.Add(subs, 1);
                    }

                    if ((int) map[subs] > max)
                    {
                        max = (int) map[subs];
                    }
                }
            }

            return max;
        }

        private int GetNumberOfUniqueChars(string value)
        {
            var chars = new HashSet<char>();
            for (var i = 0; i < value.Length; i++)
            {
                if (!chars.Contains(value[i]))
                {
                    chars.Add(value[i]);
                }
            }

            return chars.Count;
        }
    }

    public class AccountsMerge
    {
        private List<UserAccount> userAccounts;
        private Hashtable emailMap;

        public AccountsMerge()
        {
            userAccounts = new List<UserAccount>();
            emailMap = new Hashtable();
        }

        public IList<IList<string>> AccountsMerge1(IList<IList<string>> accounts)
        {
            foreach (var account in accounts)
            {
                var name = account.First();
                var emails = account.Select(e => e).Skip(1);

                // create new user in map
                var newUser = new UserAccount {name = name, emails = new List<string>()};
                userAccounts.Add(newUser);
                foreach (var email in emails)
                {
                    if (emailMap.ContainsKey(email))
                    {
                        // merge account
                        MergeAccounts((UserAccount) emailMap[email], newUser);
                    }
                    else
                    {
                        newUser.emails.Add(email);
                        emailMap.Add(email, newUser);
                    }
                }
            }

            IList<IList<string>> result = new List<IList<string>>();
            foreach (var user in userAccounts)
            {
                var userList = new List<string>();
                userList.Add(user.name);
                user.emails.Sort();
                userList.AddRange(user.emails);
                result.Add(userList);
            }

            return result;
        }

        private void MergeAccounts(UserAccount fromUser, UserAccount toUser)
        {
            if (fromUser.Equals(toUser))
            {
                return;
            }

            // remove from set, to not process anymore
            userAccounts.Remove(fromUser);

            // merge, update emails liked to toUser
            foreach (var accountEmail in fromUser.emails)
            {
                if (emailMap.ContainsKey(accountEmail))
                {
                    emailMap[accountEmail] = toUser;
                }
                else
                {
                    emailMap.Add(accountEmail, toUser);
                }

                // add modified user to map
                toUser.emails.Add(accountEmail);
            }
        }

        public class UserAccount
        {
            public string name { get; set; }
            public List<string> emails { get; set; }

            public bool Equal(UserAccount a, UserAccount b)
            {
                if (a.name != b.name)
                {
                    return false;
                }

                if (a.emails.Count != b.emails.Count)
                {
                    return false;
                }

                for (var i = 0; i < a.emails.Count; i++)
                {
                    if (a.emails[i] != b.emails[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

    public class SingleElementSortedArray
    {
        public int SingleNonDuplicate(int[] nums)
        {
            var left = 0;
            var right = nums.Length - 1;

            while (left < right)
            {
                var mid = right - (right - left) / 2;
                var evenRightSide = (right - mid) % 2;

                if (nums[mid] == nums[mid - 1])
                {
                    if (evenRightSide == 0)
                    {
                        right = mid - 2;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                else if (nums[mid] == nums[mid + 1])
                {
                    if (evenRightSide == 0)
                    {
                        left = mid + 2;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                else
                {
                    return nums[mid];
                }
            }

            return nums[left];
        }
    }

    public class ReconstructItinerary
    {
        public IList<string> FindItinerary(IList<IList<string>> tickets)
        {
            var destinations = new Hashtable(); // dest:list<dest-connections>
            var route = new List<string>();

            for (var i = 0; i < tickets.Count; i++)
            {
                if (!destinations.ContainsKey(tickets[i][0]))
                {
                    destinations.Add(tickets[i][0], new List<string>() {tickets[i][1]});
                }
                else
                {
                    var connections = (List<string>) destinations[tickets[i][0]];
                    connections.Add(tickets[i][1]);
                    connections.Sort();
                    destinations[tickets[i][0]] = connections;
                }
            }

            var departure = "JFK";
            var arrivals = (List<string>) destinations[departure];
            route.Add(departure);

            while (destinations.Count != 0)
            {
                var nextDept = arrivals[0];

                route.Add(arrivals[0]);
                arrivals.Remove(arrivals[0]);

                if (arrivals.Count == 0)
                {
                    destinations.Remove(departure);
                }
                else
                {
                    destinations[departure] = arrivals;
                }

                departure = nextDept;
                arrivals = (List<string>) destinations[departure];
            }

            return route;
        }
        
        public IList<string> FindItinerary1(IList<IList<string>> tickets)
        {
            var destinations = new Hashtable(); // dest:list<dest-connections>
            var route = new List<string>();

            for (var i = 0; i < tickets.Count; i++)
            {
                if (!destinations.ContainsKey(tickets[i][0]))
                {
                    destinations.Add(tickets[i][0], new List<string>() {tickets[i][1]});
                }
                else
                {
                    var connections = (List<string>) destinations[tickets[i][0]];
                    connections.Add(tickets[i][1]);
                    connections.Sort();
                    destinations[tickets[i][0]] = connections;
                }
            }

            var airports= new Stack<string>();
            airports.Push("JFK");
            
            while (airports.Count != 0)
            {
                var dept = airports.Peek();
                if (((List<string>) destinations[dept]).Count == 0)
                {
                    route.Add(dept);
                    airports.Pop();
                }
                else
                {
                    var dests = (List<string>) destinations[dept];
                    var next = dests[0];
                    airports.Push(next);
                    dests.Remove(next);
                }
            }

            return route;
        }
    }
}