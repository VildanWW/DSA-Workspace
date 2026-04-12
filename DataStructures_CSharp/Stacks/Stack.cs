using System.Collections;

namespace DataStructures_CSharp.Stacks {
    public class Stack<T> : IEnumerable<T> {
        private T[] _massive;
        private int _size;
        private int _capacity;

        public bool IsReadOnly => false;
        public int Count => _size;
        public int Capacity => _capacity;
        public bool IsEmpty => _size == 0;

        public Stack() {
            _massive = new T[4];
            _size = 0;
            _capacity = 4;
        }

        public Stack(int capacity) {
            if (capacity < 0) throw new ArgumentOutOfRangeException("capacity < 0");
            
            _massive = new T[capacity];
            _size = 0;
            _capacity = capacity;
        }

        public void Clear() {
            Array.Clear(_massive, 0, _size);
            _size = 0;
        }

        private void ReStack() {
            int newCapacity = _capacity == 0 ? 4 : _capacity * 2;
            Array.Resize(ref _massive, newCapacity);
            _capacity = newCapacity;
        }

        public void Push(T item) {
            if(_size==Capacity) {
                ReStack();
            }

            _massive[_size++] = item;
        }

        public T Pop() {
            if (_size == 0) throw new Exception("Size == 0");

            T value = _massive[_size - 1];
            _massive[_size - 1] = default(T);
            _size--;

            return value;
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = _size - 1; i >= 0; i--) {
                yield return _massive[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public T Peek() {
            if (_size == 0) throw new Exception("Stack is empty");
            return _massive[_size - 1];
        }

        public bool Contains(T value) {
            for (int i = 0; i < _size; i++) {
                if (Equals(_massive[i], value)) return true;
            }
            return false;
        }

        public void CopyTo(T[] massive, int index) {
            if(index<0) throw new ArgumentOutOfRangeException("index<0");
            if (massive == null) throw new ArgumentNullException(nameof(massive));
            if (massive.Length - index < _size) throw new Exception("Little spaces");

            foreach (T value in this) {
                massive[index++] = value;
            }
        }
    }
}
