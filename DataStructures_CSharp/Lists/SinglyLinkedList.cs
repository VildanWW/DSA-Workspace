using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;

namespace DataStructures_CSharp.Lists {
    public class SinglyLinkedList<T> : IEnumerable<T>, ICollection<T>, IList<T> {
        public NodeSLL<T> Head { get; private set; }
        public NodeSLL<T> Tail { get; private set; }
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public SinglyLinkedList() { Count = 0; }

        public IEnumerator<T> GetEnumerator() {
            NodeSLL<T> current = Head;
            while(current !=null) {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public T this[int index] {
            
            get {
                return FindElement(index);
            }

            set {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                NodeSLL<T> current = Head;
                for(int i=0;i<index;i++) {
                    current = current.Next;
                }
                current.Value = value;
            }
        }

        public T FindElement(int index) {
            NodeSLL<T> current = Head;

            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

            for (int i = 0; i < index; i++) {
                current = current.Next;
            }

            return current.Value;
        }

        public void Clear() {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void RemoveFirst() {
            RemoveAt(0);
        }

        public void RemoveLast() {
            RemoveAt(Count - 1);
        }

        public void Insert(int index, T value) {
            if(index<0 || index>Count) throw new IndexOutOfRangeException();

            NodeSLL<T> current = Head;

            if(index==0) {
                NodeSLL<T> node = new NodeSLL<T>(value);

                node.Next = current;
                Head = node;
                if (Count == 0) Tail = Head;
            }
            else {
                
                for(int i=0;i<index-1;i++) {
                    current = current.Next;
                }

                NodeSLL<T> node = new NodeSLL<T>(value);
                node.Next = current.Next;
                current.Next = node;

                if(node.Next == null) {
                    Tail = node;
                }
            }

            Count++;
        }

        public void Add(T value) {
            if (Head == null) {
                Head = new NodeSLL<T>(value);
                Tail = Head;
                Count++;

                return;
            }
            
            Tail.Next = new NodeSLL<T>(value);
            Tail = Tail.Next;
            
            Count++;
        }

        public void RemoveAt(int index) {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            NodeSLL<T> current = Head;

            if (index == 0) {
                Head = Head.Next;
                if (Count == 1)
                    Tail = null;
            }
            else if (index == Count - 1) {
                while (current.Next != Tail) {
                    current = current.Next;
                }
                Tail = current;
                current.Next = null;
            }
            else {
                for (int i = 0; i < index - 1; i++) {
                    current = current.Next;
                }
                current.Next = current.Next.Next;
            }

            Count--;
        }

        public void AddFirstElement(T value) {
            if (Head == null) {
                Head = new NodeSLL<T>(value);
                Tail = Head;
                Count++;
                return;
            }

            NodeSLL<T> newHead = new NodeSLL<T>(value);
            newHead.Next = Head;

            Head = newHead;
            Count++;
        }

        public void ShowList() {
            NodeSLL<T> current = Head;

            if(current == null) throw new Exception("List is empty");
               
            while(current != null) {
                Console.Write($"{current.Value} ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Index outside of array");
            }

            if (array.Length - arrayIndex < Count) {
                throw new ArgumentException("Deficiency space");
            }

            foreach (T value in this) {
                array[arrayIndex] = value;
                arrayIndex++;
            }
        }
        
        public int IndexOf(T item) {
            int index = 0;
            foreach(T value in this) {
                if(item.Equals(value)) {
                    return index;
                }
                index++;
            }

            return -1;
        }

        public bool Contains(T item) {
            foreach(T value in this) {
                if (item.Equals(value)) {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(T item) {
            for (int i = 0; i < this.Count; i++) {
                if (item.Equals(this[i])) {
                    RemoveAt(i);
                    return true;
                }
                
            }
            return false;
        }
    }
}
