using DataStructures_CSharp.Lists;
using DataStructures_CSharp.Sorting;
using DataStructures_CSharp.Trees;
using System;
using System.Diagnostics;
using System.Linq;

namespace DataStructures_CSharp {
    class Program {
        static void Main(string[] args) {
            AVL_Tree<int> aVL_Tree = new AVL_Tree<int>();

            aVL_Tree.Add(5);
            aVL_Tree.Add(2);
            aVL_Tree.Add(1);

            aVL_Tree.ShowTree();
        }
    }
}