using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    public class BinaryTree
    {
        int max = 0;
        //int maxHeight = 0;

        public int FindTilt(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                T t = new T();
                FindTiltNode(root, t);
                return t.tilt;
            }
        }

        /*
        Evaluation at node level and keep track of target at tree level
        */
        private int FindTiltNode(TreeNode root, T t)
        {
            var leftSum = 0;
            var rightSum = 0;

            if (root == null)
            {
                return 0;
            }
            else
            {
                leftSum = FindTiltNode(root.left, t);
                rightSum = FindTiltNode(root.right, t);
            }
            t.tilt += Math.Abs(leftSum - rightSum);
            return leftSum + rightSum + root.val;
        }

        public int FindSumNodes(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                return node.val + FindSumNodes(node.right) + FindSumNodes(node.left);
            }
        }

        private class T
        {
            public int tilt = 0;
        }

        public bool IsUnivaluedTree(TreeNode root, HashSet<int> set)
        {
            if (root == null)
            {
                return true;
            }

            var isLeftTreeUnivalued = IsUnivaluedTree(root.left, set);
            if (!isLeftTreeUnivalued)
            {
                return false;
            }
            var isRightTreeUnivalued = IsUnivaluedTree(root.right, set);
            if (!isRightTreeUnivalued)
            {
                return false;
            }

            if (set.Count == 0)
            {
                set.Add(root.val);
                return true;
            }
            else if (set.Contains(root.val))
            {
                return true;
            }
            return false;
        }

        /*
        DiameterOfBinaryTree

        Evaluation at node level and keep track of target at tree level
        */
        public int FindLongestPathBeetwenTwoNodes(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            var leftH = FindLongestPathBeetwenTwoNodes(root.left);
            var rightH = FindLongestPathBeetwenTwoNodes(root.right);

            max = Math.Max(max, Math.Abs(leftH - rightH));
            return Math.Max(leftH, rightH) + 1;
        }

        public void HeightOfTree(TreeNode root, int pHeight, ref int maxHeight)
        {
            if (root == null)
            {
                return;
            }
            var nodeHeight = pHeight + 1;
            if (nodeHeight > maxHeight)
            {
                maxHeight = nodeHeight;
            }
            HeightOfTree(root.left, nodeHeight, ref maxHeight);
            HeightOfTree(root.right, nodeHeight, ref maxHeight);
        }

        /*
            Bottom to top approach
         */
        public int IsBalanceTreeHeight(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            var isLeftBalance = IsBalanceTreeHeight(root.left);
            var isRightBalance = IsBalanceTreeHeight(root.right);

            if (isLeftBalance == -1 || isRightBalance == -1)
            {
                return -1;
            }

            if ((Math.Abs(isLeftBalance - isRightBalance)) > 1)
            {
                return -1;
            }

            return 1 + Math.Max(isLeftBalance, isRightBalance); // +1 root
        }

        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
            {
                return root;
            }

            var left = InvertTree(root.left);
            var right = InvertTree(root.right);

            root.left = right;
            root.right = left;

            return root;
        }

        /*
            Depth is number of nodes in the path
        */
        public int MinimunmDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 1;
            }

            // If left subtree is NULL, recur for right subtree  
            if (root.left == null)
            {
                return MinimunmDepth(root.right) + 1;
            }

            // If right subtree is NULL, recur for left subtree  
            if (root.right == null)
            {
                return MinimunmDepth(root.left) + 1;
            }

            return Math.Min(MinimunmDepth(root.left), MinimunmDepth(root.right)) + 1;
        }

        /*
            Depth is number of nodes in the path
        */
        public int MaximumDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.left == null && root.right == null)
            {
                return 1;
            }

            if (root.left == null)
            {
                return MaximumDepth(root.right) + 1;
            }

            if (root.right == null)
            {
                return MaximumDepth(root.left) + 1;
            }

            return Math.Max(MaximumDepth(root.right), MaximumDepth(root.left)) + 1;
        }

        public int FindSecondMinimumValue(TreeNode root)
        {
            // traverse tree and keep track of minimum value add values to array
            var minVal = Int32.MaxValue;
            var values = new List<int>();

            GetMinimumValue(root, ref minVal, ref values);

            // traverse array and find minimum difference against the min value
            var minSubstraction = Int32.MaxValue;
            var secondVal = Int32.MaxValue;
            foreach (var val in values)
            {
                if (minVal != val && Math.Abs(minVal - val) <= minSubstraction)
                {
                    minSubstraction = Math.Abs(minVal - val);
                    secondVal = val;
                }
            }
            if (!values.Contains(Int32.MaxValue) && secondVal == Int32.MaxValue)
            {
                return -1;
            }
            return secondVal;
        }

        private void GetMinimumValue(TreeNode root, ref int minVal, ref List<int> values)
        {
            if (root == null)
            {
                return;
            }

            var nodeVal = root.val;
            values.Add(nodeVal);
            if (nodeVal < minVal)
            {
                minVal = nodeVal;
            }

            GetMinimumValue(root.left, ref minVal, ref values);
            GetMinimumValue(root.right, ref minVal, ref values);
        }

        public void PrintLeafValue(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            if (root.left == null && root.right == null)
            {
                Console.WriteLine(root.val);
            }

            PrintLeafValue(root.left);
            PrintLeafValue(root.right);
        }

        /*
            Print tree level by level => breadth first traversal, use queue FIFO
        */
        public void PrintTreeNodeValuesByLevel(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            var nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);
            var level = 0;
            var lists = new List<List<int>>();
            while (nodes.Count != 0)
            {
                var list = new List<int>();
                var size = nodes.Count;
                for (var i = 0; i < size; i++)
                {
                    var node = nodes.Dequeue();
                    list.Add(node.val);
                    Console.Write($"{node.val}  ({level}),");
                    if (node.left != null)
                    {
                        nodes.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        nodes.Enqueue(node.right);
                    }
                }
                level++;
                lists.Add(list);
            }
        }

        public bool FindNodesPathPerSumFromRootToLeaf(TreeNode root, int targetSum, ref List<TreeNode> nodes)
        {
            if (root == null)
            {
                return targetSum == 0;
            }
            nodes.Add(root);
            int subsum = targetSum - root.val;
            if (subsum == 0 && root.left == null && root.right == null)
            {
                return true;
            }

            var foundPath = false;
            foundPath = FindNodesPathPerSumFromRootToLeaf(root.left, subsum, ref nodes);
            if (!foundPath)
            {
                nodes = new List<TreeNode>();
                nodes.Add(root);
                foundPath = FindNodesPathPerSumFromRootToLeaf(root.right, subsum, ref nodes);
            }
            return foundPath;
        }

        public bool FindNodesPathPerSumFromRootToNode(TreeNode root, int targetSum, ref List<TreeNode> nodes)
        {
            if (root == null)
            {
                return targetSum == 0;
            }
            nodes.Add(root);
            int subsum = targetSum - root.val;
            if (subsum == 0)
            {
                return true;
            }

            var foundPath = false;
            foundPath = FindNodesPathPerSumFromRootToNode(root.left, subsum, ref nodes);
            if (!foundPath)
            {
                nodes = new List<TreeNode>();
                nodes.Add(root);
                foundPath = FindNodesPathPerSumFromRootToNode(root.right, subsum, ref nodes);
            }
            return foundPath;
        }

        public void PrintLevelAverage(TreeNode root)
        {
            if (root == null)
            {
                return;
            }
            var nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);
            var level = 0;
            while (nodes.Count != 0)
            {
                var items = nodes.Count;
                var total = 0;
                for (int i = 0; i < items; i++)
                {
                    var node = nodes.Dequeue();
                    total += node.val;
                    if (node.left != null)
                    {
                        nodes.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        nodes.Enqueue(node.right);
                    }
                }
                Console.WriteLine($"level: {level} with an average of: {total / items}");
                level++;
            }
        }

        public void SerializeToList(TreeNode root, ref List<int> values)
        {
            if (root == null)
            {
                return;
            }

            var nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);
            while (nodes.Count != 0)
            {
                var size = nodes.Count;
                for (var i = 0; i < size; i++)
                {
                    var node = nodes.Dequeue();
                    values.Add(node.val);
                    if (node.left != null)
                    {
                        nodes.Enqueue(node.left);
                    }
                    if (node.right != null)
                    {
                        nodes.Enqueue(node.right);
                    }
                }
            }
        }

        public TreeNode SerializeToTree(List<int> nodes, TreeNode root, int index)
        {
            // Base case for recursion 
            if (index < nodes.Count)
            {
                TreeNode temp = new TreeNode { val = nodes[index] };
                root = temp;

                // insert left child 
                root.left = SerializeToTree(nodes, root.left, 2 * index + 1);

                // insert right child 
                root.right = SerializeToTree(nodes, root.right, 2 * index + 2);
            }
            return root;
        }
    }
    public class TreeNode
    {
        public int val { set; get; }
        public TreeNode left { set; get; }
        public TreeNode right { set; get; }
    }
}