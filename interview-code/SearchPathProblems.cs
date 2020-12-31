using System;
using System.Collections.Generic;

namespace interview_code
{
    public class SearchProblems
    {
        /*
         * Greedy Algorithm
         * best local solution/decision
         */

        /*
         * Dijkstra's Algorithm
         * 
         * 1 Mark your selected initial node with a current distance of 0 and the rest with infinity.
         * 2 Set the non-visited node with the smallest current distance as the current node C.
         * 3 For each neighbour N of your current node C: add the current distance
         *   of C with the weight of the edge connecting C-N. If it's smaller than the current distance of N,
         *   set it as the new current distance of N.
         * 4 Mark the current node C as visited.
         * 5 If there are non-visited nodes, go to step 2.
         */
        void Dijkstra(int[,] graph, int source, int verticesCount)
        {
            int[] dist = new int[verticesCount]; // The output array. dist[i] 
            // will hold the shortest 
            // distance from src to i 
  
            // sptSet[i] will true if nodes 
            // i is included in shortest path 
            // tree or shortest distance from 
            // src to i is finalized 
            bool[] sptSet = new bool[verticesCount]; 
  
            // Initialize all distances as 
            // INFINITE and stpSet[] as false 
            for (int i = 0; i < verticesCount; i++) { 
                dist[i] = int.MaxValue; 
                sptSet[i] = false; 
            } 
  
            // Distance of source nodes 
            // from itself is always 0 
            dist[source] = 0; 
  
            // Find shortest path for all vertices 
            for (int count = 0; count < verticesCount - 1; count++) { 
                // Pick the minimum distance vertex 
                // from the set of vertices not yet 
                // processed. u is always equal to 
                // src in first iteration. 
                int u = MinDistance(dist, sptSet, verticesCount); 
  
                // Mark the picked nodes as processed 
                sptSet[u] = true; 
  
                // Update dist value of the adjacent 
                // vertices of the picked nodes. 
                for (int v = 0; v < verticesCount; v++) 
  
                    // Update dist[v] only if is not in 
                    // sptSet, there is an edge from u 
                    // to v, and total weight of path 
                    // from src to v through u is smaller 
                    // than current value of dist[v] 
                    if (!sptSet[v] && graph[u, v] != 0 && // graph[u, v] = 0 there is no route from u to v
                        dist[u] != int.MaxValue && // not processed node
                        dist[u] + graph[u, v] < dist[v]) // u ---> v, distance from u to v is compare with current node(V) distance
                    {
                        dist[v] = dist[u] + graph[u, v];
                    }
            } 
        }
        
        private int MinDistance(int[] dist, bool[] sptSet, int verticesCount)
        { 
            // Initialize min value 
            int min = int.MaxValue;
            int minIndex = -1; 
  
            for (int v = 0; v < verticesCount; v++) 
                if (sptSet[v] == false && dist[v] <= min) { 
                    min = dist[v]; 
                    minIndex = v; 
                } 
  
            return minIndex; 
        }
        private static bool ToPlaceOrNotToPlace(int[, ] board, int row, int col) {  
            int i, j;
            int N = board.GetUpperBound(0);
            
            for (i = 0; i < col; i++) {  
                if (board[row, i] == 1) 
                    return false;  
            }  
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--) {  
                if (board[i, j] == 1) 
                    return false;  
            }  
            for (i = row, j = col; j >= 0 && i < N; i++, j--) {  
                if (board[i, j] == 1) 
                    return false;  
            }  
            return true;  
        }  
        private bool TheBoardSolver(int[, ] board, int col) { 
            int N = board.GetUpperBound(0);
            
            if (col >= N) 
                return true;
            
            for (int i = 0; i < N; i++) {  
                if (ToPlaceOrNotToPlace(board, i, col)) {
                    board[i, col] = 1;  
                    if (TheBoardSolver(board, col + 1)) 
                        return true;  
                    // Backtracking is hella important in this one.  
                    board[i, col] = 0;  
                }  
            }  
            return false;  
        }  
    }
}