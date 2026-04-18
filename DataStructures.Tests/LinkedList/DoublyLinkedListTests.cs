using DataStructures_CSharp.Lists;

namespace DataStructures.Tests.LinkedList;

[TestClass]
public class DoublyLinkedListTests {
    #region Constructor and Initialization Tests

    [TestMethod]
    public void Constructor_ShouldCreateEmptyList() {
        var list = new DoublyLinkedList<int>();

        Assert.AreEqual(0, list.Count);
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
        Assert.IsFalse(list.IsReadOnly);
    }

    #endregion

    #region Add Tests

    [TestMethod]
    public void Add_ShouldAddElementToEmptyList() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);

        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(10, list.Tail.Value);
        Assert.AreSame(list.Head, list.Tail);
    }

    [TestMethod]
    public void Add_ShouldAddMultipleElements() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(30, list.Tail.Value);
        Assert.AreEqual(20, list.Head.Next.Value);
        Assert.AreEqual(20, list.Tail.Prev.Value);
    }

    [TestMethod]
    public void Add_ShouldMaintainCorrectLinks() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        Assert.AreEqual(20, list.Head.Next.Value);
        Assert.AreEqual(30, list.Head.Next.Next.Value);
        Assert.IsNull(list.Head.Next.Next.Next);

        Assert.AreEqual(20, list.Tail.Prev.Value);
        Assert.AreEqual(10, list.Tail.Prev.Prev.Value);
        Assert.IsNull(list.Tail.Prev.Prev.Prev);
    }

    #endregion

    #region AddFirstElement Tests

    [TestMethod]
    public void AddFirstElement_ShouldAddToEmptyList() {
        var list = new DoublyLinkedList<int>();
        list.AddFirstElement(10);

        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(10, list.Tail.Value);
    }

    [TestMethod]
    public void AddFirstElement_ShouldAddToBeginning() {
        var list = new DoublyLinkedList<int>();
        list.Add(20);
        list.Add(30);
        list.AddFirstElement(10);

        Assert.AreEqual(3, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(20, list.Head.Next.Value);
        Assert.AreEqual(30, list.Head.Next.Next.Value);
        Assert.IsNull(list.Head.Prev);
    }

    #endregion

    #region RemoveFirst Tests

    [TestMethod]
    public void RemoveFirst_ShouldRemoveOnlyElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.RemoveFirst();

        Assert.AreEqual(0, list.Count);
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
    }

    [TestMethod]
    public void RemoveFirst_ShouldRemoveFirstElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.RemoveFirst();

        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(20, list.Head.Value);
        Assert.AreEqual(30, list.Tail.Value);
        Assert.IsNull(list.Head.Prev);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void RemoveFirst_OnEmptyList_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.RemoveFirst();
    }

    #endregion

    #region RemoveLast Tests

    [TestMethod]
    public void RemoveLast_ShouldRemoveOnlyElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.RemoveLast();

        Assert.AreEqual(0, list.Count);
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
    }

    [TestMethod]
    public void RemoveLast_ShouldRemoveLastElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.RemoveLast();

        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(20, list.Tail.Value);
        Assert.IsNull(list.Tail.Next);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void RemoveLast_OnEmptyList_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.RemoveLast();
    }

    #endregion

    #region RemoveAt Tests

    [TestMethod]
    public void RemoveAt_ShouldRemoveFirstElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.RemoveAt(0);

        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(20, list.Head.Value);
        Assert.AreEqual(30, list.Tail.Value);
    }

    [TestMethod]
    public void RemoveAt_ShouldRemoveLastElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.RemoveAt(2);

        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(20, list.Tail.Value);
    }

    [TestMethod]
    public void RemoveAt_ShouldRemoveMiddleElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Add(40);
        list.Add(50);
        list.RemoveAt(2);

        Assert.AreEqual(4, list.Count);
        var elements = list.ToList();
        CollectionAssert.AreEqual(new[] { 10, 20, 40, 50 }, elements);
    }

    [TestMethod]
    public void RemoveAt_ShouldWorkWithLargeListAndMiddleIndex() {
        var list = new DoublyLinkedList<int>();
        for (int i = 0; i < 100; i++) {
            list.Add(i);
        }

        list.RemoveAt(50);

        Assert.AreEqual(99, list.Count);
        Assert.AreEqual(49, list.FindElement(49));
        Assert.AreEqual(51, list.FindElement(50));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void RemoveAt_WithInvalidIndex_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.RemoveAt(5);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void RemoveAt_OnEmptyList_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.RemoveAt(0);
    }

    #endregion

    #region Remove Tests

    [TestMethod]
    public void Remove_ShouldRemoveExistingElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        bool result = list.Remove(20);

        Assert.IsTrue(result);
        Assert.AreEqual(2, list.Count);
        CollectionAssert.AreEqual(new[] { 10, 30 }, list.ToList());
    }

    [TestMethod]
    public void Remove_ShouldRemoveFirstElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        bool result = list.Remove(10);

        Assert.IsTrue(result);
        Assert.AreEqual(2, list.Count);
        CollectionAssert.AreEqual(new[] { 20, 30 }, list.ToList());
    }

    [TestMethod]
    public void Remove_ShouldRemoveLastElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        bool result = list.Remove(30);

        Assert.IsTrue(result);
        Assert.AreEqual(2, list.Count);
        CollectionAssert.AreEqual(new[] { 10, 20 }, list.ToList());
    }

    [TestMethod]
    public void Remove_ShouldReturnFalseForNonExistingElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        bool result = list.Remove(99);

        Assert.IsFalse(result);
        Assert.AreEqual(2, list.Count);
    }

    [TestMethod]
    public void Remove_ShouldRemoveOnlyFirstOccurrence() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(20);
        list.Add(30);
        bool result = list.Remove(20);

        Assert.IsTrue(result);
        Assert.AreEqual(3, list.Count);
        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, list.ToList());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Remove_OnEmptyList_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Remove(10);
    }

    #endregion

    #region Insert Tests

    [TestMethod]
    public void Insert_AtBeginning_ShouldInsertCorrectly() {
        var list = new DoublyLinkedList<int>();
        list.Add(20);
        list.Add(30);
        list.Insert(0, 10);

        Assert.AreEqual(3, list.Count);
        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, list.ToList());
        Assert.IsNull(list.Head.Prev);
        Assert.AreEqual(20, list.Head.Next.Value);
    }

    [TestMethod]
    public void Insert_AtEnd_ShouldInsertCorrectly() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Insert(2, 30);

        Assert.AreEqual(3, list.Count);
        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, list.ToList());
        Assert.IsNull(list.Tail.Next);
        Assert.AreEqual(20, list.Tail.Prev.Value);
    }

    [TestMethod]
    public void Insert_InMiddle_ShouldInsertCorrectly() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(30);
        list.Add(40);
        list.Insert(1, 20);

        Assert.AreEqual(4, list.Count);
        CollectionAssert.AreEqual(new[] { 10, 20, 30, 40 }, list.ToList());
        Assert.AreEqual(20, list.Head.Next.Value);
        Assert.AreEqual(30, list.Head.Next.Next.Value);
    }

    [TestMethod]
    public void Insert_IntoEmptyList_ShouldWork() {
        var list = new DoublyLinkedList<int>();
        list.Insert(0, 10);

        Assert.AreEqual(1, list.Count);
        Assert.AreEqual(10, list.Head.Value);
        Assert.AreEqual(10, list.Tail.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void Insert_WithInvalidIndex_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Insert(5, 20);
    }

    #endregion

    #region FindElement and Indexer Tests

    [TestMethod]
    public void FindElement_ShouldReturnCorrectElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Add(40);
        list.Add(50);

        Assert.AreEqual(10, list.FindElement(0));
        Assert.AreEqual(30, list.FindElement(2));
        Assert.AreEqual(50, list.FindElement(4));
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void FindElement_WithInvalidIndex_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.FindElement(5);
    }

    [TestMethod]
    public void Indexer_Get_ShouldReturnCorrectElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        Assert.AreEqual(10, list[0]);
        Assert.AreEqual(20, list[1]);
        Assert.AreEqual(30, list[2]);
    }

    [TestMethod]
    public void Indexer_Set_ShouldUpdateElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list[1] = 99;

        Assert.AreEqual(99, list[1]);
        CollectionAssert.AreEqual(new[] { 10, 99, 30 }, list.ToList());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Indexer_Set_WithInvalidIndex_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list[5] = 99;
    }

    #endregion

    #region Contains Tests

    [TestMethod]
    public void Contains_ShouldReturnTrueForExistingElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        Assert.IsTrue(list.Contains(20));
        Assert.IsTrue(list.Contains(10));
        Assert.IsTrue(list.Contains(30));
    }

    [TestMethod]
    public void Contains_ShouldReturnFalseForNonExistingElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);

        Assert.IsFalse(list.Contains(99));
    }

    [TestMethod]
    public void Contains_OnEmptyList_ShouldReturnFalse() {
        var list = new DoublyLinkedList<int>();

        Assert.IsFalse(list.Contains(10));
    }

    #endregion

    #region IndexOf Tests

    [TestMethod]
    public void IndexOf_ShouldReturnCorrectIndex() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Add(20);

        Assert.AreEqual(0, list.IndexOf(10));
        Assert.AreEqual(1, list.IndexOf(20));
        Assert.AreEqual(2, list.IndexOf(30));
    }

    [TestMethod]
    public void IndexOf_ShouldReturnMinusOneForNonExistingElement() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);

        Assert.AreEqual(-1, list.IndexOf(99));
    }

    #endregion

    #region Clear Tests

    [TestMethod]
    public void Clear_ShouldRemoveAllElements() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Clear();

        Assert.AreEqual(0, list.Count);
        Assert.IsNull(list.Head);
        Assert.IsNull(list.Tail);
    }

    [TestMethod]
    public void Clear_OnEmptyList_ShouldDoNothing() {
        var list = new DoublyLinkedList<int>();
        list.Clear();

        Assert.AreEqual(0, list.Count);
    }

    #endregion

    #region CopyTo Tests

    [TestMethod]
    public void CopyTo_ShouldCopyAllElements() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        var array = new int[3];
        list.CopyTo(array, 0);

        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, array);
    }

    [TestMethod]
    public void CopyTo_WithOffset_ShouldCopyToCorrectPosition() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        var array = new int[5] { 0, 0, 0, 0, 0 };
        list.CopyTo(array, 2);

        CollectionAssert.AreEqual(new[] { 0, 0, 10, 20, 0 }, array);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CopyTo_WithNullArray_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.CopyTo(null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CopyTo_WithInsufficientSpace_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        var array = new int[1];
        list.CopyTo(array, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void CopyTo_WithNegativeIndex_ShouldThrowException() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        var array = new int[1];
        list.CopyTo(array, -1);
    }

    #endregion

    #region GetEnumerator Tests

    [TestMethod]
    public void GetEnumerator_ShouldIterateThroughAllElements() {
        var list = new DoublyLinkedList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        var result = new List<int>();

        foreach (var item in list) {
            result.Add(item);
        }

        CollectionAssert.AreEqual(new[] { 10, 20, 30 }, result);
    }

    [TestMethod]
    public void GetEnumerator_OnEmptyList_ShouldIterateNothing() {
        var list = new DoublyLinkedList<int>();
        var result = new List<int>();

        foreach (var item in list) {
            result.Add(item);
        }

        Assert.AreEqual(0, result.Count);
    }

    #endregion

    #region Integration Tests

    [TestMethod]
    public void ComplexScenario_ShouldWorkCorrectly() {
        var list = new DoublyLinkedList<int>();

        list.Add(10);
        list.Add(20);
        list.Add(30);
        Assert.AreEqual(3, list.Count);

        list.Insert(1, 15);
        CollectionAssert.AreEqual(new[] { 10, 15, 20, 30 }, list.ToList());

        list.Remove(20);
        CollectionAssert.AreEqual(new[] { 10, 15, 30 }, list.ToList());

        list.RemoveAt(1);
        CollectionAssert.AreEqual(new[] { 10, 30 }, list.ToList());

        list.AddFirstElement(5);
        CollectionAssert.AreEqual(new[] { 5, 10, 30 }, list.ToList());

        list.RemoveFirst();
        list.RemoveLast();
        CollectionAssert.AreEqual(new[] { 10 }, list.ToList());

        list.Clear();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void LargeListOperations_ShouldPerformCorrectly() {
        var list = new DoublyLinkedList<int>();
        const int size = 1000;

        for (int i = 0; i < size; i++) {
            list.Add(i);
        }

        Assert.AreEqual(size, list.Count);
        Assert.AreEqual(0, list.Head.Value);
        Assert.AreEqual(size - 1, list.Tail.Value);

        int removeIndex = size / 2;
        list.RemoveAt(removeIndex);

        Assert.AreEqual(size - 1, list.Count);
        Assert.AreEqual(removeIndex + 1, list[removeIndex]);
        Assert.AreEqual(0, list[0]);
        Assert.AreEqual(size - 1, list[size - 2]);
        Assert.IsFalse(list.Contains(removeIndex));

        list.RemoveFirst();
        list.RemoveLast();

        Assert.AreEqual(size - 3, list.Count);
        Assert.AreEqual(1, list.Head.Value);
        Assert.AreEqual(size - 2, list.Tail.Value);
    }

    #endregion

    #region Edge Cases Tests

    [TestMethod]
    public void OperationsWithReferenceTypes_ShouldWork() {
        var list = new DoublyLinkedList<string>();
        list.Add("Apple");
        list.Add("Banana");
        list.Add("Cherry");

        Assert.IsTrue(list.Contains("Banana"));
        Assert.AreEqual(1, list.IndexOf("Banana"));
        list.Remove("Banana");
        CollectionAssert.AreEqual(new[] { "Apple", "Cherry" }, list.ToList());
    }

    [TestMethod]
    public void OperationsWithNullableTypes_ShouldWork() {
        var list = new DoublyLinkedList<int?>();
        list.Add(10);
        list.Add(null);
        list.Add(30);

        Assert.IsTrue(list.Contains(null));
        Assert.AreEqual(1, list.IndexOf(null));
        list.Remove(null);
        CollectionAssert.AreEqual(new int?[] { 10, 30 }, list.ToList());
    }

    #endregion
}