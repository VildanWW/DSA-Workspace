using DataStructures_CSharp.Sorting;

namespace DataStructures.Tests;

[TestClass]
public class SelectionSortTests {
    [TestMethod]
    public void Sort_RandomArray_ShouldBeSorted() {
        List<int> ints1 = new List<int> { 5, 1, 4, 2, 8 };
        List<int> ints2 = new List<int> { 1, 2, 4, 5, 8 };

        SelectionSort.Sort(ints1, (a, b) => a > b);

        CollectionAssert.AreEqual(ints1, ints2, "ints1[i] != ints2[i]");
    }

    [TestMethod]
    public void Sort_AlreadySorted_ShouldBeSorted() {
        List<int> ints1 = new List<int> { 1, 2, 3, 4, 5 };
        List<int> ints2 = new List<int> { 1, 2, 3, 4, 5 };

        BubbleSort.Sort(ints1, (a, b) => a > b);

        CollectionAssert.AreEqual(ints1, ints2);
    }

    [TestMethod]
    public void Sort_ReverseArray_ShouldBeSorted() {
        List<int> ints1 = new List<int> { 5, 4, 3, 2, 1 };
        List<int> ints2 = new List<int> { 1, 2, 3, 4, 5 };

        BubbleSort.Sort(ints1, (a, b) => a > b);

        CollectionAssert.AreEqual(ints1, ints2);
    }

    [TestMethod]
    public void Sort_HeavyLoad_Validation() {

        int size = 100_000;
        Random rand = new Random(777);

        List<int> ints1 = new List<int>(size);
        for (int i = 0; i < size; i++) ints1.Add(rand.Next());

        List<int> ints2 = new List<int>(ints1);

        ints2.Sort();
        ints2.Reverse();

        BubbleSort.Sort(ints1, (a, b) => a < b);

        for (int i = 0; i < size; i++) {
            if (ints1[i] != ints2[i]) Assert.Fail($"Error in index {i}");
        }
    }
}
