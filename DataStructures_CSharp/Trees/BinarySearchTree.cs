using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    internal class BinarySearchTree<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T> {
        public Node<T>? Head { get; private set; }
        public int Count { get; set; }

        public bool IsReadOnly => false;
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

        private IEnumerable<T> InOrder(Node<T> node) {
            if (node == null) yield break;

            foreach (T value in InOrder(node.Left)) yield return value;

            yield return node.Value;

            foreach (T val in InOrder(node.Right)) yield return val;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        private IEnumerable<T> PostOrder(Node<T> node) {
            if (node == null) yield break;

            foreach (T value in PostOrder(node.Left)) yield return value;

            foreach (T value in PostOrder(node.Right)) yield return value;

            yield return node.Value;
        }

        private IEnumerable<T> PreOrder(Node<T> node) {
            if (node == null) yield break;

            yield return node.Value;

            foreach (T value in PreOrder(node.Left)) yield return value;

            foreach (T value in PreOrder(node.Right)) yield return value;
        }

        public IEnumerable<T> LevelOrder() {
            if (Head == null) yield break;

            var queue = new Queue<Node<T>>();
            queue.Enqueue(Head);

            while (queue.Count > 0) {
                var current = queue.Dequeue();

                yield return current.Value;

                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }
        }

        public IEnumerable<T> PreOrder() {
            return PreOrder(Head);
        }

        public IEnumerable<T> PostOrder() {
            return PostOrder(Head);
        }

        public IEnumerator<T> GetEnumerator() {
            return InOrder(Head).GetEnumerator();
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

        public bool Remove(T value) {
            if (Find(value) != null) {
                Head = RemoveNode(Head, value);
                Count--;
                return true;
            }

            return false;
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

        public void Add(T item) {
            Head = AddNode(Head, item);

            Count++;
        }

        public void ShowTree() {
            ShowTree(Head, 0);
        }

        public bool Contains(T value) {
            return Contains(Head, value);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Index outside of array");

            if (array.Length - arrayIndex < Count) throw new ArgumentOutOfRangeException("little space");

            foreach(T value in this) {
                array[arrayIndex++] = value;
            }
        }
    }
}
