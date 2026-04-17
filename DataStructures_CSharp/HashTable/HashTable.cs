using System.Collections;

namespace DataStructures_CSharp.HashTable {
    public class HashTable<KeyType, ValueType> : ICollection<KeyValuePair<KeyType, ValueType>>, IEnumerable<KeyValuePair<KeyType, ValueType>>  {
        private const int DefaultCapacity = 16;
        private const double DefaultLoadFactor = 0.75;

        private int _hashSize;
        private Func<KeyType, int, int> _hashFunction;
        private Node<KeyType, ValueType>[] _hashMassive;
        private int _countInsert;

        public int HashSize => _hashSize;

        public int Count => _countInsert;

        public bool IsReadOnly => false;

        public HashTable() {
            _hashSize = DefaultCapacity;
            _countInsert = 0;

            _hashMassive = new Node<KeyType, ValueType>[DefaultCapacity];

            _hashFunction = (key, size) => HashFunctions.Fnv1aHash(key, size);
        }

        public HashTable(int hashSize, Func<KeyType, int, int> hashFunction) {
            if (hashSize <= 0) throw new ArgumentException("hashSize<=0");
            _hashSize = hashSize;
            _hashFunction = hashFunction;
            _countInsert = 0;

            _hashMassive = new Node<KeyType, ValueType>[hashSize];
        }

        private bool TryUpdate(KeyType key, ValueType value) {
            int index = _hashFunction(key, _hashSize);
            var current = _hashMassive[index];

            while(current != null) {
                if(EqualityComparer<KeyType>.Default.Equals(current.Key, key)) {
                    current.Value = value;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        private void ReHash() {

            int newCapacity = _hashSize * 2;
            var newBuckets = new Node<KeyType, ValueType>[newCapacity];

            for(int i=0;i<_hashSize;i++) {
                var current = _hashMassive[i];
                while(current!=null) {
                    int newIndex = _hashFunction(current.Key, newCapacity);
                    
                    var nextInOldList = current.Next;

                    current.Next = newBuckets[newIndex];
                    newBuckets[newIndex] = current;

                    current = nextInOldList;
                }
            }
            _hashMassive = newBuckets;
            _hashSize = _hashSize * 2;
        }

        public void Add(KeyType key, ValueType value) {
            if (TryUpdate(key, value)) {
                return;
            }

            if(GetLoadFactor()>=DefaultLoadFactor) {
                ReHash();
            }

            int index = _hashFunction(key, _hashSize);
            _countInsert++;
            var newNode = new Node<KeyType, ValueType>(key, value);
            newNode.Next = _hashMassive[index];
            _hashMassive[index] = newNode;
        }

        public double GetLoadFactor() {
            return (double)_countInsert / _hashSize;
        }

        public bool Find(KeyType key) {
            int index = _hashFunction(key, _hashSize);
            var current = _hashMassive[index];

            while (current != null) {
                if (EqualityComparer<KeyType>.Default.Equals(current.Key, key)) {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public int GetEmptyBuckets() {
            int count = 0;
            for (int i = 0; i < _hashSize; i++) {
                if (_hashMassive[i] == null) count++;
            }
            return count;
        }

        public IEnumerator<KeyValuePair<KeyType, ValueType>> GetEnumerator() {
            for(int i=0;i<_hashSize;i++) {
                var current = _hashMassive[i];

                while(current!=null) {
                    yield return new KeyValuePair<KeyType, ValueType>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public bool Remove(KeyType key) {
            int index = _hashFunction(key, _hashSize);
            var current = _hashMassive[index];

            if (current == null) return false;

            if (EqualityComparer<KeyType>.Default.Equals(current.Key, key)) {
                _hashMassive[index] = current.Next;
                _countInsert--;
                return true;
            }

            while (current.Next != null) {
                if (EqualityComparer<KeyType>.Default.Equals(current.Next.Key, key)) {
                    current.Next = current.Next.Next;
                    _countInsert--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void ShowTable() {
            for (int i = 0; i < _hashSize; i++) {
                Console.Write($"{i}: ");
                var current = _hashMassive[i];

                while (current != null) {
                    Console.Write($"[Key:{current.Key}, Val:{current.Value}]");
                    current = current.Next;
                }
                Console.WriteLine();
            }
        }

        public void Clear() {
            for(int i=0;i<_hashSize;i++) {
                _hashMassive[i] = null;
            }
            _countInsert = 0;
        }

        public void Add(KeyValuePair<KeyType, ValueType> item) {
            Add(item.Key, item.Value);
        }

        public bool TryGetValue(KeyType key, out ValueType value) {
            int index = _hashFunction(key, _hashSize);
            var current = _hashMassive[index];

            while (current != null) {
                if (EqualityComparer<KeyType>.Default.Equals(key, current.Key)) {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
            }
            value = default;
            return false;
        }

        public bool Contains(KeyValuePair<KeyType, ValueType> item) {
            if (TryGetValue(item.Key, out var value)) {
                return EqualityComparer<ValueType>.Default.Equals(value, item.Value);
            }
            return false;
        }

        public void CopyTo(KeyValuePair<KeyType, ValueType>[] array, int arrayIndex) {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _countInsert) throw new ArgumentException("Little spaces");
            
            foreach(var pair in this) {
                array[arrayIndex++] = pair;
            }
        }

        public bool Remove(KeyValuePair<KeyType, ValueType> item) {
            if (Contains(item)) {
                return Remove(item.Key);
            }
            return false;
        }

        public ValueType this[KeyType key] {
            get => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException("Not found key");
            set => Add(key, value);
        }
    }
}
