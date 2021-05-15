using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    public class BinaryTree
    {
        int max = 0;

        /*
         * The tilt of a tree node is defined as the absolute difference between the sum of all left subtree node values
         * and the sum of all right subtree node values.
         */
        public int FindTilt(TreeNode root)
        {
            Console.WriteLine();
            if (root == null)
            {
                return 0;
            }
            else
            {
                var tilt = 0;
                FindTiltNode(root, tilt);
                return tilt;
            }
        }

        /*
         * Evaluation at node level and keep track of target at tree level
         */
        private int FindTiltNode(TreeNode root, int tilt)
        {
            var leftSum = 0;
            var rightSum = 0;

            if (root == null)
            {
                return 0;
            }
            else
            {
                leftSum = FindTiltNode(root.left, tilt);
                rightSum = FindTiltNode(root.right, tilt);
            }

            // Add current tilt to overall 
            tilt += Math.Abs(leftSum - rightSum);

            // Returns sum of nodes under current tree, level
            return leftSum + rightSum + root.val;
        }

        /*
         * Sum all nodes values 
         */
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

        /*
         * A binary tree is univalued if every node in the tree has the same value.
         */
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
         * DiameterOfBinaryTree
         * Evaluation at node level and keep track of target at tree level
         */
        public int FindLongestPathBeetwenTwoNodes(TreeNode node, int maxPath)
        {
            if (node == null)
            {
                return 0;
            }

            // path to last left leaf
            var leftHeight = FindLongestPathBeetwenTwoNodes(node.left, maxPath);
            // path to last right leaf
            var rightHeight = FindLongestPathBeetwenTwoNodes(node.right, maxPath);

            maxPath = Math.Max(maxPath, leftHeight + rightHeight);
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        public int HeightOfTree(TreeNode root)
        {
            if (root == null)
                return 0;

            /* compute the depth of each subtree */
            int lDepth = HeightOfTree(root.left);
            int rDepth = HeightOfTree(root.right);

            /* use the larger one */
            return Math.Max(lDepth, rDepth) + 1;
        }

        /*
         * Bottom to top approach
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
         * Depth is number of nodes in the path
         */
        public int MinimunmDepth(TreeNode root)
        {
            // Null node has 0 depth.
            if (root == null)
            {
                return 0;
            }
            // Leaf node reached.
            if (root.left == null && root.right == null)
            {
                return 1;
            }
            // Current node has only right subtree.
            if (root.left == null)
            {
                return MinimunmDepth(root.right) + 1;
            }
            // Current node has only left subtree.
            if (root.right == null)
            {
                return MinimunmDepth(root.left) + 1;
            }
            // if none of the above cases, then recur on both left and right subtrees.
            return Math.Min(MinimunmDepth(root.left), MinimunmDepth(root.right)) + 1;
        }

        /*
         * Depth is number of nodes in the path
         */
        public int MaximumDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            /* compute the depth of each subtree */
            int lDepth = MaximumDepth(root.left);
            int rDepth = MaximumDepth(root.right);

            /* use the larger one */
            if (lDepth > rDepth)
                return (lDepth + 1);
            return (rDepth + 1);
        }

        /*
         * Traverse using a second helper method
         */
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

        /*
         * FindSecondMinimumValue helper
         */
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

        /*
         * print leaf values => node.left, and node.right == null
         */
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
         * Print tree level by level => breadth first traversal, use queue FIFO
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

            while (nodes.Count != 0)
            {
                var size = nodes.Count;
                for (var i = 0; i < size; i++)
                {
                    var node = nodes.Dequeue();
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
            }
        }

        /*
         * Normal traversal
         */
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
                nodes = new List<TreeNode> {root};
                foundPath = FindNodesPathPerSumFromRootToLeaf(root.right, subsum, ref nodes);
            }

            return foundPath;
        }

        /*
         * Normal traversal
         */
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

        /*
         * Breadth first traversal, use queue FIFO
         */
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

        /*
         * Breadth first traversal, use queue FIFO
         */
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

        /*
         * root.left = SerializeToTree(nodes, root.left, 2 * index + 1);
         * root.right = SerializeToTree(nodes, root.right, 2 * index + 2);
         */
        public TreeNode SerializeToTree(List<int> nodes, TreeNode root, int index)
        {
            // Base case for recursion 
            if (index < nodes.Count)
            {
                //TreeNode temp = new TreeNode { val = nodes[index] };
                //root = temp;
                root = new TreeNode {val = nodes[index]};

                // insert left child 
                root.left = SerializeToTree(nodes, root.left, 2 * index + 1);

                // insert right child 
                root.right = SerializeToTree(nodes, root.right, 2 * index + 2);
            }

            return root;
        }

        public void InOrderTraversalInteractive(TreeNode root)
        {
            /*
             * right, root, left => stack LIFO(in-order) root is the order node
             * right, left, root => stack LIFO(pre-order)
             * root, right, left => stack LIFO(post-order)
             */

            var nodes = new Stack<TreeNode>();
            nodes.Push(root);
            var round = 0;

            while (nodes.Count != 0)
            {
                var node = nodes.Pop();
                //Console.WriteLine($"{round} -- pop-node= {node.val}");
                //if (node.visited)
                //{
                    //Console.WriteLine("l-" + node.val + ", ");
                //}
                //else
                //{
                    if (node.right != null)
                    {
                        nodes.Push(node.right);
                    }

                    //node.visited = true;
                    //Console.WriteLine($"{round} -- push/visited-node= {node.val}");
                    nodes.Push(node);
                    if (node.left != null)
                    {
                        nodes.Push(node.left);
                    }
                //}

                //round++;
            }
        }

        /*
         * Tree is Symmetric
         */
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            return IsMirror(root.left, root.right);
        }

        private bool IsMirror(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
            {
                return true;
            }

            if (root1 != null && root2 != null && root1.val == root2.val)
            {
                return IsMirror(root1.left, root2.right) && IsMirror(root1.right, root2.left);
            }

            return false;
        }

        /*
         * Sorted array to BST
         */
        public TreeNode SortedArrayToBst(int[] nums)
        {
            var root = BuildTree(nums, 0, nums.Length - 1);

            return root;
        }

        private TreeNode BuildTree(int[] nums, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            var mid = (start + end) / 2;
            var root = new TreeNode(nums[mid]);

            root.left = BuildTree(nums, start, mid - 1);
            root.right = BuildTree(nums, mid + 1, end);

            return root;
        }

        public int MaxPathSum(TreeNode root)
        {
            int maxSum = int.MinValue;
            TrackSum(root, ref maxSum);
            return maxSum;
        }

        private int TrackSum(TreeNode root, ref int maxSum)
        {
            if (root == null)
            {
                return 0;
            }

            var lsum = 0;
            if (root.left != null)
            {
                lsum = Math.Max(0, TrackSum(root.left, ref maxSum));
            }

            var rsum = 0;
            if (root.right != null)
            {
                rsum = Math.Max(0, TrackSum(root.right, ref maxSum));
            }

            maxSum = Math.Max(maxSum, lsum + rsum + root.val);
            Console.WriteLine(maxSum);
            return Math.Max(lsum, rsum) + root.val;
        }

        public int KthSmallest(TreeNode root, int k)
        {
            Stack<int> minElements = new Stack<int>();
            TraverBst(root, ref minElements, k);
            return minElements.Pop();
        }

        private static int TraverBst(TreeNode root, ref Stack<int> minElements, int kth)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left != null)
            {
                TraverBst(root.left, ref minElements, kth);
            }

            if (minElements.Count != kth)
            {
                minElements.Push(root.val);
            }

            TraverBst(root.right, ref minElements, kth);
            return root.val;
        }
    }

    public class TreeNode
    {
        public TreeNode()
        {
        }

        public TreeNode(int value)
        {
            this.val = value;
        }

        public int val { set; get; }
        public bool visited { get; set; }
        public TreeNode left { set; get; }
        public TreeNode right { set; get; }
    }
}