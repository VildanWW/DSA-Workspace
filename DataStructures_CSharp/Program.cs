using DataStructures_CSharp.Lists;
using DataStructures_CSharp.Sorting;
using DataStructures_CSharp.Trees;
using System;
using System.Diagnostics;
using System.Linq;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            RBT_Tree<int> aVL_Tree = new RBT_Tree<int>();

            aVL_Tree.Add(11123);
            aVL_Tree.Add(244);

            foreach (int i in aVL_Tree) {
                Console.WriteLine(i);
            }
        }
    }
}