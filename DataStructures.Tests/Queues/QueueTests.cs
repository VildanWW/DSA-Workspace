using DataStructures_CSharp.Queues;

namespace DataStructures_CSharp.Queues.Tests {
    [TestClass]
    public class QueueTests {
        [TestMethod]
        public void Constructor_Default_ShouldCreateEmptyQueue() {
            var queue = new Queue<int>();
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(4, queue.Capacity);
            Assert.IsTrue(queue.IsEmpty);
            Assert.IsFalse(queue.IsReadOnly);
        }

        [TestMethod]
        public void Constructor_WithCapacity_ShouldCreateQueueWithSpecifiedCapacity() {
            var queue = new Queue<string>(10);
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(10, queue.Capacity);
            Assert.IsTrue(queue.IsEmpty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Constructor_NegativeCapacity_ShouldThrowException() {
            var queue = new Queue<int>(-1);
        }

        [TestMethod]
        public void Enqueue_ShouldAddItemsToQueue() {
            var queue = new Queue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            Assert.AreEqual(3, queue.Count);
        }

        [TestMethod]
        public void Enqueue_WhenFull_ShouldResizeQueue() {
            var queue = new Queue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(4, queue.Capacity);
            Assert.AreEqual(1, queue.Peek());
        }

        [TestMethod]
        public void Enqueue_ZeroCapacity_ShouldResizeTo4() {
            var queue = new Queue<int>(0);
            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Count);
            Assert.AreEqual(4, queue.Capacity);
        }

        [TestMethod]
        public void Dequeue_ShouldRemoveAndReturnFirstItem() {
            var queue = new Queue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            var result = queue.Dequeue();
            Assert.AreEqual(10, result);
            Assert.AreEqual(2, queue.Count);
            Assert.AreEqual(20, queue.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Dequeue_EmptyQueue_ShouldThrowException() {
            var queue = new Queue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void Dequeue_ShouldHandleWrapAround() {
            var queue = new Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);
            queue.Dequeue();
            queue.Enqueue(5);
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(4, queue.Dequeue());
            Assert.AreEqual(5, queue.Dequeue());
        }

        [TestMethod]
        public void Peek_ShouldReturnFirstItemWithoutRemoving() {
            var queue = new Queue<string>();
            queue.Enqueue("first");
            queue.Enqueue("second");
            var result = queue.Peek();
            Assert.AreEqual("first", result);
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_EmptyQueue_ShouldThrowException() {
            var queue = new Queue<double>();
            queue.Peek();
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllItems() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
            Assert.IsTrue(queue.IsEmpty);
        }

        [TestMethod]
        public void Clear_EmptyQueue_ShouldDoNothing() {
            var queue = new Queue<int>();
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void Clear_ShouldResetHeadAndTail() {
            var queue = new Queue<int>(5);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Clear();
            queue.Enqueue(10);
            Assert.AreEqual(10, queue.Peek());
            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void Contains_ShouldReturnTrueWhenItemExists() {
            var queue = new Queue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            Assert.IsTrue(queue.Contains(20));
            Assert.IsTrue(queue.Contains(10));
            Assert.IsTrue(queue.Contains(30));
        }

        [TestMethod]
        public void Contains_ShouldReturnFalseWhenItemDoesNotExist() {
            var queue = new Queue<string>();
            queue.Enqueue("apple");
            queue.Enqueue("banana");
            Assert.IsFalse(queue.Contains("orange"));
            Assert.IsFalse(queue.Contains(null));
        }

        [TestMethod]
        public void Contains_ShouldHandleReferenceTypes() {
            var queue = new Queue<object>();
            var obj1 = new object();
            var obj2 = new object();
            queue.Enqueue(obj1);
            Assert.IsTrue(queue.Contains(obj1));
            Assert.IsFalse(queue.Contains(obj2));
        }

        [TestMethod]
        public void CopyTo_ShouldCopyAllItemsToArray() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var array = new int[3];
            queue.CopyTo(array, 0);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
            Assert.AreEqual(3, array[2]);
        }

        [TestMethod]
        public void CopyTo_WithOffset_ShouldCopyToCorrectPosition() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            var array = new int[5];
            queue.CopyTo(array, 2);
            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(0, array[1]);
            Assert.AreEqual(1, array[2]);
            Assert.AreEqual(2, array[3]);
            Assert.AreEqual(0, array[4]);
        }

        [TestMethod]
        public void CopyTo_WithWrapAround_ShouldCopyInCorrectOrder() {
            var queue = new Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);
            var array = new int[3];
            queue.CopyTo(array, 0);
            Assert.AreEqual(2, array[0]);
            Assert.AreEqual(3, array[1]);
            Assert.AreEqual(4, array[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ShouldThrowException() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyTo_NegativeIndex_ShouldThrowException() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            var array = new int[1];
            queue.CopyTo(array, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CopyTo_InsufficientSpace_ShouldThrowException() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            var array = new int[1];
            queue.CopyTo(array, 0);
        }

        [TestMethod]
        public void GetEnumerator_ShouldIterateOverItems() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var list = new List<int>();
            foreach (var item in queue) {
                list.Add(item);
            }
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }

        [TestMethod]
        public void GetEnumerator_WithWrapAround_ShouldIterateCorrectly() {
            var queue = new Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);
            var list = queue.ToList();
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(2, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(4, list[2]);
        }

        [TestMethod]
        public void GetEnumerator_EmptyQueue_ShouldNotIterate() {
            var queue = new Queue<int>();
            var count = 0;
            foreach (var item in queue) {
                count++;
            }
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void IEnumerable_GetEnumerator_ShouldWork() {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            System.Collections.IEnumerable enumerable = queue;
            var list = new List<int>();
            foreach (var item in enumerable) {
                list.Add((int)item);
            }
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void ComplexScenario_ShouldWorkCorrectly() {
            var queue = new Queue<string>(2);
            queue.Enqueue("A");
            queue.Enqueue("B");
            Assert.AreEqual(2, queue.Count);
            Assert.AreEqual("A", queue.Dequeue());
            queue.Enqueue("C");
            queue.Enqueue("D");
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(4, queue.Capacity);
            Assert.IsTrue(queue.Contains("B"));
            Assert.IsTrue(queue.Contains("C"));
            Assert.IsTrue(queue.Contains("D"));
            Assert.AreEqual("B", queue.Peek());
            var array = new string[3];
            queue.CopyTo(array, 0);
            Assert.AreEqual("B", array[0]);
            Assert.AreEqual("C", array[1]);
            Assert.AreEqual("D", array[2]);
            queue.Clear();
            Assert.IsTrue(queue.IsEmpty);
        }

        [TestMethod]
        public void MultipleResizes_ShouldWorkCorrectly() {
            var queue = new Queue<int>(2);
            for (int i = 0; i < 100; i++) {
                queue.Enqueue(i);
            }
            Assert.AreEqual(100, queue.Count);
            Assert.IsTrue(queue.Capacity >= 100);
            for (int i = 0; i < 50; i++) {
                Assert.AreEqual(i, queue.Dequeue());
            }
            Assert.AreEqual(50, queue.Count);
            for (int i = 100; i < 150; i++) {
                queue.Enqueue(i);
            }
            Assert.AreEqual(100, queue.Count);
        }
    }
}
