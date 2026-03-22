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

        private Node<T> AddNode(Node<T> head, T value) {
            if (head == null) {
                return new Node<T>(value);
            }

            if (value.CompareTo(head.Value) < 0) {
                head.Left = AddNode(head.Left, value);
            } else if(value.CompareTo(head.Value)>0) {
                head.Right = AddNode(head.Right, value);
            }

            return head;
        }

        private void ShowTree(Node<T> head, int ur) {
            if (head == null) return;

            ShowTree(head.Left, ur + 1);

            Console.WriteLine(new String(' ', ur * 4) + "-> " + head.Value);
            

            ShowTree(head.Right, ur + 1);
        }

        private bool Contains(Node<T> head, T value) {
            if (head == null) return false;
            int number = value.CompareTo(head.Value);

            if (number == 0) return true;

            if (number < 0) {
                return Contains(head.Left, value);
            } else {
                return Contains(head.Right, value);
            }
        }

        private T GetMin(Node<T> head) {
            if (head.Left == null) return head.Value;

            return GetMin(head.Left);
        }

        private T GetMax(Node<T> head) {
            if(head.Right==null) return head.Value;

            return GetMax(head.Right);
        }

        private Node<T> Find(Node<T> head, T value) {
            if (head == null) return null;

            int number = value.CompareTo(head.Value);
            if (number == 0) return head;

            if (number < 0) {
                return Find(head.Left, value);
            } else {
                return Find(head.Right, value);
            }
        }

        private int HeightTree(Node<T> head) {
            if (head == null) return 0;

            return Math.Max(HeightTree(head.Left), HeightTree(head.Right)) + 1;
        }

        private int CountLeaf(Node<T> head) {
            if (head == null) return 0;

            if (head.Left == null && head.Right == null) return 1;

            return CountLeaf(head.Left) + CountLeaf(head.Right);
        }

        private int GetLevelNode(Node<T> head, T value, int ur) {
            if (head == null) return -1;

            int number = value.CompareTo(head.Value);

            if (number == 0) return ur;

            if(number<0) {
                return GetLevelNode(head.Left, value, ur + 1);
            } else {
                return GetLevelNode(head.Right, value, ur + 1);
            }
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
