using DataStructures_CSharp.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DataStructures.Tests.Trees;

[TestClass]
public class AVL_TreeTests {

    [TestMethod]
    public void Add_ShouldIncreaseCount() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(5);

        Assert.AreEqual(3, tree.Count);
    }

    [TestMethod]
    public void Add_DuplicateValue_ShouldNotIncreaseCount() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(10);

        Assert.AreEqual(1, tree.Count);
    }

    [TestMethod]
    public void Contains_ExistingValue_ShouldReturnTrue() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);

        Assert.IsTrue(tree.Contains(10));
        Assert.IsTrue(tree.Contains(20));
    }

    [TestMethod]
    public void Contains_NonExistingValue_ShouldReturnFalse() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);

        Assert.IsFalse(tree.Contains(99));
    }

    [TestMethod]
    public void Remove_ExistingValue_ShouldReturnTrueAndDecreaseCount() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);

        bool result = tree.Remove(10);

        Assert.IsTrue(result);
        Assert.AreEqual(1, tree.Count);
        Assert.IsFalse(tree.Contains(10));
    }

    [TestMethod]
    public void Remove_NonExistingValue_ShouldReturnFalse() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);

        bool result = tree.Remove(99);

        Assert.IsFalse(result);
        Assert.AreEqual(1, tree.Count);
    }

    [TestMethod]
    public void Remove_FromEmptyTree_ShouldReturnFalse() {
        var tree = new AVL_Tree<int>();

        bool result = tree.Remove(10);

        Assert.IsFalse(result);
        Assert.AreEqual(0, tree.Count);
    }

    [TestMethod]
    public void FindMin_ShouldReturnSmallestValue() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        int min = tree.FindMin();

        Assert.AreEqual(20, min);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void FindMin_EmptyTree_ShouldThrowException() {
        var tree = new AVL_Tree<int>();
        tree.FindMin();
    }

    [TestMethod]
    public void FindMax_ShouldReturnLargestValue() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        int max = tree.FindMax();

        Assert.AreEqual(70, max);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void FindMax_EmptyTree_ShouldThrowException() {
        var tree = new AVL_Tree<int>();
        tree.FindMax();
    }

    [TestMethod]
    public void Clear_ShouldResetTree() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);

        tree.Clear();

        Assert.AreEqual(0, tree.Count);
        Assert.IsTrue(tree.isEmpty());
        Assert.IsFalse(tree.Contains(10));
    }

    [TestMethod]
    public void IsEmpty_ShouldReturnTrueWhenEmpty() {
        var tree = new AVL_Tree<int>();

        Assert.IsTrue(tree.isEmpty());

        tree.Add(10);
        Assert.IsFalse(tree.isEmpty());
    }

    [TestMethod]
    public void IsReadOnly_ShouldReturnFalse() {
        var tree = new AVL_Tree<int>();

        Assert.IsFalse(tree.IsReadOnly);
    }

    [TestMethod]
    public void GetHeight_ShouldReturnCorrectHeight() {
        var tree = new AVL_Tree<int>();

        Assert.AreEqual(0, tree.GetHeight(tree.Head));

        tree.Add(10);
        Assert.AreEqual(1, tree.GetHeight(tree.Head));

        tree.Add(20);
        tree.Add(30);

        int height = tree.GetHeight(tree.Head);
        Assert.IsTrue(height >= 2 && height <= 3);
    }

    [TestMethod]
    public void CopyTo_ShouldCopyElementsToArray() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);

        int[] arr = new int[3];
        tree.CopyTo(arr, 0);

        Assert.AreEqual(10, arr[0]);
        Assert.AreEqual(20, arr[1]);
        Assert.AreEqual(30, arr[2]);
    }

    [TestMethod]
    public void CopyTo_WithIndex_ShouldCopyFromIndex() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);

        int[] arr = new int[5];
        tree.CopyTo(arr, 2);

        Assert.AreEqual(0, arr[0]);
        Assert.AreEqual(0, arr[1]);
        Assert.AreEqual(10, arr[2]);
        Assert.AreEqual(20, arr[3]);
        Assert.AreEqual(0, arr[4]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CopyTo_NullArray_ShouldThrowException() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);

        tree.CopyTo(null, 0);
    }

    [TestMethod]
    public void InOrder_ShouldReturnSortedElements() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);
        tree.Add(60);
        tree.Add(80);

        var result = tree.ToList();

        CollectionAssert.AreEqual(new[] { 20, 30, 40, 50, 60, 70, 80 }, result);
    }

    [TestMethod]
    public void PreOrder_ShouldReturnRootFirst() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        var result = tree.PreOrder().ToList();

        Assert.AreEqual(50, result[0]);
    }

    [TestMethod]
    public void PostOrder_ShouldReturnLeavesFirst() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        var result = tree.PostOrder().ToList();

        Assert.AreEqual(20, result[0]);
        Assert.AreEqual(40, result[1]);
        Assert.AreEqual(30, result[2]);
    }

    [TestMethod]
    public void LevelOrder_ShouldReturnBFSOrder() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        var result = tree.LevelOrder().ToList();

        Assert.AreEqual(50, result[0]);
    }

    [TestMethod]
    public void Foreach_ShouldIterateOverTree() {
        var tree = new AVL_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);

        int sum = 0;
        foreach (var item in tree) {
            sum += item;
        }

        Assert.AreEqual(60, sum);
    }

    [TestMethod]
    public void AVL_ShouldStayBalancedAfterManyInsertions() {
        var tree = new AVL_Tree<int>();

        for (int i = 1; i <= 100; i++) {
            tree.Add(i);
        }

        int height = tree.GetHeight(tree.Head);
        int maxHeight = (int)Math.Ceiling(1.44 * Math.Log2(101));

        Assert.IsTrue(height <= maxHeight);
        Assert.AreEqual(100, tree.Count);
    }

    [TestMethod]
    public void AVL_ShouldStayBalancedAfterInsertAndDelete() {
        var tree = new AVL_Tree<int>();

        for (int i = 1; i <= 50; i++) {
            tree.Add(i);
        }

        for (int i = 1; i <= 25; i++) {
            tree.Remove(i);
        }

        Assert.AreEqual(25, tree.Count);

        for (int i = 26; i <= 50; i++) {
            Assert.IsTrue(tree.Contains(i));
        }
    }

    [TestMethod]
    public void Remove_NodeWithTwoChildren_ShouldWork() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);
        tree.Add(60);
        tree.Add(80);

        tree.Remove(50);

        Assert.IsFalse(tree.Contains(50));
        Assert.AreEqual(6, tree.Count);
        Assert.IsTrue(tree.Contains(30));
        Assert.IsTrue(tree.Contains(70));
        Assert.IsTrue(tree.Contains(20));
        Assert.IsTrue(tree.Contains(40));
        Assert.IsTrue(tree.Contains(60));
        Assert.IsTrue(tree.Contains(80));
    }

    [TestMethod]
    public void Remove_NodeWithOneChild_ShouldWork() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);
        tree.Add(80);

        tree.Remove(70);

        Assert.IsFalse(tree.Contains(70));
        Assert.AreEqual(5, tree.Count);
        Assert.IsTrue(tree.Contains(80));
    }

    [TestMethod]
    public void Remove_LeafNode_ShouldWork() {
        var tree = new AVL_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);

        tree.Remove(30);

        Assert.IsFalse(tree.Contains(30));
        Assert.AreEqual(2, tree.Count);
        Assert.IsTrue(tree.Contains(50));
        Assert.IsTrue(tree.Contains(70));
    }

    [TestMethod]
    public void MultipleOperations_ShouldMaintainCorrectCount() {
        var tree = new AVL_Tree<int>();

        tree.Add(10);
        tree.Add(20);
        tree.Add(30);
        Assert.AreEqual(3, tree.Count);

        tree.Remove(20);
        Assert.AreEqual(2, tree.Count);

        tree.Add(15);
        Assert.AreEqual(3, tree.Count);

        tree.Clear();
        Assert.AreEqual(0, tree.Count);
    }
}