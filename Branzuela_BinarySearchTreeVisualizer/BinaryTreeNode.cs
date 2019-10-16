namespace Branzuela_BinarySearchTreeVisualizer
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }
        public BinaryTree<T> EquivalentData { get; set; } 
        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
    }
}
