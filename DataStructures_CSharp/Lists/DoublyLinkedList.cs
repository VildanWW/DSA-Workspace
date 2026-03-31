using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Lists {
    public class DoublyLinkedList<T> : IEnumerable<T> {
        public NodeDLL<T>? Head { get; private set; }
        public NodeDLL<T>? Tail { get; private set; }
        public int Count { get; private set; }

        public DoublyLinkedList() { Count = 0; }

        public bool IsReadOnly => false;

        public void Clear() {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void Add(T value) {
            if (Head == null) {
                Head = new NodeDLL<T>(value);
                Tail = Head;
                Count++;
                return;
            }

            Count++;
            Tail.Next = new NodeDLL<T>(value);
            Tail.Next.Prev = Tail;
            Tail = Tail.Next;
        }

        public void ShowList() {
            NodeDLL<T> current = Head;

            if (current == null) throw new Exception("List is empty");

            while (current != null) {
                Console.Write($"{current.Value} ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public IEnumerator<T> GetEnumerator() {
            NodeDLL<T> current = Head;

            while (current != null) {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool Contains(T item) {
            foreach(T value in this) {
                if(value.Equals(item)) return true;
            }
            return false;
        }

        public T FindElement(int index) {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

            NodeDLL<T>? current = (index < Count / 2) ? Head : Tail;

            if (index < Count / 2) {
                for (int i = 0; i < index; i++) current = current.Next;
            }
            else {
                for (int i = Count - 1; i > index; i--) current = current.Prev;
            }

            return current.Value;
        }

        public int IndexOf(T item) {
            int index = 0;
            foreach (T value in this) {
                if (value.Equals(item)) return index;
                index++;
            }
            return -1;
        }

        public void Insert(int index, T item) {
            if(index<0 || index>Count) throw new IndexOutOfRangeException("Index outside");

            if(index==0) {
                NodeDLL<T> node = new NodeDLL<T>(item);

                if(Tail==null) {
                    Head = node;
                    Tail = node;
                } else {
                    node.Next = Head;
                    Head.Prev = node;
                    Head = node;
                }                
            }
            else if(index == Count) {
                Add(item); 
                return;
            }
            else {
                NodeDLL<T> node = new NodeDLL<T>(item);
                NodeDLL<T>? cur = (Count / 2 > index) ? Head : Tail;

                if(Count/2>index) {
                    for(int i=0;i<index;i++) {
                        cur = cur.Next;
                    }

                    node.Next = cur;
                    node.Prev = cur.Prev;

                    if(cur.Prev!=null) {
                        cur.Prev.Next = node;
                    } else {
                        Head = node;
                    }

                    cur.Prev = node;
                }
                else {
                    for(int i=Count-1;i>index;i--) {
                        cur = cur.Prev;
                    }

                    node.Next = cur;
                    node.Prev = cur.Prev;

                    if(cur.Prev!=null) {
                        cur.Prev.Next = node;
                    } else {
                        Head = node;
                    }

                    cur.Prev = node;
                }
            }

            Count++;
        }

        public void AddFirstElement(T value) {
            if(Count==0) {
                Head = new NodeDLL<T>(value);
                Tail = Head;
                Count++;
                return;
            }

            NodeDLL<T> node = new NodeDLL<T>(value);
            Count++;

            node.Next = Head;
            Head.Prev = node;
            Head = node;
        }

        public void RemoveLast() {
            if (Count == 0) throw new Exception("Size of list 0");
            if(Count ==1) {
                Head = null;
                Tail = null;
                Count--;
                return;
            }

            Count--;
            Tail = Tail.Prev;
            Tail.Next = null;
        }

        public void RemoveFirst() {
            if (Count == 0) throw new Exception("Size of list 0");

            if(Count==1) {
                Head = null;
                Tail = null;
                Count--;
                return;
            }

            Head = Head.Next;
            Head.Prev = null;

            Count--;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new IndexOutOfRangeException("Index outside of array");
            if(array.Length-arrayIndex<Count) throw new ArgumentException("Deficiency space");

            foreach (T item in this) {
                array[arrayIndex++] = item;
            }
        }

        public T this[int index] {
            get {
                return FindElement(index);
            }

            set {
                if(index<0 || index>=Count) throw new ArgumentOutOfRangeException();

                NodeDLL<T>? current = (index < Count / 2) ? Head : Tail;

                if (index < Count / 2) {
                    for(int i=0;i<index;i++) {
                        current = current.Next;
                    }
                } else {
                    for(int i=Count-1;i>index;i--) {
                        current = current.Prev;
                    }
                }

                current.Value = value;
            }
        }

        public void RemoveAt(int index) {
            if (Count == 0) throw new Exception("Count = 0");
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException("index not good");

            if (index == 0) {
                RemoveFirst();
                return;
            }

            else if(index==Count-1) {
                RemoveLast();
                return;
            }

            NodeDLL<T> cur = (Count / 2 > index) ? Head : Tail;

            if (index < Count / 2) {
                for(int i=0;i<index;i++) {
                    cur = cur.Next;
                }

                cur.Prev.Next = cur.Next;
                cur.Next.Prev = cur.Prev;
            } else {
                for (int i = Count - 1; i > index; i--) {
                    cur = cur.Prev;
                }

                cur.Prev.Next = cur.Next;
                cur.Next.Prev = cur.Prev;
            }

            Count--;
        }

        public bool Remove(T item) {
            if (Count == 0) throw new Exception("Count = 0");

            int index = 0;
            foreach (var cur in this) {
                if (item.Equals(cur)) {
                    RemoveAt(index);
                    return true;
                }
                index++;
            }
            return false;
        }
    }
}
