using System.Collections.Generic;

namespace interview_code
{
    public class EbuilderTechScreening
    {
        /*
         * maze problem, find possible path from a to b, then come back from b to a
         * on the way you have determine if the path contains 1s and add to a counter when traversing both paths
         * -1 is an invalid path
         * return the total values of 1s collection from a to b plus b to a
         */
        public static int CollectMax(List<List<int>> mat)
        {
            var riders = 0;
            if (FindPathWithRidersToAirport(mat, 0, 0, riders) == false) {
                return 0; 
            }
            if (FindPathWithRidersToStart(mat, 0, 0, riders) == false) {
                return 0; 
            }

            return riders;
        }

        private static bool FindPathWithRidersToAirport(List<List<int>> mat, int x, int y, int riders)
        {
            // found airport
            if ((x == mat.Count - 1 && y == mat.Count - 1) && (mat[x][y] == 1 || mat[x][y] == 0))
            {
                if (mat[x][y] == 1)
                {
                    riders += 1;
                }
                return true;
            }

            // Check if maze[x][y] is valid
            if (x >= 0 && x < mat.Count && y >= 0 && y < mat.Count && mat[x][y] != -1)
            {
                if (mat[x][y] == 1)
                {
                    riders += 1;
                }
                // mark x, y as part of solution path 
                mat[x][y] = 0;

                // Move right
                if (FindPathWithRidersToAirport(mat, x + 1, y, riders))
                    return true;

                // Move down
                if (FindPathWithRidersToAirport(mat, x, y + 1, riders))
                    return true;
                
                // backtrack
                riders -= 1;
                mat[x][y] = 1;
                return false;
            }

            return false;
        }
        
        private static bool FindPathWithRidersToStart(List<List<int>> mat, int x, int y, int riders)
        {
            // found airport
            if ((x == 0 && y == 0) && (mat[x][y] == 1 || mat[x][y] == 0))
            {
                if (mat[x][y] == 1)
                {
                    riders += 1;
                }
                return true;
            }

            // Check if maze[x][y] is valid
            if (x >= 0 && x < mat.Count && y >= 0 && y < mat.Count && mat[x][y] != -1)
            {
                if (mat[x][y] == 1)
                {
                    riders += 1;
                }
                // mark x, y as part of solution path 
                mat[x][y] = 0;

                // Move right
                if (FindPathWithRidersToAirport(mat, x - 1, y, riders))
                    return true;

                // Move down
                if (FindPathWithRidersToAirport(mat, x, y - 1, riders))
                    return true;
                
                // backtrack
                riders -= 1;
                mat[x][y] = 1;
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
            for (int i = 0; i <= N - M; i++) { 
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