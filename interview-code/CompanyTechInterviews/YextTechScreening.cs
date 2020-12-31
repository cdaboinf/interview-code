namespace interview_code
{
    public class YextTechScreening
    {
        public void SortTreeDescending(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            
            SortTreeDescending(node.left);
            SortTreeDescending(node.right);

            if ((node.left == null || node.left.val <= node.val)
                && (node.right == null || node.right.val <= node.val))
            {
                return;
            }

            var swap = node.left;

            if (node.left == null)
            {
                swap = node.right;
            }
            else if (node.right != null &&  node.right.val >= node.left.val)
            {
                swap = node.right;
            }

            var temp = swap.val;
            swap.val = node.val;
            node.val = temp;
        }
    }
}