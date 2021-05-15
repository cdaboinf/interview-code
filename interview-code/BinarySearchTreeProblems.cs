using System;
using System.Collections;
using System.Collections.Generic;

namespace interview_code
{
    public class BinarySearchTree
    {
        public TreeNode MinBst(List<int> array, TreeNode node, int start, int end) {
            if(end < start){
                return node;
            }
		
            var mid = start + (end - start) / 2;
            var newNode = new TreeNode(array[mid]);
		
            if(node == null){
                node = new TreeNode(newNode.val);
            }
            else{
                if (array[mid] < node.val)
                {
                    node.left = newNode;
                    node = node.left;
                }
                else
                {
                    node.right = newNode;
                    node = node.right;
                }
            }
		
            MinBst(array, node, start , mid-1);
            MinBst(array, node, mid+1, end);		
		
            return node;
        }
        
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

        /*
         * - root is null nothing to delete, return false
         * - node not found using search, then return false
         * 
         * - (node to remove only has a left child) node is a left child of its parent => parent.left = node.left
         * - (node to remove only has a left child) node is a right child of its parent => parent.right = node.left
         * 
         * - (node to remove only has a right child) node is a left child of its parent => parent.left = node.right
         * - (node to remove only has a right child) node is a right child of its parent => parent.right = node.right
         *
         * - node to remove has right/left children, need to two variables, parentDel, nodeDel
         *     + node: node to delete
         *     + parent: node to delete parent
         *     + nodeDel: node to replace the deleted node
         *     + parentDel: parent of nodeDel
         *
         *     nodeDel is the left most child of the node(delete) right subtree
         *     node.val = delNode.val
         *     parentDel.Left = null
         * 
         * - node to remove is root
         *     + one node in the tree
         *         root = null        
         *     + root has a left child only
         *         root = root.left
         *     + root has a right child only
         *         root = root.right
         */
        public void Delete(int target, TreeNode root)
        {
            if (root == null)
            {
                return;
            }
            
            TreeNode delParent = null;
            TreeNode delNode = null;

            if (root.val == target)
            {
                // only node in the bst
                if (root.left == null && root.right == null)
                {
                    root = null;
                }
                // set root to its left child
                else if (root.right == null)
                {
                    root = root.left;
                }
                // set root to its right child
                else if (root.right == null)
                {
                    root = root.right;
                }
                else
                {
                    delParent = root;
                    delNode = root.right;
                    while (delNode.left != null)
                    {
                        delParent = delNode;
                        delNode = delNode.left;
                    }

                    root.val = delNode.val;
                    if (delNode.right != null)
                    {
                        if (delParent.val > delNode.val)
                        {
                            delParent.left = delNode.right;
                        }
                        else
                        {
                            delParent.right = delNode.right;
                        }
                    }
                    else
                    {
                        if(delNode.val < delParent.val)
                        {
                            delParent.left = null;
                        }
                        else
                        {
                            delParent.right = null;
                        }
                    }
                }
            }

            else
            {
                TreeNode parent = null;
                TreeNode node = root;

                // find node to remove
                while (node != null && node.val != target)
                {
                    parent = node;
                    if (target < node.val)
                    {
                        node = node.left;
                    }
                    else if (target > node.val)
                    {
                        node = node.right;
                    }
                }
                
                // node not found
                if (node == null || node.val != target)
                {
                    return;
                }
                // remove node has no children
                else if (node.left == null && node.right == null)
                {
                    if (target < parent.val)
                    {
                        parent.left = null;
                    }
                    else
                    {
                        parent.right = null;
                    }

                    return;
                }
                // remove node has left child only
                else if (node.left != null && node.right == null)
                {
                    if (target < parent.val)
                    {
                        parent.left = node.left;
                    }
                    else
                    {
                        parent.right = node.left;
                    }

                    return;
                }
                // remove node has right child only
                else if (node.left == null && node.right != null)
                {
                    if (target < parent.val)
                    {
                        parent.left = node.right;
                    }
                    else
                    {
                        parent.right = node.right;
                    }

                    return;
                }
                else
                {
                    delParent = node;
                    delNode = node.right;
                    while (delNode.left != null)
                    {
                        delParent = delNode;
                        delNode = delNode.left;
                    }

                    node.val = delNode.val;
                    if (delNode.right != null)
                    {
                        if (delParent.val > delNode.val)
                        {
                            delParent.left = delNode.right;
                        }
                        else
                        {
                            delParent.right = delNode.right;
                        }
                    }
                    else
                    {
                        if(delNode.val < delParent.val)
                        {
                            delParent.left = null;
                        }
                        else
                        {
                            delParent.right = null;
                        }
                    }
                }
            }
        }
    }
}