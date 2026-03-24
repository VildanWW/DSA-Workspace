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

            binarySearchTree.AddNode(20);
            binarySearchTree.AddNode(100);
            binarySearchTree.AddNode(10);
            binarySearchTree.AddNode(5);

            Console.WriteLine(binarySearchTree.IsBalanced());

            binarySearchTree.ShowTree();
        }
    }
}