using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    internal class BinarySearchTree<T> where T : IComparable<T> {
        public Node<T>? Head { get; private set; }
        public int Count { get; set; }

        public BinarySearchTree() {
            Count = 0;
        }

        private Node<T> AddNode(Node<T> node, T value) {
            if (node == null) {
                return new Node<T>(value);
            }

            if (value.CompareTo(node.Value) < 0) {
                node.Left = AddNode(node.Left, value);
            } else if(value.CompareTo(node.Value)>0) {
                node.Right = AddNode(node.Right, value);
            }

            return node;
        }

        private void ShowTree(Node<T> node, int ur) {
            if (node == null) return;

            ShowTree(node.Left, ur + 1);

            Console.WriteLine(new String(' ', ur * 4) + "-> " + node.Value);
            

            ShowTree(node.Right, ur + 1);
        }

        private bool Contains(Node<T> node, T value) {
            if (node == null) return false;
            int number = value.CompareTo(node.Value);

            if (number == 0) return true;

            if (number < 0) {
                return Contains(node.Left, value);
            } else {
                return Contains(node.Right, value);
            }
        }

        private T GetMin(Node<T> node) {
            if (node.Left == null) return node.Value;

            return GetMin(node.Left);
        }

        private T GetMax(Node<T> node) {
            if(node.Right==null) return node.Value;

            return GetMax(node.Right);
        }

        private Node<T> Find(Node<T> node, T value) {
            if (node == null) return null;

            int number = value.CompareTo(node.Value);
            if (number == 0) return node;

            if (number < 0) {
                return Find(node.Left, value);
            } else {
                return Find(node.Right, value);
            }
        }

        private int HeightTree(Node<T> node) {
            if (node == null) return 0;

            return Math.Max(HeightTree(node.Left), HeightTree(node.Right)) + 1;
        }

        private int CountLeaf(Node<T> node) {
            if (node == null) return 0;

            if (node.Left == null && node.Right == null) return 1;

            return CountLeaf(node.Left) + CountLeaf(node.Right);
        }

        private int GetLevelNode(Node<T> node, T value, int ur) {
            if (node == null) return -1;

            int number = value.CompareTo(node.Value);

            if (number == 0) return ur;

            if(number<0) {
                return GetLevelNode(node.Left, value, ur + 1);
            } else {
                return GetLevelNode(node.Right, value, ur + 1);
            }
        }

        private Node<T> RemoveNode(Node<T> node, T value) {
            if (node == null) return null;

            int number = value.CompareTo(node.Value);

            if(number == 0) {
                if (node.Left == null) return node.Right;

                if (node.Right == null) return node.Left;

                node.Value = GetMin(node.Right);

                node.Right = RemoveNode(node.Right, node.Value);
                return node;
            }
            
            if(number < 0) {
                node.Left = RemoveNode(node.Left, value);
            } else {
                node.Right = RemoveNode(node.Right, value);
            }

            return node;
        }
        
        public bool IsBalanced(Node<T> node) {
            if (node == null) return true;

            int left = HeightTree(node.Left);
            int right = HeightTree(node.Right);

            return Math.Abs(left - right) <= 1 && IsBalanced(node.Left) && IsBalanced(node.Right);
        }

        public bool IsBalanced() {
            return IsBalanced(Head);
        }

        public void RemoveNode(T value) {
            Head = RemoveNode(Head, value);
        }

        public int GetLevelNode(T value) {
            return GetLevelNode(Head, value, 0);
        }

        public int CountLeaf() {
            return CountLeaf(Head);
        }

        public int HeightTree() {
            return HeightTree(Head);
        }
        
        public bool IsEmpty() {
            return Head == null;
        }

        public Node<T> Find(T value) {
            return Find(Head, value);
        }

        public void Clear() {
            Head = null;
            Count = 0;
        }

        public T GetMax() {
            return GetMax(Head);
        }

        public T GetMin() {
            return GetMin(Head);
        }

        public void AddNode(T value) {
            Head = AddNode(Head, value);

            Count++;
        }

        public void ShowTree() {
            ShowTree(Head, 0);
        }

        public bool Contains(T value) {
            return Contains(Head, value);
        }
    }
}
