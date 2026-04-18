using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures_CSharp.Heaps;
using System;
using System.Linq;

namespace DataStructures.Tests.Heaps {
    [TestClass]
    public class MinHeapTests {

        [TestMethod]
        public void Constructor_Default_ShouldCreateEmptyHeap() {
            var heap = new MinHeap<int>();
            Assert.AreEqual(0, heap.Count);
            Assert.IsTrue(heap.IsEmpty);
            Assert.IsFalse(heap.IsReadOnly);
        }

        [TestMethod]
        public void Add_SingleElement_ShouldBePeek() {
            var heap = new MinHeap<int>();
            heap.Add(42);

            Assert.AreEqual(1, heap.Count);
            Assert.AreEqual(42, heap.Peek());
        }

        [TestMethod]
        public void Add_MultipleElements_ShouldKeepMinAtRoot() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            heap.Add(8);
            heap.Add(1);

            Assert.AreEqual(1, heap.Peek());
        }

        [TestMethod]
        public void Add_AfterPop_ShouldWorkCorrectly() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            heap.Pop();

            heap.Add(8);

            Assert.AreEqual(5, heap.Peek());
            Assert.AreEqual(3, heap.Count);
        }

        [TestMethod]
        public void Add_WithDuplicates_ShouldWork() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(5);
            heap.Add(5);
            heap.Add(3);

            Assert.AreEqual(3, heap.Pop());
            Assert.AreEqual(5, heap.Pop());
            Assert.AreEqual(5, heap.Pop());
            Assert.AreEqual(5, heap.Pop());
        }

        [TestMethod]
        public void Add_WithStrings_ShouldWork() {
            var heap = new MinHeap<string>();
            heap.Add("apple");
            heap.Add("banana");
            heap.Add("cherry");
            heap.Add("date");

            Assert.AreEqual("apple", heap.Pop());
            Assert.AreEqual("banana", heap.Pop());
            Assert.AreEqual("cherry", heap.Pop());
            Assert.AreEqual("date", heap.Pop());
        }

        [TestMethod]
        public void Add_LargeNumberOfElements_ShouldPerformCorrectly() {
            var heap = new MinHeap<int>();
            var input = Enumerable.Range(1, 10000).ToArray();

            foreach (var item in input) heap.Add(item);

            Assert.AreEqual(10000, heap.Count);

            for (int i = 1; i <= 10000; i++) {
                Assert.AreEqual(i, heap.Pop());
            }

            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void Pop_ShouldRemoveAndReturnMin() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            int min = heap.Pop();

            Assert.AreEqual(3, min);
            Assert.AreEqual(5, heap.Peek());
            Assert.AreEqual(2, heap.Count);
        }

        [TestMethod]
        public void Pop_UntilEmpty_ShouldReturnElementsInAscendingOrder() {
            var heap = new MinHeap<int>();
            var input = new[] { 5, 3, 8, 1, 9, 2, 7, 4, 6 };
            foreach (var item in input) heap.Add(item);

            var result = new List<int>();
            while (heap.Count > 0) result.Add(heap.Pop());

            var expected = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Pop_OnEmptyHeap_ShouldThrow() {
            var heap = new MinHeap<int>();
            heap.Pop();
        }

        [TestMethod]
        public void Peek_ShouldReturnMinWithoutRemoving() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            Assert.AreEqual(3, heap.Peek());
            Assert.AreEqual(3, heap.Count);
            Assert.AreEqual(3, heap.Peek());
            Assert.AreEqual(3, heap.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_OnEmptyHeap_ShouldThrow() {
            var heap = new MinHeap<int>();
            heap.Peek();
        }

        [TestMethod]
        public void IsEmpty_NewHeap_ShouldBeTrue() {
            var heap = new MinHeap<int>();
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void IsEmpty_AfterAdd_ShouldBeFalse() {
            var heap = new MinHeap<int>();
            heap.Add(1);
            Assert.IsFalse(heap.IsEmpty);
        }

        [TestMethod]
        public void IsEmpty_AfterPop_ShouldBeTrue() {
            var heap = new MinHeap<int>();
            heap.Add(1);
            heap.Pop();
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void IsEmpty_AfterClear_ShouldBeTrue() {
            var heap = new MinHeap<int>();
            heap.Add(1);
            heap.Add(2);
            heap.Clear();
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void Count_NewHeap_ShouldBeZero() {
            var heap = new MinHeap<int>();
            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void Count_AfterAdd_ShouldIncrease() {
            var heap = new MinHeap<int>();
            heap.Add(1);
            Assert.AreEqual(1, heap.Count);
            heap.Add(2);
            Assert.AreEqual(2, heap.Count);
            heap.Add(3);
            Assert.AreEqual(3, heap.Count);
        }

        [TestMethod]
        public void Count_AfterPop_ShouldDecrease() {
            var heap = new MinHeap<int>();
            heap.Add(1);
            heap.Add(2);
            heap.Add(3);
            heap.Pop();
            Assert.AreEqual(2, heap.Count);
            heap.Pop();
            Assert.AreEqual(1, heap.Count);
            heap.Pop();
            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void Count_AfterClear_ShouldBeZero() {
            var heap = new MinHeap<int>();
            heap.Add(1);
            heap.Add(2);
            heap.Add(3);
            heap.Clear();
            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void IsReadOnly_ShouldAlwaysBeFalse() {
            var heap = new MinHeap<int>();
            Assert.IsFalse(heap.IsReadOnly);

            heap.Add(5);
            Assert.IsFalse(heap.IsReadOnly);

            heap.Pop();
            Assert.IsFalse(heap.IsReadOnly);

            heap.Clear();
            Assert.IsFalse(heap.IsReadOnly);
        }

        [TestMethod]
        public void Clear_ShouldResetHeap() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);
            heap.Clear();

            Assert.AreEqual(0, heap.Count);
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void Clear_OnEmptyHeap_ShouldDoNothing() {
            var heap = new MinHeap<int>();
            heap.Clear();

            Assert.AreEqual(0, heap.Count);
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void Clear_ShouldAllowNewAdditions() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Clear();

            heap.Add(42);
            Assert.AreEqual(42, heap.Peek());
            Assert.AreEqual(1, heap.Count);
        }

        [TestMethod]
        public void Contains_ExistingItem_ShouldReturnTrue() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            Assert.IsTrue(heap.Contains(3));
            Assert.IsTrue(heap.Contains(5));
            Assert.IsTrue(heap.Contains(10));
        }

        [TestMethod]
        public void Contains_NonExistingItem_ShouldReturnFalse() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);

            Assert.IsFalse(heap.Contains(42));
            Assert.IsFalse(heap.Contains(0));
            Assert.IsFalse(heap.Contains(7));
        }

        [TestMethod]
        public void Contains_AfterPop_ShouldReturnFalseForPoppedItem() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            heap.Pop();

            Assert.IsFalse(heap.Contains(3));
            Assert.IsTrue(heap.Contains(5));
            Assert.IsTrue(heap.Contains(10));
        }

        [TestMethod]
        public void Contains_AfterClear_ShouldReturnFalse() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Clear();

            Assert.IsFalse(heap.Contains(5));
            Assert.IsFalse(heap.Contains(10));
        }

        [TestMethod]
        public void Contains_OnEmptyHeap_ShouldReturnFalse() {
            var heap = new MinHeap<int>();
            Assert.IsFalse(heap.Contains(42));
        }

        [TestMethod]
        public void CopyTo_ShouldCopyToArray() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            var array = new int[3];
            heap.CopyTo(array, 0);

            CollectionAssert.Contains(array, 3);
            CollectionAssert.Contains(array, 5);
            CollectionAssert.Contains(array, 10);
        }

        [TestMethod]
        public void CopyTo_WithOffset_ShouldCopyToCorrectPosition() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            var array = new int[5];
            heap.CopyTo(array, 2);

            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(0, array[1]);
            Assert.IsTrue(array[2] == 3 || array[2] == 5 || array[2] == 10);
            Assert.IsTrue(array[3] == 3 || array[3] == 5 || array[3] == 10);
            Assert.IsTrue(array[4] == 3 || array[4] == 5 || array[4] == 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ShouldThrow() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyTo_NegativeIndex_ShouldThrow() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            var array = new int[1];
            heap.CopyTo(array, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_InsufficientSpace_ShouldThrow() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);

            var array = new int[1];
            heap.CopyTo(array, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_OffsetTooLarge_ShouldThrow() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);

            var array = new int[5];
            heap.CopyTo(array, 4);
        }

        [TestMethod]
        public void GetEnumerator_ShouldIterateOverAllElements() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            heap.Add(3);

            var count = 0;
            foreach (var item in heap) {
                count++;
            }

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void GetEnumerator_ShouldContainAllElements() {
            var heap = new MinHeap<int>();
            var input = new[] { 5, 10, 3, 8, 1 };
            foreach (var item in input) heap.Add(item);

            var list = heap.ToList();

            Assert.AreEqual(5, list.Count);
            CollectionAssert.Contains(list, 1);
            CollectionAssert.Contains(list, 3);
            CollectionAssert.Contains(list, 5);
            CollectionAssert.Contains(list, 8);
            CollectionAssert.Contains(list, 10);
        }

        [TestMethod]
        public void GetEnumerator_OnEmptyHeap_ShouldIterateZeroTimes() {
            var heap = new MinHeap<int>();
            var count = 0;
            foreach (var item in heap) count++;
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void ICollectionAdd_ShouldWorkSameAsAdd() {
            var heap = new MinHeap<int>();
            ((ICollection<int>)heap).Add(42);

            Assert.AreEqual(42, heap.Peek());
            Assert.AreEqual(1, heap.Count);
        }

        [TestMethod]
        public void IEnumerable_GetEnumerator_ShouldWork() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);

            var enumerable = (System.Collections.IEnumerable)heap;
            var count = 0;
            foreach (var item in enumerable) count++;

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void Heap_WithNegativeNumbers_ShouldWork() {
            var heap = new MinHeap<int>();
            heap.Add(-5);
            heap.Add(-10);
            heap.Add(-3);
            heap.Add(-8);
            heap.Add(-1);

            Assert.AreEqual(-10, heap.Peek());
            Assert.AreEqual(-10, heap.Pop());
            Assert.AreEqual(-8, heap.Pop());
            Assert.AreEqual(-5, heap.Pop());
            Assert.AreEqual(-3, heap.Pop());
            Assert.AreEqual(-1, heap.Pop());
        }

        [TestMethod]
        public void Heap_WithZero_ShouldWork() {
            var heap = new MinHeap<int>();
            heap.Add(0);
            heap.Add(-5);
            heap.Add(5);

            Assert.AreEqual(-5, heap.Peek());
            Assert.AreEqual(-5, heap.Pop());
            Assert.AreEqual(0, heap.Pop());
            Assert.AreEqual(5, heap.Pop());
        }

        [TestMethod]
        public void AddAndPop_Alternating_ShouldMaintainCorrectOrder() {
            var heap = new MinHeap<int>();
            heap.Add(5);
            heap.Add(10);
            Assert.AreEqual(5, heap.Pop());

            heap.Add(3);
            heap.Add(8);
            Assert.AreEqual(3, heap.Pop());

            heap.Add(1);
            Assert.AreEqual(1, heap.Pop());
            Assert.AreEqual(8, heap.Pop());
            Assert.AreEqual(10, heap.Pop());
        }
    }
}