using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    public class AVL_Tree<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T> {
        public NodeAVL<T>? Head {  get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        private NodeAVL<T> Add(NodeAVL<T> head, T value) {
            if(head == null) {
                return new NodeAVL<T>(value);
            }

            

            if(value.CompareTo(head.Value) < 0) {
                head.Left = Add(head.Left, value);
            } else if(value.CompareTo(head.Value) > 0) {
                head.Right = Add(head.Right, value);
            } else {
                return head;
            }

            head.Height = 1 + Math.Max(GetHeight(head.Left), GetHeight(head.Right));
            return Balance(head);
        }

        private NodeAVL<T> Balance(NodeAVL<T> node) {
            int balance = GetHeight(node.Left) - GetHeight(node.Right);

            if (balance > 1) {
                if(GetHeight(node.Left.Left)>=GetHeight(node.Left.Right)) {
                    return RotateRight(node);
                } else {
                    node.Left = RotateLeft(node.Left);
                    return RotateRight(node);
                }
            }

            if (balance<-1) {
                if (GetHeight(node.Right.Right) >= GetHeight(node.Right.Left)) {
                    return RotateLeft(node);
                } else {
                    node.Right = RotateRight(node.Right);
                    return RotateLeft(node);
                }
            }

            return node;
        }

        private NodeAVL<T> RotateRight(NodeAVL<T> y) {
            NodeAVL<T> x = y.Left;
            NodeAVL<T> t2 = x.Right;

            x.Right = y;
            y.Left = t2;

            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            return x;
        }

        private NodeAVL<T> RotateLeft(NodeAVL<T> x) {
            NodeAVL<T> y = x.Right;
            NodeAVL<T> t2 = y.Left;

            y.Left = x;
            x.Right = t2;

            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return y;

        }

        public int GetHeight(NodeAVL<T> node) {
            return (node==null) ? 0 : node.Height;
        }

        public void Add(T value) {
            if (!Contains(value)) {
                Head = Add(Head, value);
                Count++;
            }
        }

        private T FindMin(NodeAVL<T> node) {
            if (node.Left == null) return node.Value;

            return FindMin(node.Left);
        }

        public T FindMin() {
            if (Head == null) throw new InvalidOperationException("Tree is empty");
            return FindMin(Head);
        }

        private void ShowTree(NodeAVL<T> node, int ur) {
            if (node == null) return;

            ShowTree(node.Left, ur+1);

            Console.WriteLine(new String(' ', ur * 4) + "-> " + node.Value);

            ShowTree(node.Right, ur+1);
        }

        public void ShowTree() {
            ShowTree(Head, 0);
        }

        public bool isEmpty() {
            return Head == null;
        }

        private T FindMax(NodeAVL<T> node) {
            if(node.Right == null) return node.Value;

            return FindMax(node.Right);
        }

        public bool Contains(NodeAVL<T> node, T value) {
            if (node == null) return false;

            if (value.Equals(node.Value)) return true;

            if(value.CompareTo(node.Value)<0) {
                return Contains(node.Left, value);
            } else {
                return Contains(node.Right, value);
            }
        }

        public bool Contains(T value) {
            return Contains(Head, value);
        }

        public void Clear() {
            Head = null;
            Count = 0;
        }

        public T FindMax() {
            if (Head == null) throw new InvalidOperationException("Tree is empty");
            return FindMax(Head);
        }

        public IEnumerator<T> GetEnumerator() {
            return InOrder(Head).GetEnumerator();
        }

        private IEnumerable<T> InOrder(NodeAVL<T> node) {
            if (node == null) yield break;

            foreach (T value in InOrder(node.Left)) yield return value;

            yield return node.Value;

            foreach(T value in InOrder(node.Right)) yield return value;
        }

        private NodeAVL<T> RemoveNode(NodeAVL<T> node, T value) {
            if (node == null) return null;

            if(value.CompareTo(node.Value)<0) {
                node.Left = RemoveNode(node.Left, value);
            } else if(value.CompareTo(node.Value)>0) {
                node.Right = RemoveNode(node.Right, value);
            } else {
                if (node.Left == null && node.Right == null) return null;

                else if(node.Left==null && node.Right!=null) {
                    return node.Right;
                }
                else if(node.Left!=null && node.Right==null) {
                    return node.Left;
                }
                else {
                    T minValue = FindMin(node.Right);
                    node.Value = minValue;
                    node.Right = RemoveNode(node.Right, minValue);

                    return node;
                }
            }

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return Balance(node);
        }

        public bool Remove(T value) {
            if(!Contains(value)) return false;

            Head = RemoveNode(Head, value);
            Count--;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private IEnumerable<T> PreOrder(NodeAVL<T> node) {
            if (node == null) yield break;

            yield return node.Value;

            foreach(T value in PreOrder(node.Left)) yield return value;

            foreach (T value in PreOrder(node.Right)) yield return value;
        }

        public IEnumerable<T> PreOrder() {
            return PreOrder(Head);
        }

        private IEnumerable<T> PostOrder(NodeAVL<T> node) {
            if(node == null) yield break;

            foreach (T value in PostOrder(node.Left)) yield return value;
            foreach (T value in PostOrder(node.Right)) yield return value;

            yield return node.Value;
        }

        public IEnumerable<T> PostOrder() {
            return PostOrder(Head);
        }

        public IEnumerable<T> LevelOrder() {
            if (Head == null) yield break;
            NodeAVL<T> node = Head;
            Queue<NodeAVL<T>> q = new Queue<NodeAVL<T>>();

            q.Enqueue(node);
            while (q.Count > 0) {
                NodeAVL<T> tp = q.Dequeue();

                yield return tp.Value;

                
                if (tp.Left!=null) q.Enqueue(tp.Left);
                if (tp.Right!=null) q.Enqueue(tp.Right);
            }
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (array.Length - arrayIndex < Count) throw new Exception("little space");
            if (arrayIndex < 0) throw new IndexOutOfRangeException("index < 0");

            foreach(T item in this) {
                array[arrayIndex++] = item;
            }
        }
    }
}
