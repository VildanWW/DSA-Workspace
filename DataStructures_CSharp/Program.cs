using DataStructures_CSharp.Lists;
using DataStructures_CSharp.Sorting;
using DataStructures_CSharp.Trees;
using System;
using System.Diagnostics;
using System.Linq;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>();

            list.Add(34);
            list.Insert(0, 23);
            list.Insert(1, 24);
            list.Add(29);

            list.ShowList();

            foreach (int i in list) {
                Console.WriteLine(i);
            }
        }
    }
}