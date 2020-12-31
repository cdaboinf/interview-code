using System;
using System.Collections.Generic;

namespace interview_code
{
    public class HackerRankTest
    {
        public HackerRankTest()
        {
        }

        /*
            John works at a clothing store. He has a large pile of socks that he must pair by color for sale. 
            Given an array of integers representing the color of each sock, determine how many pairs of socks with matching colors there are.

            For example, there are  socks with colors . There is one pair of color  and one of color . 
            There are three odd socks left, one of each color. The number of pairs is .

            Function Description

            Complete the sockMerchant function in the editor below. 
            It must return an integer representing the number of matching pairs of socks that are available.

            sockMerchant has the following parameter(s):

            n: the number of socks in the pile
            ar: the colors of each sock
            Input Format

            The first line contains an integer , the number of socks represented in .
            The second line contains  space-separated integers describing the colors  of the socks in the pile.

            Constraints

             where 
            Output Format

            Return the total number of matching pairs of socks that John can sell.
         */
        public int SockMerchant(int n, int[] ar)
        {
            var counter = new HashSet<int>();
            var result = 0;
            for (var i = 0; i < ar.Length; i++)
            {
                if (!counter.Contains(ar[i]))
                {
                    counter.Add(ar[i]);
                }
                else
                {
                    counter.Remove(ar[i]);
                    result++;
                }
            }

            return result;
        }

        /*
         * Gary is an avid hiker. He tracks his hikes meticulously, paying close attention to small details like topography.
         * During his last hike he took exactly  steps. For every step he took, he noted if it was an uphill, , or a downhill,  step.
         * Gary's hikes start and end at sea level and each step up or down represents a  unit change in altitude. We define the following terms:

            A mountain is a sequence of consecutive steps above sea level, starting with a step up from sea level and ending with a step down to sea level.
            A valley is a sequence of consecutive steps below sea level, starting with a step down from sea level and ending with a step up to sea level.
            Given Gary's sequence of up and down steps during his last hike, find and print the number of valleys he walked through.

            For example, if Gary's path is , he first enters a valley  units deep. Then he climbs out an up onto a mountain  units high. Finally, he returns to sea level and ends his hike.

            Function Description

            Complete the countingValleys function in the editor below. It must return an integer that denotes the number of valleys Gary traversed.

            countingValleys has the following parameter(s):

            n: the number of steps Gary takes
            s: a string describing his path
            Input Format

            The first line contains an integer , the number of steps in Gary's hike.
            The second line contains a single string , of  characters that describe his path.
         */
        public int CountingValleys(int n, string s)
        {
            var seaLevel = 0;
            var valleys = 0;

            for (var i = 0; i < s.Length; i++)
            {
                // uphill
                if (s[i] == 'U')
                {
                    seaLevel += 1;
                }
                // downhill
                else if (s[i] == 'D')
                {
                    seaLevel -= 1;
                }

                // sea level from valley
                if (seaLevel == 0 && s[i] == 'U')
                {
                    valleys++;
                }
            }

            return valleys;
        }

        /*
         *  Emma is playing a new mobile game that starts with consecutively numbered clouds.
            Some of the clouds are thunderheads and others are cumulus. 
            She can jump on any cumulus cloud having a number that is equal to the number of the current cloud plus  or . 
            She must avoid the thunderheads. 
            Determine the minimum number of jumps it will take Emma to jump from her starting postion to the last cloud. It is always possible to win the game.

            For each game, Emma will get an array of clouds numbered  if they are safe or  if they must be avoided. 
            For example,  indexed from . 
            The number on each cloud is its index in the list so she must avoid the clouds at indexes  and . 
            She could follow the following two paths:  or . The first path takes  jumps while the second takes .

            Function Description:

                Complete the jumpingOnClouds function in the editor below. It should return the minimum number of jumps required, as an integer.

                jumpingOnClouds has the following parameter(s):

                c: an array of binary integers
                Input Format
         */
        public int JumpingOnClouds(int[] c)
        {
            var dJumps = 0;
            var sJumps = 0;

            for (var i = 0; i < c.Length; i++)
            {
                if (c[i] == 1)
                {
                    continue;
                }

                if (2 * dJumps + sJumps == c.Length - 1)
                {
                    return dJumps + sJumps;
                }

                var single = i < c.Length - 1 && c[i + 1] != 1;
                var multiple = i < c.Length - 2 && c[i + 2] != 1;

                if (multiple)
                {
                    Console.WriteLine($"d-index: {i}");
                    dJumps += 1;
                    i = i + 1;
                }
                else
                {
                    Console.WriteLine($"s-index: {i}");
                    sJumps += 1;
                }
            }

            return -1;
        }

        /*
         *  Lilah has a string, , of lowercase English letters that she repeated infinitely many times.

            Given an integer, , find and print the number of letter a's in the first  letters of Lilah's infinite string.

            For example, if the string  and , the substring we consider is , the first  characters of her infinite string. There are  occurrences of a in the substring.

            Function Description

            Complete the repeatedString function in the editor below. It should return an integer representing the number of occurrences of a in the prefix of length  in the infinitely repeating string.

            repeatedString has the following parameter(s):

            s: a string to repeat
            n: the number of characters to consider
            Input Format

            The first line contains a single string, .
            The second line contains an integer, .
         */
        public long RepeatedString(string s, long n)
        {
            //var sCounter = 0;
            long aCounter = 0;
            //var newString = new StringBuilder("");

            long remainder = n % s.Length; // 10 % 3 = 1
            long total = n - remainder; // 10 - 1 = 9

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a')
                {
                    aCounter++;
                }
            }

            Console.WriteLine($"aCounter: {aCounter}");
            aCounter = aCounter * total / s.Length; // for aba => 2 * (9 / 3)

            // extra space will fit some of the characters
            for (var i = 0; i < remainder; i++)
            {
                if (s[i] == 'a')
                {
                    aCounter++;
                }
            }

            /*
            for(var i=0; i<n; i++){
                if(sCounter == s.Length){
                    sCounter = 0;
                }
                newString.Append(s[sCounter]);
                if(newString.ToString()[i] == 'a'){
                    aCounter++;
                }
                sCounter++;
            }*/
            return aCounter;
        }
    }
}