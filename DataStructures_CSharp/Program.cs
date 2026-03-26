using DataStructures_CSharp.Lists;
using DataStructures_CSharp.Sorting;
using DataStructures_CSharp.Tests;
using DataStructures_CSharp.Trees;
using System;
using System.Diagnostics;
using System.Linq;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>();

            binarySearchTree.Add(20);
            binarySearchTree.Add(100);
            binarySearchTree.Add(10);
            binarySearchTree.Add(5);
            binarySearchTree.Add(1000);

            int[] ints = new int[6];

            binarySearchTree.CopyTo(ints, 1);

            foreach(int i in ints) {
                Console.WriteLine(i);
            }

            binarySearchTree.ShowTree();
        }
    }
}