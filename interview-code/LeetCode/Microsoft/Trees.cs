using System;
using System.Collections.Generic;
using System.Linq;

namespace interview_code.LeetCode.MicrosoftTrees
{
    public class Trees
    {
        public bool IsValidBST(TreeNode root)
        {
            return IsValidRange(root, null, null);
        }

        private bool IsValidRange(TreeNode node, int? max, int? min)
        {
            if (node == null)
            {
                return true;
            }

            if (max.HasValue && node.val >= max)
                return false;

            if (min.HasValue && node.val <= min)
                return false;

            return IsValidRange(node.left, node.val, min) && IsValidRange(node.right, max, node.val);
        }

        public bool IsValidBstInteractive(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            int? min = null;
            int? max = null;
            var nodes = new Stack<TreeNode>();
            var maxs = new Stack<int?>();
            var mins = new Stack<int?>();

            nodes.Push(root);
            maxs.Push(max);
            mins.Push(min);

            while (nodes.Count != 0)
            {
                var node = nodes.Pop();
                min = mins.Pop();
                max = maxs.Pop();

                if (node == null)
                    continue;

                if (min.HasValue && node.val <= min)
                {
                    Console.WriteLine("n: " + node.val + ", min: " + min);
                    return false;
                }

                if (max.HasValue && node.val >= max)
                {
                    Console.WriteLine("n: " + node.val + ", max: " + max);
                    return false;
                }

                nodes.Push(node.right);
                maxs.Push(max);
                mins.Push(node.val);

                nodes.Push(node.left);
                maxs.Push(node.val);
                mins.Push(min);
            }

            return true;
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            var nodes = new List<int>();
            /*InOrderRecursive(root, nodes);
            return nodes;*/

            var nodesS = new Stack<TreeNode>();
            var current = root;

            while (current != null || nodesS.Count != 0)
            {
                while (current != null)
                {
                    nodesS.Push(current);
                    current = current.left;
                }

                current = nodesS.Pop();
                nodes.Add(current.val);
                current = current.right;
            }

            return nodes;
        }

        public void InOrderRecursive(TreeNode root, IList<int> nodes)
        {
            if (root == null)
            {
                return;
            }

            InOrderRecursive(root.left, nodes);
            nodes.Add(root.val);
            InOrderRecursive(root.right, nodes);
        }

        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> result = new List<IList<int>>();

            if (root == null)
            {
                return result;
            }

            var nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);

            while (nodes.Count != 0)
            {
                var size = nodes.Count;
                var list = new List<int>();
                for (var i = 0; i < size; i++)
                {
                    var node = nodes.Dequeue();
                    if (node.left != null)
                    {
                        nodes.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        nodes.Enqueue(node.right);
                    }

                    list.Add(node.val);
                }

                result.Add(list);
            }

            return result;
        }

        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> results = new List<IList<int>>();

            if (root == null)
            {
                return results;
            }

            var node_queue = new Queue<TreeNode>();
            node_queue.Enqueue(root);
            node_queue.Enqueue(new TreeNode(-198891));

            var level_list = new LinkedList<int>();
            var is_order_left = true;

            while (node_queue.Count > 0)
            {
                var curr_node = node_queue.Dequeue();

                if (curr_node.val != -198891)
                {
                    if (is_order_left)
                    {
                        level_list.AddLast(curr_node.val);
                    }
                    else
                    {
                        level_list.AddFirst(curr_node.val);
                    }

                    if (curr_node.left != null)
                    {
                        node_queue.Enqueue(curr_node.left);
                    }

                    if (curr_node.right != null)
                    {
                        node_queue.Enqueue(curr_node.right);
                    }
                }
                else
                {
                    // we finish the scan of one level
                    results.Add(level_list.ToList());
                    level_list = new LinkedList<int>();
                    // prepare for the next level
                    if (node_queue.Count > 0)
                    {
                        node_queue.Enqueue(new TreeNode(-198891));
                    }

                    is_order_left = !is_order_left;
                }
            }

            return results;
        }

        public Node1 Connect(Node1 root)
        {
            if (root == null)
            {
                return root;
            }

            var nodes = new Queue<Node1>();
            nodes.Enqueue(root);

            var levelNodes = new List<Node1>();

            while (nodes.Count != 0)
            {
                var size = nodes.Count;
                levelNodes = new List<Node1>();

                for (var i = 0; i < size; i++)
                {
                    var node = nodes.Dequeue();

                    if (node.left != null)
                    {
                        nodes.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        nodes.Enqueue(node.right);
                    }

                    levelNodes.Add(node);

                    if (levelNodes.Count >= 1 && i >= 1)
                    {
                        levelNodes[i - 1].next = levelNodes[i];
                    }
                }
            }

            return root;
        }

        public TreeNode LowestCommonAncestorBST(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root.val > Math.Max(p.val, q.val))
            {
                return LowestCommonAncestorBST(root.left, p, q);
            }
            else if (root.val < Math.Min(p.val, q.val))
            {
                return LowestCommonAncestorBST(root.right, p, q);
            }
            else
            {
                return root;
            }
        }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
            {
                return null;
            }

            if (root.val == p.val || root.val == q.val)
            {
                return root;
            }

            var left = LowestCommonAncestor(root.left, p, q);
            var right = LowestCommonAncestor(root.right, p, q);

            if (left != null && right != null)
            {
                return root;
            }

            if (left == null && right == null)
            {
                return null;
            }

            return left ?? right;
        }

        private int pIndex = 0;

        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            pIndex = 0;
            return Build(preorder, inorder, 0, inorder.Length - 1, null);
        }

        private TreeNode Build(
            int[] preorder,
            int[] inorder,
            int start,
            int end,
            TreeNode root)
        {
            if (start > end || preorder.Length == 0 || inorder.Length == 0)
            {
                return null;
            }

            var pNodeVal = preorder[pIndex++];
            var tNode = new TreeNode(pNodeVal);

            if (start == end)
            {
                return tNode;
            }

            var iIndex = Find(inorder, start, end, pNodeVal);

            tNode.left = Build(preorder, inorder, start, iIndex - 1, tNode);
            tNode.right = Build(preorder, inorder, iIndex + 1, end, tNode);

            return tNode;
        }

        private int Find(int[] inorder, int start, int end, int value)
        {
            int i;
            for (i = start; i <= end; i++)
            {
                if (inorder[i] == value)
                {
                    return i;
                }
            }

            return i;
        }

        public int NumIslands(char[][] grid)
        {
            var visited = new bool[grid.Length][];

            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid[y].Length; x++)
                {
                    visited[y] = new bool[grid[y].Length];
                }
            }

            var queue = new Queue<int[]>();
            var islands = 0;

            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if (visited[row][col])
                        continue;

                    queue.Enqueue(new int[] {row, col});
                    int islandLength = 0;

                    while (queue.Count != 0)
                    {
                        var size = queue.Count;

                        for (var i = 0; i < size; i++)
                        {
                            var c = queue.Dequeue();
                            var row1 = c[0];
                            var col1 = c[1];

                            if (visited[row1][col1])
                            {
                                continue;
                            }

                            visited[row1][col1] = true;

                            if (grid[row1][col1] == '0')
                            {
                                continue;
                            }

                            islandLength++;

                            if (row1 >= 1 && !visited[row1 - 1][col1] && grid[row1 - 1][col1] == '1')
                            {
                                queue.Enqueue(new int[] {row1 - 1, col1});
                                Console.WriteLine("row: " + row1 + " col: " + col1 + " up: " + (row1 - 1) + "," + col1);
                            }

                            if (row1 < grid.Length - 1 && !visited[row1 + 1][col1] && grid[row1 + 1][col1] == '1')
                            {
                                queue.Enqueue(new int[] {row1 + 1, col1});
                                Console.WriteLine("row: " + (row1) + " col: " + col1 + " down: " + (row1 + 1) + "," +
                                                  col1);
                            }

                            if (col1 >= 1 && !visited[row1][col1 - 1] && grid[row1][col1 - 1] == '1')
                            {
                                queue.Enqueue(new int[] {row1, col1 - 1});
                                Console.WriteLine(
                                    "row: " + row1 + " col: " + col1 + " left: " + row1 + "," + (col1 - 1));
                            }

                            if (col1 < grid[row1].Length - 1 && !visited[row1][col1 + 1] && grid[row1][col1 + 1] == '1')
                            {
                                queue.Enqueue(new int[] {row1, col1 + 1});
                                Console.WriteLine("row: " + row1 + " col: " + col1 + " right: " + row1 + "," +
                                                  (col1 + 1));
                            }
                        }
                    }

                    if (islandLength > 0)
                    {
                        Console.WriteLine("island length: " + islandLength + " from: " + row + "," + col);
                        islands++;
                    }
                }
            }

            return islands;
        }
    }

    public class Node1
    {
        public int val;
        public Node1 left;
        public Node1 right;
        public Node1 next;

        public Node1()
        {
        }

        public Node1(int _val)
        {
            val = _val;
        }

        public Node1(int _val, Node1 _left, Node1 _right, Node1 _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }
}