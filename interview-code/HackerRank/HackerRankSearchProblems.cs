using System;
using System.Linq;

namespace interview_code.HackerRank
{
    public class HackerRankSearchProblems
    {
        /*
         * find unique elements from each array that form triplets where a[x] <= b[x] >= c[x]
         */
        public long Triplets(int[] a, int[] b, int[] c)
        {
            var triplets = 0;

            Array.Sort(a);
            Array.Sort(b);
            Array.Sort(c);

            a = a.Distinct().ToArray();
            b = b.Distinct().ToArray();
            c = c.Distinct().ToArray();

            for (var i = 0; i < b.Length; i++)
            {
                var j = 0;
                var k = 0;
                while (j < a.Length && a[j] <= b[i])
                {
                    //triplets++;
                    j++;
                }

                while (k < c.Length && c[k] <= b[i])
                {
                    //triplets++;
                    k++;
                }
                triplets += j * k;
            }

            return triplets;
        }
    }
}