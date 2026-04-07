using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures_CSharp.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Tests;

[TestClass]
public class RBT_TreeTests {

    [TestMethod]
    public void Add_SingleElement_CountIsOne() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        Assert.AreEqual(1, tree.Count);
        Assert.IsTrue(tree.Contains(10));
    }

    [TestMethod]
    public void Add_MultipleElements_CountIsCorrect() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);
        tree.Add(5);
        tree.Add(15);
        Assert.AreEqual(5, tree.Count);
        Assert.IsTrue(tree.Contains(10));
        Assert.IsTrue(tree.Contains(20));
        Assert.IsTrue(tree.Contains(30));
        Assert.IsTrue(tree.Contains(5));
        Assert.IsTrue(tree.Contains(15));
    }

    [TestMethod]
    public void Add_DuplicateValue_ShouldNotAdd() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(10);
        Assert.AreEqual(2, tree.Count);
    }

    [TestMethod]
    public void Remove_ExistingElement_ReturnsTrueAndDecreasesCount() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);
        bool result = tree.Remove(20);
        Assert.IsTrue(result);
        Assert.AreEqual(2, tree.Count);
        Assert.IsFalse(tree.Contains(20));
    }

    [TestMethod]
    public void Remove_NonExistingElement_ReturnsFalse() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        bool result = tree.Remove(99);
        Assert.IsFalse(result);
        Assert.AreEqual(2, tree.Count);
    }

    [TestMethod]
    public void Remove_OnlyElement_TreeBecomesEmpty() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        bool result = tree.Remove(10);
        Assert.IsTrue(result);
        Assert.AreEqual(0, tree.Count);
        Assert.IsFalse(tree.Contains(10));
        Assert.IsNull(tree.Head);
    }

    [TestMethod]
    public void Clear_NonEmptyTree_ResetsTree() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);
        tree.Clear();
        Assert.AreEqual(0, tree.Count);
        Assert.IsNull(tree.Head);
        Assert.IsFalse(tree.Contains(10));
    }

    [TestMethod]
    public void Contains_EmptyTree_ReturnsFalse() {
        var tree = new RBT_Tree<int>();
        Assert.IsFalse(tree.Contains(10));
    }

    [TestMethod]
    public void InOrderTraversal_ReturnsSortedElements() {
        var tree = new RBT_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);
        tree.Add(60);
        tree.Add(80);

        var result = tree.ToList();
        var expected = new List<int> { 20, 30, 40, 50, 60, 70, 80 };

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void PreOrderTraversal_ReturnsCorrectOrder() {
        var tree = new RBT_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        var result = tree.PreOrder().ToList();
        var expected = new List<int> { 50, 30, 20, 40, 70 };

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void PostOrderTraversal_ReturnsCorrectOrder() {
        var tree = new RBT_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        var result = tree.PostOrder().ToList();
        var expected = new List<int> { 20, 40, 30, 70, 50 };

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LevelOrderTraversal_ReturnsCorrectOrder() {
        var tree = new RBT_Tree<int>();
        tree.Add(50);
        tree.Add(30);
        tree.Add(70);
        tree.Add(20);
        tree.Add(40);

        var result = tree.LevelOrder().ToList();
        var expected = new List<int> { 50, 30, 70, 20, 40 };

        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CopyTo_ValidArray_CopiesElements() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);

        var array = new int[5];
        tree.CopyTo(array, 1);

        Assert.AreEqual(0, array[0]);
        Assert.IsTrue(array.Contains(10));
        Assert.IsTrue(array.Contains(20));
        Assert.IsTrue(array.Contains(30));
        Assert.AreEqual(0, array[4]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CopyTo_NullArray_ThrowsException() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.CopyTo(null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void CopyTo_NegativeIndex_ThrowsException() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        var array = new int[5];
        tree.CopyTo(array, -1);
    }

    [TestMethod]
    public void GetEnumerator_ReturnsElementsInOrder() {
        var tree = new RBT_Tree<int>();
        tree.Add(30);
        tree.Add(10);
        tree.Add(20);

        var enumerator = tree.GetEnumerator();
        var result = new List<int>();

        while (enumerator.MoveNext()) {
            result.Add(enumerator.Current);
        }

        var expected = new List<int> { 10, 20, 30 };
        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void Foreach_IteratesOverTree() {
        var tree = new RBT_Tree<int>();
        tree.Add(10);
        tree.Add(20);
        tree.Add(30);

        var result = new List<int>();
        foreach (var item in tree) {
            result.Add(item);
        }

        var expected = new List<int> { 10, 20, 30 };
        CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void IsReadOnly_ReturnsFalse() {
        var tree = new RBT_Tree<int>();
        Assert.IsFalse(tree.IsReadOnly);
    }

    [TestMethod]
    public void LargeDataSet_InsertAndRemove_WorksCorrectly() {
        var tree = new RBT_Tree<int>();
        var random = new Random();
        var inserted = new HashSet<int>();

        for (int i = 0; i < 100; i++) {
            int value = random.Next(1, 1000);
            if (inserted.Add(value)) {
                tree.Add(value);
            }
        }

        Assert.AreEqual(inserted.Count, tree.Count);

        var toRemove = inserted.Take(50).ToList();
        foreach (var value in toRemove) {
            tree.Remove(value);
            inserted.Remove(value);
            Assert.AreEqual(inserted.Count, tree.Count);
            Assert.IsFalse(tree.Contains(value));
        }

        foreach (var value in inserted) {
            Assert.IsTrue(tree.Contains(value));
        }
    }

    [TestMethod]
    public void BlackHeightProperty_MaintainedAfterMultipleOperations() {
        var tree = new RBT_Tree<int>();

        for (int i = 1; i <= 100; i++) {
            tree.Add(i);
        }

        Assert.IsTrue(CheckBlackHeight(tree.Head));

        for (int i = 1; i <= 50; i++) {
            tree.Remove(i);
        }

        Assert.IsTrue(CheckBlackHeight(tree.Head));
    }

    private bool CheckBlackHeight(NodeRBT<int> node) {
        if (node == null) return true;

        int leftHeight = GetBlackHeight(node.Left);
        int rightHeight = GetBlackHeight(node.Right);

        if (leftHeight != rightHeight) return false;

        if (node.Color == NodeColor.Red) {
            if ((node.Left != null && node.Left.Color == NodeColor.Red) ||
                (node.Right != null && node.Right.Color == NodeColor.Red)) {
                return false;
            }
        }

        return CheckBlackHeight(node.Left) && CheckBlackHeight(node.Right);
    }

    private int GetBlackHeight(NodeRBT<int> node) {
        if (node == null) return 1;

        int leftHeight = GetBlackHeight(node.Left);
        int rightHeight = GetBlackHeight(node.Right);

        if (leftHeight != rightHeight) return -1;

        return leftHeight + (node.Color == NodeColor.Black ? 1 : 0);
    }
}