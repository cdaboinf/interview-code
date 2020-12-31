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

        public static bool DfsVisit(GraphNode node, int target)
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

        public bool BfsVisit(GraphNode start, int target)
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
