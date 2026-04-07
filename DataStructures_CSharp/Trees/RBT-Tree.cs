using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Trees {
    public class RBT_Tree<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T> {
        public NodeRBT<T> Head { get; private set; }
        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public RBT_Tree() { Count = 0; }

        private void BalanceAfterInsert(NodeRBT<T> node) {
            while (node != Head && node.Parent != null && node.Parent.Color == NodeColor.Red) {
                NodeRBT<T> parent = node.Parent;
                NodeRBT<T> grandParent = parent.Parent;

                if (parent == grandParent.Right) {
                    NodeRBT<T> uncle = grandParent.Left;

                    if (uncle != null && uncle.Color == NodeColor.Red) {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandParent.Color = NodeColor.Red;
                        node = grandParent;
                    }
                    else {
                        if (node == parent.Left) {
                            node = parent;
                            RotateRight(node);
                        }
                        else {
                            node.Parent.Color = NodeColor.Black;
                            node.Parent.Parent.Color = NodeColor.Red;
                            RotateLeft(grandParent);
                        }
                    }
                }
                else {
                    NodeRBT<T> uncle = grandParent.Right;

                    if (uncle != null && uncle.Color == NodeColor.Red) {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandParent.Color = NodeColor.Red;
                        node = grandParent;
                    }
                    else {
                        if (node == parent.Right) {
                            node = parent;
                            RotateLeft(node);
                        }
                        else {
                            node.Parent.Color = NodeColor.Black;
                            node.Parent.Parent.Color = NodeColor.Red;
                            RotateRight(grandParent);
                        }
                    }
                }
            }
            Head.Color = NodeColor.Black;
        }

        private void RotateLeft(NodeRBT<T> node) {
            NodeRBT<T> pivot = node.Right;

            node.Right = pivot.Left;
            if (pivot.Left != null) {
                pivot.Left.Parent = node;
            }

            pivot.Parent = node.Parent;
            if (node.Parent == null) {
                Head = pivot;
            }
            else if (node == node.Parent.Left) {
                node.Parent.Left = pivot;
            }
            else {
                node.Parent.Right = pivot;
            }

            pivot.Left = node;
            node.Parent = pivot;
        }

        private void RotateRight(NodeRBT<T> node) {
            NodeRBT<T> pivot = node.Left;

            node.Left = pivot.Right;
            if (pivot.Right != null) {
                pivot.Right.Parent = node;
            }

            pivot.Parent = node.Parent;
            if (node.Parent == null) {
                Head = pivot;
            }
            else if (node == node.Parent.Right) {
                node.Parent.Right = pivot;
            }
            else {
                node.Parent.Left = pivot;
            }

            pivot.Right = node;
            node.Parent = pivot;
        }

        private NodeRBT<T> Add(NodeRBT<T> node, NodeRBT<T> parent, T value, out NodeRBT<T> newNode) {
            if (node == null) {
                newNode = new NodeRBT<T>(value) { Parent = parent, Color = NodeColor.Red };
                return newNode;
            }

            newNode = null;
            if (value.CompareTo(node.Value) < 0) {
                node.Left = Add(node.Left, node, value, out newNode);
            }
            else if (value.CompareTo(node.Value) > 0) {
                node.Right = Add(node.Right, node, value, out newNode);
            }

            return node;
        }

        public void Add(T value) {
            NodeRBT<T> newNode;
            NodeRBT<T> oldHead = Head;

            Head = Add(Head, null, value, out newNode);

            if (newNode != null) {
                BalanceAfterInsert(newNode);
                Head.Color = NodeColor.Black;
                Count++;
            }
        }

        private IEnumerable<T> InOrder(NodeRBT<T> node) {
            if (node == null) yield break;

            foreach (var element in InOrder(node.Left)) yield return element;

            yield return node.Value;

            foreach (var element in InOrder(node.Right)) yield return element;
        }

        public IEnumerator<T> GetEnumerator() {
            return InOrder(Head).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private IEnumerable<T> PostOrder(NodeRBT<T> node) {
            if (node == null) yield break;

            foreach (var element in PostOrder(node.Left)) yield return element;

            foreach (var element in PostOrder(node.Right)) yield return element;

            yield return node.Value;
        }

        public IEnumerable<T> PostOrder() {
            return PostOrder(Head);
        }

        private IEnumerable<T> PreOrder(NodeRBT<T> node) {
            if (node == null) yield break;

            yield return node.Value;

            foreach (T value in PreOrder(node.Left)) yield return value;

            foreach (T value in PreOrder(node.Right)) yield return value;
        }

        public IEnumerable<T> PreOrder() {
            return PreOrder(Head);
        }

        private IEnumerable<T> LevelOrder(NodeRBT<T> node) {
            Queue<NodeRBT<T>> q = new Queue<NodeRBT<T>>();
            q.Enqueue(node);

            while(q.Count > 0) {
                var element = q.Dequeue();
                yield return element.Value;

                if(element.Left!= null) q.Enqueue(element.Left);
                if(element.Right!=null) q.Enqueue(element.Right);
            }
        }

        public IEnumerable<T> LevelOrder() {
            return LevelOrder(Head);
        }

        public void Clear() {
            Head = null;
            Count = 0;
        }

        public bool Contains(NodeRBT<T> node, T value) {
            if (node == null) return false;

            if (value.Equals(node.Value)) return true;

            if (value.CompareTo(node.Value) < 0) {
                return Contains(node.Left, value);
            }
            else {
                return Contains(node.Right, value);
            }
        }

        public bool Contains(T value) {
            return Contains(Head, value);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (array.Length - arrayIndex < Count) throw new Exception("little space");
            if (arrayIndex < 0) throw new IndexOutOfRangeException("index < 0");

            foreach (T item in this) {
                array[arrayIndex++] = item;
            }
        }

        public bool Remove(T item) {
            NodeRBT<T> node = Head;
            while (node != null) {
                int cmp = item.CompareTo(node.Value);
                if (cmp == 0) break;
                node = cmp < 0 ? node.Left : node.Right;
            }

            if (node == null) return false;

            NodeRBT<T> replacement;
            NodeColor deletedColor = node.Color;

            if (node.Left == null || node.Right == null) {
                replacement = ReplaceNode(node);
            }
            else {
                NodeRBT<T> successor = node.Right;
                while (successor.Left != null) successor = successor.Left;
                node.Value = successor.Value;
                deletedColor = successor.Color;
                replacement = ReplaceNode(successor);
            }

            if (deletedColor == NodeColor.Black) {
                NodeRBT<T> balanceParent = (replacement != null) ? replacement.Parent : node.Parent;
                BalanceAfterDelete(replacement, balanceParent);
            }

            Count--;
            return true;
        }

        private NodeRBT<T> ReplaceNode(NodeRBT<T> node) {
            NodeRBT<T> child = (node.Left != null) ? node.Left : node.Right;
            if (node.Parent == null) Head = child;
            else if (node == node.Parent.Left) node.Parent.Left = child;
            else node.Parent.Right = child;
            if (child != null) child.Parent = node.Parent;
            return child;
        }
        
        public int GetHeight(NodeRBT<T> node) {
            if (node == null) return 0;
            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }
       
        private void BalanceAfterDelete(NodeRBT<T> node, NodeRBT<T> parent) {
            while (node != Head && (node == null || node.Color == NodeColor.Black)) {
                if (parent == null) break;

                if (node == parent.Left) {
                    NodeRBT<T> sib = parent.Right;

                    if (sib != null && sib.Color == NodeColor.Red) {
                        sib.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        RotateLeft(parent);
                        sib = parent.Right;
                    }

                    if (sib == null ||
                        ((sib.Left == null || sib.Left.Color == NodeColor.Black) &&
                         (sib.Right == null || sib.Right.Color == NodeColor.Black))) {
                        if (sib != null) sib.Color = NodeColor.Red;
                        node = parent;
                        parent = node.Parent;
                    }
                    else {
                        if (sib.Right == null || sib.Right.Color == NodeColor.Black) {
                            if (sib.Left != null) sib.Left.Color = NodeColor.Black;
                            sib.Color = NodeColor.Red;
                            RotateRight(sib);
                            sib = parent.Right;
                        }
                        sib.Color = parent.Color;
                        parent.Color = NodeColor.Black;
                        if (sib.Right != null) sib.Right.Color = NodeColor.Black;
                        RotateLeft(parent);
                        node = Head;
                    }
                }
                else {
                    NodeRBT<T> sib = parent.Left;

                    if (sib != null && sib.Color == NodeColor.Red) {
                        sib.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        RotateRight(parent);
                        sib = parent.Left;
                    }

                    if (sib == null ||
                        ((sib.Left == null || sib.Left.Color == NodeColor.Black) &&
                         (sib.Right == null || sib.Right.Color == NodeColor.Black))) {
                        if (sib != null) sib.Color = NodeColor.Red;
                        node = parent;
                        parent = node.Parent;
                    }
                    else {
                        if (sib.Left == null || sib.Left.Color == NodeColor.Black) {
                            if (sib.Right != null) sib.Right.Color = NodeColor.Black;
                            sib.Color = NodeColor.Red;
                            RotateLeft(sib);
                            sib = parent.Left;
                        }
                        sib.Color = parent.Color;
                        parent.Color = NodeColor.Black;
                        if (sib.Left != null) sib.Left.Color = NodeColor.Black;
                        RotateRight(parent);
                        node = Head;
                    }
                }
            }
            if (node != null) node.Color = NodeColor.Black;
        }
    }
}
