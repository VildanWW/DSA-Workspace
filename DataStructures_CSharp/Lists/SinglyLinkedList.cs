using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures_CSharp.Lists {
    internal class SinglyLinkedList<T> : IEnumerable<T> {
        public Node<T> head { get; private set; }
        public Node<T> tail { get; private set; }
        public int count { get; private set; }

        public SinglyLinkedList() { count = 0; }

        public IEnumerator<T> GetEnumerator() {
            Node<T> current = head;
            while(current !=null) {
                yield return current.value;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public T this[int index] {
            
            get {
                return FindElement(index);
            }

            set {
                if (index < 0 || index >= count) throw new IndexOutOfRangeException();
                Node<T> current = head;
                for(int i=0;i<index;i++) {
                    current = current.next;
                }
                current.value = value;
            }
        }

        public T FindElement(int index) {
            Node<T> current = head;

            if (index < 0 || index >= count) throw new IndexOutOfRangeException();

            for (int i = 0; i < index; i++) {
                current = current.next;
            }

            return current.value;
        }

        public void Clear() {
            head = null;
            tail = null;
            count = 0;
        }

        public void RemoveFirst() {
            Remove(0);
        }

        public void RemoveLast() {
            Remove(count - 1);
        }

        public void Insert(int index, T value) {
            if(index<0 || index>count) throw new IndexOutOfRangeException();

            Node<T> current = head;

            if(index==0) {
                Node<T> node = new Node<T>(value);

                node.next = current;
                head = node;
                if (count == 0) tail = head;
            }
            else {
                
                for(int i=0;i<index-1;i++) {
                    current = current.next;
                }

                Node<T> node = new Node<T>(value);
                node.next = current.next;
                current.next = node;

                if(node.next == null) {
                    tail = node;
                }
            }

            count++;
        }

        public void Add(T value) {
            if (head == null) {
                head = new Node<T>(value);
                tail = head;
                count++;

                return;
            }
            
            tail.next = new Node<T>(value);
            tail = tail.next;
            
            count++;
        }

        public void Remove(int index) {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Node<T> current = head;

            if (index == 0) {
                head = head.next;
                if (count == 1)
                    tail = null;
            }
            else if (index == count - 1) {
                while (current.next != tail) {
                    current = current.next;
                }
                tail = current;
                current.next = null;
            }
            else {
                for (int i = 0; i < index - 1; i++) {
                    current = current.next;
                }
                current.next = current.next.next;
            }

            count--;
        }

        public void AddFirstElement(T value) {
            if (head == null) {
                head = new Node<T>(value);
                tail = head;
                count++;
                return;
            }

            Node<T> newHead = new Node<T>(value);
            newHead.next = head;

            head = newHead;
            count++;
        }

        public void ShowList() {
            Node<T> current = head;

            if(current == null) throw new Exception("List is empty");
               
            while(current != null) {
                Console.Write($"{current.value} ");
                current = current.next;
            }
            Console.WriteLine();
        }
    }
}
