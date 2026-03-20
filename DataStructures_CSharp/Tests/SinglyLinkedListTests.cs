using DataStructures_CSharp.Lists;
using DataStructures_CSharp.Sorting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures_CSharp.Tests {
    internal class SinglyLinkedListTests {
        public static void Run() {

            SinglyLinkedList<int> ints1 = new SinglyLinkedList<int>();
            SinglyLinkedList<int> ints2 = new SinglyLinkedList<int>();

            Console.WriteLine("=== Singly Linked List Functional Tests ===");

            int size = 1_000;
            Random random = new Random();

            for (int i = 0; i < size; i++) {
                int value = random.Next(1000);

                ints1.Add(value);
                ints2.Add(value);
            }

            Console.WriteLine("Performing complex operations: RemoveFirst, RemoveLast, RemoveAt, Remove(value), Insert(0)...");
            
            ints1.RemoveFirst(); ints1.RemoveLast(); ints1.RemoveAt(476); ints1.Remove(ints1[736]); ints1.Add(823); ints1.Insert(0, 100);
            ints2.RemoveFirst(); ints2.RemoveLast(); ints2.RemoveAt(476); ints2.Remove(ints2[736]); ints2.Add(823); ints2.Insert(0, 100);

            bool flag2 = true;
            for (int i = 0; i < ints1.Count; i++) {
                if (ints1[i] != ints2[i]) {
                    flag2 = false;
                }
            }
            Console.WriteLine($"Result:{flag2}");

            ints1.Clear(); ints2.Clear();

            Console.WriteLine("Check moment, when two lists is empty");

            Console.WriteLine((ints1.Count == ints2.Count) ? "Lists are empty" : "Cleaning failed");

            ints1.Add(9999);
            Console.WriteLine(ints1.Contains(9999) ? "Contains: OK" : "Contains: FAILED");
            Console.WriteLine(ints1.IndexOf(-9999) == -1 ? "IndexOf: OK" : "IndexOf: FAILED");

            ints1.Clear();
            ints1.Add(10); ints1.Add(20); ints1.Add(30);

            int[] testArray = new int[ints1.Count + 1];
            
            ints1.CopyTo(testArray, 1);

            bool copyOk = (testArray.Length == ints1.Count + 1) && (testArray[1] == 10 && testArray[2] == 20 && testArray[3]==30);

            Console.WriteLine(copyOk ? "CopyTo: OK" : "CopyTo: FAILED");

            ints1.Clear();
            ints1.Add(55);
            ints1.RemoveAt(0);

            bool singleTest = (ints1.Count == 0 && ints1.Head == null && ints1.Tail == null);
            Console.WriteLine(singleTest ? "Single Element Test: OK" : "Single Element Test: FAILED");

            ints1.Add(10);
            ints1.Insert(ints1.Count, 20);

            bool insertTailTest = (ints1.Tail != null && ints1.Tail.Value == 20);
            Console.WriteLine(insertTailTest ? "Insert Tail Test: OK" : "Insert Tail Test: FAILED");

            ints1.Clear();
            ints1.Add(100);
            ints1[0] = 200; 

            bool indexerSetTest = (ints1[0] == 200);
            Console.WriteLine(indexerSetTest ? "Indexer Set Test: OK" : "Indexer Set Test: FAILED");
        }
    }
}
