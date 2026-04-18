using DataStructures_CSharp.Sorting;

namespace DataStructures.Tests.Sorting {
    [TestClass]
    public class HeapSortTests {

        [TestMethod]
        public void SortIncrease_ShouldSortIntArray() {
            var list = new List<int> { 5, 3, 8, 1, 9, 2, 7, 4, 6 };
            list.SortIncrease();

            var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void SortDecrease_ShouldSortIntArrayDescending() {
            var list = new List<int> { 5, 3, 8, 1, 9, 2, 7, 4, 6 };
            list.SortDecrease();

            var expected = new List<int> { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void SortIncrease_WithEmptyList_ShouldDoNothing() {
            var list = new List<int>();
            list.SortIncrease();

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void SortIncrease_WithSingleElement_ShouldDoNothing() {
            var list = new List<int> { 42 };
            list.SortIncrease();

            var expected = new List<int> { 42 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void SortIncrease_WithAlreadySorted_ShouldRemainSorted() {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            list.SortIncrease();

            var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void SortIncrease_WithDuplicates_ShouldHandleDuplicates() {
            var list = new List<int> { 5, 3, 8, 3, 9, 2, 7, 5, 6 };
            list.SortIncrease();

            var expected = new List<int> { 2, 3, 3, 5, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void SortIncrease_WithStrings_ShouldSortAlphabetically() {
            var list = new List<string> { "banana", "apple", "cherry", "date" };
            list.SortIncrease();

            var expected = new List<string> { "apple", "banana", "cherry", "date" };
            CollectionAssert.AreEqual(expected, list);
        }

        [TestMethod]
        public void SortIncrease_WithNegativeNumbers_ShouldSortCorrectly() {
            var list = new List<int> { -5, 3, -8, 1, 0, -2, 7 };
            list.SortIncrease();

            var expected = new List<int> { -8, -5, -2, 0, 1, 3, 7 };
            CollectionAssert.AreEqual(expected, list);
        }
    }
}
