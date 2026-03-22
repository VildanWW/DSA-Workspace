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

            binarySearchTree.AddNode(24);
            binarySearchTree.AddNode(12);
            binarySearchTree.AddNode(-232);
            binarySearchTree.AddNode(100);
            binarySearchTree.AddNode(14);
            binarySearchTree.AddNode(11111);
            binarySearchTree.AddNode(13);

            Console.WriteLine(binarySearchTree.CountLeaf());
        }
    }
}