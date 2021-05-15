using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    public class GraphsProblems
    {
        public GraphsProblems() { }

        public bool DFS(Graph graph, int target)
        {
            foreach (var node in graph.Nodes)
            {
                if (node.State == State.Unvisited && DfsVisit(node, target))
                    return true;
            }
            return false;
        }

        private bool DfsVisit(GraphNode node, int target)
        {
            node.State = State.Visiting;

            if (node.Val == target)
            {
                return true;
            }

            foreach (var neighbor in node.Neightbors)
            {
                if (neighbor.State == State.Unvisited && DfsVisit(neighbor, target))
                    return true;
            }

            node.State = State.Visited;

            return false;
        }

        private bool BfsVisit(GraphNode start, int target)
        {
            Queue<GraphNode> q = new Queue<GraphNode>();
            q.Enqueue(start);

            start.State = State.Visiting;
            while (q.Count != 0)
            {
                GraphNode current = q.Dequeue();
                if (current.Val == target)
                {
                    return true;
                }
                foreach (var neighbor in current.Neightbors)
                {
                    if (neighbor.State == State.Unvisited)
                    {
                        q.Enqueue(neighbor);
                        neighbor.State = State.Visiting;
                    }
                }
                current.State = State.Visited;
            }
            return false;
        }

        /// <summary>
        /// array with jump values, find cycle
        /// the values can wrap when crossing the boundary
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool SingleCycleCheck(int []array)
        {
            var n = array.Length;
            var jumps = 0;
		
            var visited = new int[n];
            for(var i=0; i<n; i++){
                visited[i] = 0;
            }
            visited[0] = 1;
            var index = 0;
		
            while(jumps < array.Length){
                if(jumps > 0 && index == 0){
                    return false;
                }
                // movement
                var move = array[index];
                var target = (move + index) % array.Length;
                if(target < 0){
                    target = target + array.Length;
                }
                index = target;
		 	
                if(visited[index] > 1){
                    return false;
                }
                else{
                    visited[index] +=1;
                }
                jumps++;
            }
            return visited[0] == 2;
        }
    }

    public class Graph
    {
        public List<GraphNode> Nodes { get; set; }
    }

    public class GraphNode
    {
        public int Val { get; set; }
        public State State { get; set; }
        public List<GraphNode> Neightbors { get; set; }

        public GraphNode(int value)
        {
            Val = value;
        }
    }

    public enum State
    {
        Visiting,
        Visited,
        Unvisited
    }
}
