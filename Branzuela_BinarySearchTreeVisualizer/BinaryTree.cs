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
                if (Compare(currNode.Data, node.Data) == 1) //greater than node data
                {
                    currNode = currNode.Left;
                    levelCounter++;
                }
                else if (Compare(currNode.Data, node.Data) == -1) //lesser than node data
                {
                    currNode = currNode.Right;
                    levelCounter++;
                }
            }

            return levelCounter;
        }

        public bool HasTwoChildren(T leafToCheck)
        {
            var searched = Search(leafToCheck);
            return searched.Left != null && searched.Right != null;
        }

        public void DeleteLessThanTwoNodes(T leafToDelete)
        {
            var searched = Search(leafToDelete);
            if (searched.Left == null && searched.Right == null) //if terminating node
            {
                //mas dako ang node saiyahang parent
                if (Compare(searched.Data, GetParent(searched, Root).Data) == 1) GetParent(searched, Root).Right = null;
                else GetParent(searched, Root).Left = null;
            }
            //naay left child
            if (searched.Left != null && searched.Right == null)
            {
                //if greater sa parent
                if (Compare(searched.Data, GetParent(searched, Root).Data) == 1) GetParent(searched, Root).Right = searched.Left;
                else GetParent(searched, Root).Left = searched.Left;
            }
            //naay right child
            if (searched.Left == null && searched.Right != null)
            {
                //if greater sa parent
                if (Compare(searched.Data, GetParent(searched, Root).Data) == 1) GetParent(searched,Root).Right = searched.Right;
                else GetParent(searched,Root).Left = searched.Right;
            }
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
        public void DeleteByCopying(T data)
        {
            var byeLeaf = Search(data);
            if (byeLeaf == null) throw new Exception("Node not found in Tree");
            var parentLeaf = GetParent(byeLeaf, Root);

            var newLeaf = byeLeaf.Left; // left of byeleaf
            var replacingLeaf = newLeaf;
            var rightBranch = byeLeaf.Right;

            //case: is leaf, left then right
            if (newLeaf == null)
            {
                if (rightBranch == null)
                {
                    if (parentLeaf.Left == byeLeaf) parentLeaf.Left = null;
                    else if (parentLeaf.Right == byeLeaf) parentLeaf.Right = null;
                }
                else
                {
                    parentLeaf.Right = rightBranch;
                    if (parentLeaf == byeLeaf) Root = Root.Right;
                }
            }

            //Find the replacing Leaf
            while (replacingLeaf.Right != null)
            {
                replacingLeaf = replacingLeaf.Right;
            }

            //case: delete root
            if (parentLeaf == byeLeaf)
            {
                Root.Data = replacingLeaf.Data;
                if (replacingLeaf == newLeaf) Root.Left = newLeaf.Left;
            }

            //Specialcase: Replacingleaf is the left 
            if (replacingLeaf == newLeaf)
            {
                if (parentLeaf.Left == byeLeaf) parentLeaf.Left = replacingLeaf;
                else if (parentLeaf.Right == byeLeaf) parentLeaf.Right = replacingLeaf;

                replacingLeaf.Right = rightBranch;
            }

            //Connects Children of replacing leaf to replacing leafs parent
            if (replacingLeaf.Left != null) GetParent(replacingLeaf, Root).Right = replacingLeaf.Left;
            else if (replacingLeaf.Left == null) GetParent(replacingLeaf, Root).Right = null;

            if (parentLeaf.Left == byeLeaf) parentLeaf.Left.Data = replacingLeaf.Data;
            else if (parentLeaf.Right == byeLeaf) parentLeaf.Right.Data = replacingLeaf.Data;
        }

        public void DeleteMerging(T leafToDelete)
        {
            var deleteLeaf = Search(leafToDelete);
            if (deleteLeaf == null) return;
            var parentOfLeafToBeDeleted = GetParent(deleteLeaf, Root);
            var nLeaf = deleteLeaf.Left;
            if (deleteLeaf == Root) //root ang idelete
            {
                if (Root.Left == null && Root.Right == null)
                {
                    Root = null;
                    return;
                }
                parentOfLeafToBeDeleted = parentOfLeafToBeDeleted.Left;
                Root = parentOfLeafToBeDeleted;
            }
            var rightLeaves = deleteLeaf.Right;
            if (nLeaf == null) nLeaf = rightLeaves;
            else
            {
                //pangitaon ang pinaka right node sa left subtree
                var toConnect = nLeaf;
                while (toConnect.Right != null) toConnect = toConnect.Right;

                //iconnect ang right subtree ang rightmost node sa left subtree
                toConnect.Right = rightLeaves;
            }

            if (parentOfLeafToBeDeleted == deleteLeaf) parentOfLeafToBeDeleted.Left = nLeaf;
            else if (parentOfLeafToBeDeleted.Right == deleteLeaf) parentOfLeafToBeDeleted.Right = nLeaf;

        }

    }
}