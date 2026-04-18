using DataStructures_CSharp.Lists;
namespace DataStructures.Tests.LinkedList {
    [TestClass]
    public sealed class SinglyLinkedTests {
        [TestMethod]
        public void Add_OneElement_IncrementsCount() {
            var list = new SinglyLinkedList<int>();

            list.Add(9999);

            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.Contains(9999));
        }

        [TestMethod]
        public void ComplexOperations_TwoListsShouldMatch() {
            var ints1 = new SinglyLinkedList<int>();
            var ints2 = new SinglyLinkedList<int>();
            var random = new Random();

            for(int i=0;i<500;i++) {
                int value = random.Next(1000);
                ints1.Add(value);
                ints2.Add(value);
            }

            ints1.RemoveFirst(); ints1.RemoveLast(); ints1.Add(823);
            ints2.RemoveFirst(); ints2.RemoveLast(); ints2.Add(823);

            Assert.AreEqual(ints1.Count, ints2.Count);
            for (int i = 0; i < ints1.Count; i++) {
                Assert.AreEqual(ints1[i], ints2[i], $"Error in index {i}");
            }
        }

        [TestMethod]
        public void CopyTo_ShouldOffsetArrayCorrectly() {
            var list = new SinglyLinkedList<int> { 10, 20, 30 };
            int[] testArray = new int[4];

            list.CopyTo(testArray, 1);

            Assert.AreEqual(10, testArray[1]);
            Assert.AreEqual(20, testArray[2]);
            Assert.AreEqual(30, testArray[3]);
        }

        [TestMethod]
        public void RemoveAt_LastElement_ShouldResetList() {
            var list = new SinglyLinkedList<int> { 55 };
            list.RemoveAt(0);

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Indexer_Set_UpdatesValue() {
            var list = new SinglyLinkedList<int> { 100 };
            list[0] = 200;

            Assert.AreEqual(200, list[0]);
        }
    }
}
