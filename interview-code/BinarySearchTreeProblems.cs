using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    public class BinarySearchTree
    {
        // Is BST: Given a Binary Tree, determine if it is a Binary Search Tree (BST)
        public bool IsBst(TreeNode node, int min, int max)
        {
            // Base case. An empty tree is a BST.
            if (node == null)
                return true;
            // Checking if a key is outside the permitted range.
            if (node.val < min)
                return false;
            if (node.val > max)
                return false;
            // Sending in updates ranges to the right and left subtree
            return IsBst(node.right, node.val, max) &&
                   IsBst(node.left, min, node.val);
        }

        public void AddNode(int value, TreeNode root)
        {
            TreeNode parent = null;
            TreeNode current = root;
            while (current != null)
            {
                parent = current;
                // In this case, if there is a duplicate node, it will end up
                // on the left side. You can discuss with the interviewer.
                current = current.val < value ? current.right : current.left;
            }

            if (parent == null)
            {
                root = new TreeNode(value);
            }
            else if (parent.val < value)
            {
                parent.right = new TreeNode(value);
            }
            else
            {
                parent.left = new TreeNode(value);
            }
        }

        public TreeNode Search(int target, TreeNode root)
        {
            TreeNode current = root;
            while (current != null)
            {
                if (current.val < target)
                {
                    current = current.right;
                }
                else if (current.val > target)
                {
                    current = current.left;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }
    }
}