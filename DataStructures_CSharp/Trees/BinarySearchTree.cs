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
