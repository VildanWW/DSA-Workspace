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

            int[] n = { 2, 6, 1, 6, 3 };
            Sorting.QuickSort.Sort(n, (x, y) => x < y);

            foreach (int i in n) {
                Console.WriteLine(i);
            }
        }
    }
}