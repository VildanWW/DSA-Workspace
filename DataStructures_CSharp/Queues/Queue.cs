using System.Collections;

namespace DataStructures_CSharp.Queues {
    public class Queue<T> : IEnumerable<T> {
        private T[] _array;
        private int _head;
        private int _tail;
        private int _size;
        private int _capacity;

        public int Count => _size;
        public int Capacity => _capacity;
        public bool IsEmpty => _size == 0;
        public bool IsReadOnly => false;

        public Queue() {
            _array = new T[4];
            _capacity = 4;
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public Queue(int capacity) {
            if (capacity < 0) throw new Exception("capacity<0");
            _capacity = capacity;
            _head = 0;
            _tail = 0;
            _size = 0;
            _array = new T[capacity];
        }

        public T Dequeue() {
            if (_size == 0) throw new Exception("Queue is empty");

            T value = _array[_head];
            _array[_head] = default(T);
            _head = (_head + 1) % _capacity;
            _size--;

            return value;
        }

        private void ReQueue() {
            int newCapacity = _capacity == 0 ? 4 : _capacity * 2;
            T[] newArray = new T[newCapacity];

            for(int i=0;i<_size;i++) {
                newArray[i] = _array[(_head + i) % _capacity];
            }

            _array = newArray;
            _head = 0;
            _tail = _size;
            _capacity = newCapacity;
        }

        public void Enqueue(T item) {
            if(_size==_capacity) {
                ReQueue();
            }

            _array[_tail] = item;
            _tail = (_tail + 1) % _capacity;
            _size++;
        }

        public T Peek() {
            if (_size == 0) throw new InvalidOperationException("Queue is empty");

            return _array[_head];
        }

        public void Clear() {
            if (_size == 0) return;

            for(int i=0;i<_size;i++) {
                _array[(_head + i) % _capacity] = default(T);
            }

            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public bool Contains(T value) {
            for (int i = 0; i < _size; i++) {
                if (Equals(_array[(_head+i)%_capacity], value)) return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (array.Length - arrayIndex < Count) throw new Exception("little space");
            if (arrayIndex < 0) throw new IndexOutOfRangeException("index < 0");

            for(int i=0;i<_size;i++) {
                array[arrayIndex++] = _array[(_head + i) % _capacity];
            }
        }

        public IEnumerator<T> GetEnumerator() {
            for(int i=0;i<_size;i++) {
                yield return _array[(_head + i) % _capacity];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
