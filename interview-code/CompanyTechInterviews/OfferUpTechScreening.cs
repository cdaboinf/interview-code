using System;
using System.Collections.Generic;

namespace interview_code.CompanyTechInterviews
{
    public class OfferUpTechScreening
    {
        /*
         * maze problem, find possible path from a to b, then come back from b to a
         * on the way you have determine if the path contains 1s and add to a counter when traversing both paths
         * -1 is an invalid path
         * return the total values of 1s collection from a to b plus b to a
         */
        public int CollectMax(int[,] mat)
        {
            var riders = 0;
            var visited = new List<int>();
            var sol = new int[3, 3]
            {
                {0, 0, 0},
                {0, 0, 0},
                {0, 0, 0},
            };
            /*
            if (!FindPathWithRidersToAirport(mat, 0, 0, sol) == false)
            {
                return 0;
            }*/
            //if (!FindPathWithRidersToStart(mat, 0, 0, riders) == false) {
            //   return 0; 
            //}

            var flag = FindPathWithRidersToAirport(mat, 0, 0, sol);
            
            for (var i = 0; i < 3; i++) {
                for (var j = 0; j < 3; j++) {
                    Console.Write(sol[i, j] + "\t");
                }
                Console.WriteLine();
            }

            return riders;
        }

        private static bool FindPathWithRidersToAirport(int[,] mat, int row, int col, int[,] sol)
        {
            // found airport
            if ((row == 3 - 1 && col == 3 - 1) && (mat[row, col] == 1))
            {
                sol[row, col] = 1;
                return true;
            }

            // Check if maze[x][y] is valid
            if ((row >= 0 && row < 3) && (col >= 0 && col < 3) && mat[row, col] == 1)
            {
                // mark x, y as part of solution path 
                sol[row, col] = 1;

                // Move right
                if (FindPathWithRidersToAirport(mat, row, col + 1, sol))
                {
                    return true;
                }

                // Move down
                if (FindPathWithRidersToAirport(mat, row + 1, col, sol))
                {
                    return true;
                }

                // backtrack
                sol[row, col] = 0;
                return false;
            }

            return false;
        }

        /*
         * Find if a substring x in substring s
         * then return the index of the first occurence
         */
        public int FirstOccurrence(string s, string x)
        {
            int N = s.Length;
            int M = x.Length;

            /* A loop to slide pat[] one by one */
            for (int i = 0; i <= N - M; i++)
            {
                int j;

                /* For current index i, check for 
                pattern match */
                for (j = 0; j < M; j++)
                    if (s[i + j] != x[j] && s[i + j] != '*')
                        break;

                if (j == M)
                    return i;
            }

            return -1;
        }
    }
}