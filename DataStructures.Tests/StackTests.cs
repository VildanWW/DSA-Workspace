using DataStructures_CSharp.Stacks;

namespace DataStructures.Tests.Stacks;

[TestClass]
public class StackTests {

    [TestMethod]
    public void Push_AddElement_CountIncreases() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(10);
        Assert.AreEqual(1, stack.Count);
        Assert.IsFalse(stack.IsEmpty);
    }

    [TestMethod]
    public void Push_MultipleElements_CountCorrect() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Assert.AreEqual(3, stack.Count);
    }

    [TestMethod]
    public void Push_WhenFull_Resizes() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>(2);
        stack.Push(1);
        stack.Push(2);
        Assert.AreEqual(2, stack.Capacity);
        stack.Push(3);
        Assert.AreEqual(4, stack.Capacity);
        Assert.AreEqual(3, stack.Count);
    }

    [TestMethod]
    public void Pop_ReturnsLastElement() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        int result = stack.Pop();
        Assert.AreEqual(30, result);
        Assert.AreEqual(2, stack.Count);
    }

    [TestMethod]
    public void Pop_RemovesElement() {
        var stack = new DataStructures_CSharp.Stacks.Stack<string>();
        stack.Push("first");
        stack.Push("second");
        stack.Pop();
        Assert.IsTrue(stack.Contains("first"));
        Assert.IsFalse(stack.Contains("second"));
        Assert.AreEqual(1, stack.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Pop_EmptyStack_ThrowsException() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Pop();
    }

    [TestMethod]
    public void Peek_ReturnsLastElementWithoutRemoving() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(10);
        stack.Push(20);
        int result = stack.Peek();
        Assert.AreEqual(20, result);
        Assert.AreEqual(2, stack.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Peek_EmptyStack_ThrowsException() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Peek();
    }

    [TestMethod]
    public void Clear_RemovesAllElements() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Clear();
        Assert.AreEqual(0, stack.Count);
        Assert.IsTrue(stack.IsEmpty);
    }

    [TestMethod]
    public void Clear_ResetsSizeButNotCapacity() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        int capacity = stack.Capacity;
        stack.Clear();
        Assert.AreEqual(0, stack.Count);
        Assert.AreEqual(capacity, stack.Capacity);
    }

    [TestMethod]
    public void Contains_ElementExists_ReturnsTrue() {
        var stack = new DataStructures_CSharp.Stacks.Stack<string>();
        stack.Push("apple");
        stack.Push("banana");
        Assert.IsTrue(stack.Contains("banana"));
    }

    [TestMethod]
    public void Contains_ElementNotExists_ReturnsFalse() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        Assert.IsFalse(stack.Contains(99));
    }

    [TestMethod]
    public void Contains_EmptyStack_ReturnsFalse() {
        var stack = new DataStructures_CSharp.Stacks.Stack<double>();
        Assert.IsFalse(stack.Contains(3.14));
    }

    [TestMethod]
    public void CopyTo_CopiesElementsFromTopToBottom() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        int[] array = new int[3];
        stack.CopyTo(array, 0);

        Assert.AreEqual(3, array[0]);
        Assert.AreEqual(2, array[1]);
        Assert.AreEqual(1, array[2]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CopyTo_NullArray_ThrowsException() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.CopyTo(null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_NegativeIndex_ThrowsException() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        int[] array = new int[5];
        stack.CopyTo(array, -1);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void CopyTo_NotEnoughSpace_ThrowsException() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        int[] array = new int[2];
        stack.CopyTo(array, 1);
    }

    [TestMethod]
    public void GetEnumerator_IteratesFromTopToBottom() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        var result = new List<int>();
        foreach (var item in stack) {
            result.Add(item);
        }

        CollectionAssert.AreEqual(new[] { 30, 20, 10 }, result);
    }

    [TestMethod]
    public void Constructor_Default_CapacityIs4() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        Assert.AreEqual(4, stack.Capacity);
        Assert.AreEqual(0, stack.Count);
    }

    [TestMethod]
    public void Constructor_WithCapacity_SetsCapacity() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>(10);
        Assert.AreEqual(10, stack.Capacity);
        Assert.AreEqual(0, stack.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void Constructor_NegativeCapacity_ThrowsException() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>(-5);
    }

    [TestMethod]
    public void IsReadOnly_ReturnsFalse() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        Assert.IsFalse(stack.IsReadOnly);
    }

    [TestMethod]
    public void Count_AfterMultiplePushesAndPops_Correct() {
        var stack = new DataStructures_CSharp.Stacks.Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Pop();
        stack.Push(3);
        stack.Push(4);
        stack.Pop();
        Assert.AreEqual(2, stack.Count);
    }

    [TestMethod]
    public void Contains_WithNullValue_WorksCorrectly() {
        var stack = new DataStructures_CSharp.Stacks.Stack<string>();
        stack.Push("hello");
        stack.Push(null);
        stack.Push("world");
        Assert.IsTrue(stack.Contains(null));
        Assert.IsTrue(stack.Contains("world"));
    }
}