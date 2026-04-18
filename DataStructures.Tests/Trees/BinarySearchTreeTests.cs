namespace DataStructures.Tests.Trees;
using DataStructures_CSharp.Trees;
[TestClass]
public class BinarySearchTreeTests {
    [TestMethod]
    public void Add_MultipleValues_ShouldBeSortedInEnumerator() {
        var tree = new BinarySearchTree<int> { 50, 30, 70, 20, 40 };

        int[] expected = { 20, 30, 40, 50, 70 };
        int[] actual = tree.ToArray();

        CollectionAssert.AreEqual(expected, actual);
        Assert.AreEqual(5, tree.Count);
    }

    [TestMethod]
    public void Contains_ExistingAndMissingElements_ReturnsCorrectBool() {
        var tree = new BinarySearchTree<int> { 10, 20, 30 };

        Assert.IsTrue(tree.Contains(20));
        Assert.IsFalse(tree.Contains(99));
    }

    [TestMethod]
    public void HeightTree_DifferentStructures_ReturnsCorrectHeight() {
        var tree = new BinarySearchTree<int>();
        Assert.AreEqual(0, tree.HeightTree());

        tree.Add(50); tree.Add(30); tree.Add(70);
        Assert.AreEqual(2, tree.HeightTree());
    }

    [TestMethod]
    public void Remove_LeafNode_UpdatesCountAndStructure() {
        var tree = new BinarySearchTree<int> { 50, 30, 70 };

        bool result = tree.Remove(30);

        Assert.IsTrue(result);
        Assert.AreEqual(2, tree.Count);
        Assert.IsFalse(tree.Contains(30));
    }

    [TestMethod]
    public void Remove_NodeWithTwoChildren_CorrectlyReplacesNode() {
        var tree = new BinarySearchTree<int> { 50, 30, 70, 60, 80 };

        tree.Remove(50);

        Assert.IsFalse(tree.Contains(50));
        Assert.AreEqual(4, tree.Count);

        Assert.IsTrue(tree.Contains(60));
        Assert.IsTrue(tree.Contains(80));
    }

    [TestMethod]
    public void GetLevelNode_VariousNodes_ReturnsCorrectLevel() {
        var tree = new BinarySearchTree<int> { 100, 50, 150, 25 };

        Assert.AreEqual(0, tree.GetLevelNode(100));
        Assert.AreEqual(2, tree.GetLevelNode(25));
        Assert.AreEqual(-1, tree.GetLevelNode(999));
    }

    [TestMethod]
    public void IsBalanced_BalancedAndUnbalancedTrees() {
        var tree = new BinarySearchTree<int> { 10, 5, 15 };
        Assert.IsTrue(tree.IsBalanced());

        tree.Add(20);
        tree.Add(25);
        tree.Add(30);
        Assert.IsFalse(tree.IsBalanced());
    }

    [TestMethod]
    public void GetMinMax_ReturnsCorrectValues() {
        var tree = new BinarySearchTree<int> { 42, 10, 88, 5 };

        Assert.AreEqual(5, tree.GetMin());
        Assert.AreEqual(88, tree.GetMax());
    }
}