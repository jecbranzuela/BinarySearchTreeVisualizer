using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Branzuela_BinarySearchTreeVisualizer
{
    public class BinaryTree<T>
    {
        private readonly Queue<BinaryTreeNode<T>> _inOrderTraversalResult = new Queue<BinaryTreeNode<T>>();
        private readonly Queue<BinaryTreeNode<T>> _preOrderTraversalResult = new Queue<BinaryTreeNode<T>>();
        private readonly Queue<BinaryTreeNode<T>> _postOrderTraversalResult = new Queue<BinaryTreeNode<T>>();
        private readonly Queue<BinaryTreeNode<T>> _balancedBinaryTreeNodes = new Queue<BinaryTreeNode<T>>();
        public BinaryTreeNode<T> Root { get; set; }
        public virtual BinaryTreeNode<T> BalanceHelper(List<BinaryTreeNode<T>> nodes, int start, int end)
        {
            // base case  
            if (start > end)
            {
                return null;
            }

            /* Get the middle element and make it root */
            int mid = (start + end) / 2;
            BinaryTreeNode<T> node = nodes[mid];

            /* Using index in Inorder traversal, construct  
               left and right subtress */
            node.Left = BalanceHelper(nodes, start, mid - 1);
            node.Right = BalanceHelper(nodes, mid + 1, end);

            return node;
        }


        public virtual BinaryTreeNode<T> Balance()
        {
            List<BinaryTreeNode<T>> inOrder = new List<BinaryTreeNode<T>>();
            var q = InOrderTraversal();
            foreach (var a in q) inOrder.Add(a);
            int n = inOrder.Count;
            return BalanceHelper(inOrder, 0, n - 1);
        }
        public int NumberOfLeavesCounter(BinaryTreeNode<T> currentNode) //counts the number of nodes
        {
            if (currentNode == null) return 0; //walay sulod

            if (currentNode.Left == null && currentNode.Right == null) return 1; // naay sulod pero walay left and right

            else return NumberOfLeavesCounter(currentNode.Left) + NumberOfLeavesCounter(currentNode.Right) + 1;
        }
        public int levelFromRootCounter(BinaryTreeNode<T> node)
        {
            int levelCounter = 1;
            var currNode = Root;
            while (node != currNode)
            {
                if (Compare(currNode.Data, node.Data) == 1) //greater than node leafToDelete
                {
                    currNode = currNode.Left;
                    levelCounter++;
                }
                else if (Compare(currNode.Data, node.Data) == -1) //lesser than node leafToDelete
                {
                    currNode = currNode.Right;
                    levelCounter++;
                }
            }

            return levelCounter;
        }
        public void Delete(T key)
        {
            Root = DeleteHelper(Root, key);
        }
        BinaryTreeNode<T> DeleteHelper(BinaryTreeNode<T> root, T key)
        {
            if (root == null) return root;


            if (Compare(root.Data,key) == 1) //mas dako ang root
                root.Left = DeleteHelper(root.Left, key);
            else if (Compare(root.Data,key) == -1) //mas dako ang to be deleted
                root.Right = DeleteHelper(root.Right, key);

            else
            {
                // node with only one child or no child  
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;

                // node with two children: Get the 
                // inorder successor (smallest  
                // in the right subtree)  
                root.Data = minimumValue(root.Right);

                // Delete the inorder successor  
                root.Right = DeleteHelper(root.Right, root.Data);
            }
            return root;
        }
        T minimumValue(BinaryTreeNode<T> root)
        {
            T minv = root.Data;
            while (root.Left != null)
            {
                minv = root.Left.Data;
                root = root.Left;
            }
            return minv;
        }
        public BinaryTreeNode<T> Search(T data, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;

            var currentNode = Root;
            BinaryTreeNode<T> prev = null;
            while (currentNode != null)
            {
                int compareResult = comparer.Compare(data, currentNode.Data);
                if (compareResult == 0) return currentNode;

                prev = currentNode; //set prev to current node, for reference

                currentNode = compareResult < 0 ? currentNode.Left : currentNode.Right;
            }

            return null;
        }

        public int Compare(T data1, T data2, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            return comparer.Compare(data1, data2);
        }
        public void Clear()
        {
            Root = null;
        }
        public void Insert(T data, IComparer<T> comparer = null) //insert leafToDelete unto the binary tree
        {
            if (comparer == null) comparer = Comparer<T>.Default;

            var currentNode = Root;
            BinaryTreeNode<T> prev = null;
            //look for the parent to insert the leafToDelete
            //store this parent to the variable prev
            while (currentNode != null)
            {
                int compareResult = comparer.Compare(data, currentNode.Data); 
                //kung -1, less than, kung 1, greater than, kung equal, throw error kay di pwede nga pareho'g value

                if (compareResult == 0) return;

                prev = currentNode; //set prev to current node, for reference

                if (compareResult < 0) //if compare result is -1
                    currentNode = currentNode.Left; //ibalhin napud ang currentnode sa left, para macompare nasad ang values kung asa mas dako nila sa leafToDelete
                else //if compare result is 1
                    currentNode = currentNode.Right; //ibalhin ang currentnode sa right, para macompare ang values kung asa mas dako nila sa leafToDelete
            }
            //set the node to the correct direction
            //set the root if it is null
            //set to left if the leafToDelete is less than the parent(prev)
            //set to right otherwise
            if (Root == null) Root = new BinaryTreeNode<T>(data); //if walay sulod ang tree sa pag insert, matic nga mahimong root ang leafToDelete
            else //kung naay sulod
            {
                int compareResult = comparer.Compare(data, prev.Data); //icompare una values sa leafToDelete og sa prev
                if (compareResult<0) prev.Left = new BinaryTreeNode<T>(data); //if -1 ang value sa compareresult, ibutang sa left kay mas gamay ang value 
                else prev.Right = new BinaryTreeNode<T>(data); //if 1, ibutang sa right kay mas dako'g value
            }
        }
        public Queue<BinaryTreeNode<T>> BreadthFirst(BinaryTreeNode<T> currentNode = null)
        {
            var output = new Queue<BinaryTreeNode<T>>();
            if (currentNode == null) currentNode = Root;
            var tempQueue = new Queue<BinaryTreeNode<T>>();
            if (currentNode == null) return new Queue<BinaryTreeNode<T>>();

            tempQueue.Enqueue(currentNode);

            while (tempQueue.Count>0)
            {
                currentNode = tempQueue.Dequeue();
                output.Enqueue(currentNode);

                if (currentNode.Left != null)
                    tempQueue.Enqueue(currentNode.Left);
                if (currentNode.Right != null)
                    tempQueue.Enqueue(currentNode.Right);
            }
            return output;
        }

        public Queue<BinaryTreeNode<T>> InOrderTraversal(BinaryTreeNode<T> node = null)
        {
            if (node == null) node = Root;
            return InOrderTraversalHelper(node);
        }
        public Queue<BinaryTreeNode<T>> InOrderTraversalHelper(BinaryTreeNode<T> node = null) //ascending order
        {
            if (node != null)
            {
                InOrderTraversalHelper(node.Left);
                //visit
                _inOrderTraversalResult.Enqueue(node);
                InOrderTraversalHelper(node.Right);
            }

            return _inOrderTraversalResult;
        }
        public Queue<BinaryTreeNode<T>> PreOrderTraversal(BinaryTreeNode<T> node = null)
        {
            if (node == null) node = Root;
            return PreOrderTraversalHelper(node);
        }
        public Queue<BinaryTreeNode<T>> PreOrderTraversalHelper(BinaryTreeNode<T> node = null) //visit una, VLR
        {
            if (node != null)
            {
                //visit
                _preOrderTraversalResult.Enqueue(node); //V
                PreOrderTraversalHelper(node.Left); //L

                PreOrderTraversalHelper(node.Right);//R
            }

            return _preOrderTraversalResult;
        }
        public Queue<BinaryTreeNode<T>> PostOrderTraversal(BinaryTreeNode<T> node = null)
        {
            if (node == null) node = Root;
            return PostOrderTraversalHelper(node);
        }
        public Queue<BinaryTreeNode<T>> PostOrderTraversalHelper(BinaryTreeNode<T> node = null) //LRV
        {
            if (node != null)
            {
                PostOrderTraversalHelper(node.Left);
                PostOrderTraversalHelper(node.Right);
                //visit
                _postOrderTraversalResult.Enqueue(node);
            }

            return _postOrderTraversalResult;
        }

        public BinaryTreeNode<T> GetParent(BinaryTreeNode<T> toGetParent, BinaryTreeNode<T> root)
        {
            var result = toGetParent;
            if (root.Left != null) result = toGetParent == root.Left ? root : GetParent(result, root.Left);
            if (root.Right != null) result = toGetParent == root.Right ? root : GetParent(result, root.Right);
            return result;
        }
    }
}