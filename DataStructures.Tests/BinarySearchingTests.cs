using DataStructures_CSharp.Searching;

namespace DataStructures.Tests.Searching;

[TestClass]
public class BinarySearchingTests {

    [TestMethod]
    public void Find_ElementExists_ReturnsIndex() {
        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int index = BinarySearching.Find(arr, 5);
        Assert.AreEqual(4, index);
    }

    [TestMethod]
    public void Find_ElementExists_FirstElement_ReturnsIndex() {
        int[] arr = { 1, 2, 3, 4, 5 };
        int index = BinarySearching.Find(arr, 1);
        Assert.AreEqual(0, index);
    }

    [TestMethod]
    public void Find_ElementExists_LastElement_ReturnsIndex() {
        int[] arr = { 1, 2, 3, 4, 5 };
        int index = BinarySearching.Find(arr, 5);
        Assert.AreEqual(4, index);
    }

    [TestMethod]
    public void Find_ElementDoesNotExist_ReturnsMinusOne() {
        int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int index = BinarySearching.Find(arr, 99);
        Assert.AreEqual(-1, index);
    }

    [TestMethod]
    public void Find_EmptyArray_ReturnsMinusOne() {
        int[] arr = new int[0];
        int index = BinarySearching.Find(arr, 5);
        Assert.AreEqual(-1, index);
    }

    [TestMethod]
    public void Find_NullArray_ReturnsMinusOne() {
        int[] arr = null;
        int index = BinarySearching.Find(arr, 5);
        Assert.AreEqual(-1, index);
    }

    [TestMethod]
    public void Find_SingleElement_Exists_ReturnsIndex() {
        int[] arr = { 42 };
        int index = BinarySearching.Find(arr, 42);
        Assert.AreEqual(0, index);
    }

    [TestMethod]
    public void Find_SingleElement_NotExists_ReturnsMinusOne() {
        int[] arr = { 42 };
        int index = BinarySearching.Find(arr, 99);
        Assert.AreEqual(-1, index);
    }

    [TestMethod]
    public void Find_TwoElements_First_ReturnsIndex() {
        int[] arr = { 10, 20 };
        int index = BinarySearching.Find(arr, 10);
        Assert.AreEqual(0, index);
    }

    [TestMethod]
    public void Find_TwoElements_Second_ReturnsIndex() {
        int[] arr = { 10, 20 };
        int index = BinarySearching.Find(arr, 20);
        Assert.AreEqual(1, index);
    }

    [TestMethod]
    public void Find_TwoElements_NotExists_ReturnsMinusOne() {
        int[] arr = { 10, 20 };
        int index = BinarySearching.Find(arr, 15);
        Assert.AreEqual(-1, index);
    }

    [TestMethod]
    public void Find_List_ElementExists_ReturnsIndex() {
        List<int> list = new List<int> { 10, 20, 30, 40, 50 };
        int index = BinarySearching.Find(list, 30);
        Assert.AreEqual(2, index);
    }

    [TestMethod]
    public void Find_StringArray_ElementExists_ReturnsIndex() {
        string[] arr = { "apple", "banana", "cherry", "date" };
        int index = BinarySearching.Find(arr, "cherry");
        Assert.AreEqual(2, index);
    }

    [TestMethod]
    public void Find_DoubleArray_ElementExists_ReturnsIndex() {
        double[] arr = { 1.1, 2.2, 3.3, 4.4, 5.5 };
        int index = BinarySearching.Find(arr, 3.3);
        Assert.AreEqual(2, index);
    }

    [TestMethod]
    public void Find_LargeArray_ElementExists_ReturnsIndex() {
        int[] arr = new int[10000];
        for (int i = 0; i < arr.Length; i++) arr[i] = i;

        int index = BinarySearching.Find(arr, 5000);
        Assert.AreEqual(5000, index);
    }

    [TestMethod]
    public void Find_LargeArray_ElementNotExists_ReturnsMinusOne() {
        int[] arr = new int[10000];
        for (int i = 0; i < arr.Length; i++) arr[i] = i;

        int index = BinarySearching.Find(arr, 10000);
        Assert.AreEqual(-1, index);
    }
}