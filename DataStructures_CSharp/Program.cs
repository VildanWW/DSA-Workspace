using DataStructures_CSharp.Lists;
using System;
using System.Linq;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            SinglyLinkedList<int> singlyLinkedList = new SinglyLinkedList<int>();

            singlyLinkedList.Add(2);
            singlyLinkedList.Add(23);
            singlyLinkedList.Insert(1, 22);

            singlyLinkedList.ShowList();

            Console.WriteLine(singlyLinkedList.count);
            
        }
    }
}