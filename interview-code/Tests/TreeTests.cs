using System;
using NUnit.Framework;

namespace interview_code
{
    [TestFixture]
    public class TreeTests
    {
        private TreeNode Root;

        [SetUp]
        public void SetUp()
        {
            Root = new TreeNode(1);
        }

        [Test]
        public void TestHeight()
        {
            //Arrange
            var treeHeight = 3;

            Root.left = new TreeNode(2);
            Root.right = new TreeNode(3);
            Root.left.left = new TreeNode(2);
            Root.left.right = new TreeNode(5);

            var binaryTree = new BinaryTree();

            //Act
            var height = binaryTree.HeightOfTree(Root);

            //Assert
            Assert.AreEqual(height, treeHeight);
        }
    }
}
