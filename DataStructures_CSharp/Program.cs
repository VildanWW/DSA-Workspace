using DataStructures_CSharp.Lists;
using System;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            SinglyLinkedList<int> singlyLinkedList = new SinglyLinkedList<int>();

            singlyLinkedList.Add(2);
            singlyLinkedList.Add(1);
            singlyLinkedList.Add(42);

            singlyLinkedList.Remove(2);

            singlyLinkedList.ShowList();

            Console.WriteLine(singlyLinkedList.tail.value);
        }
    }
}