using System.Collections;

namespace DataStructures_CSharp.Heaps {
    public class MinHeap<T> : ICollection<T>, IEnumerable<T> where T : IComparable<T> {
        private const int baseCapacity = 4;
        T[] values;

        public bool IsEmpty => Count == 0;
        public bool IsReadOnly => false;

        public int Count { get; private set; }

        public MinHeap() {
            values = new T[baseCapacity];
            Count = 0;
        }

        public void Add(T value) {
            if (Count == values.Length) {
                Array.Resize(ref values, values.Length * 2);
            }

            values[Count] = value;

            UpElement(Count);

            Count++;
        }

        private void UpElement(int index) {
            while (index > 0) {
                int parentIndex = (index - 1) / 2;

                if (values[index].CompareTo(values[parentIndex]) >= 0) {
                    break;
                }

                (values[index], values[parentIndex]) = (values[parentIndex], values[index]);

                index = parentIndex;
            }
        }

        public void PrintAsTree() {
            PrintAsTree(0, "");
        }

        private void PrintAsTree(int index, string space) {
            if (index >= Count) return;

            PrintAsTree(2 * index + 1, space + "    ");

            Console.WriteLine(space + values[index]);

            PrintAsTree(2 * index + 2, space + "    ");
        }

        public T Pop() {
            if (Count == 0) throw new IndexOutOfRangeException("Heap is empty");

            T result = values[0];
            values[0] = values[Count - 1];
            values[Count - 1] = default;
            Count--;

            DownElement(0);
            return result;
        }

        private void DownElement(int index) {
            while (true) {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;

                if (left < Count && values[left].CompareTo(values[largest]) < 0) {
                    largest = left;
                }

                if (right < Count && values[right].CompareTo(values[largest]) < 0) {
                    largest = right;
                }

                if (largest == index) break;

                (values[index], values[largest]) = (values[largest], values[index]);
                index = largest;
            }
        }

        public T Peek() {
            if (Count == 0) throw new InvalidOperationException("Heap is empty");
            return values[0];
        }

        public void Clear() {
            Count = 0;
            Array.Clear(values);
            Array.Resize(ref values, baseCapacity);
        }

        public bool Contains(T item) {
            for (int i = 0; i < Count; i++) {
                if (item.Equals(values[i])) return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < Count; i++) {
                yield return values[i];
            }
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException("array is null");
            if (arrayIndex < 0) throw new IndexOutOfRangeException("index < 0");
            if (array.Length - arrayIndex < Count) throw new ArgumentException("little spaces");

            foreach (var item in this) {
                array[arrayIndex++] = item;
            }
        }

        [Obsolete("Removing arbitrary elements is not supported in a heap. Use Pop() instead.", error: true)]
        #pragma warning disable CS0618
        #pragma warning restore CS0618
        public bool Remove(T item) => throw new NotSupportedException(
            "Removing arbitrary elements is not supported in a heap data structure. " +
            "Use Pop() to remove the maximum element."
        );

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
