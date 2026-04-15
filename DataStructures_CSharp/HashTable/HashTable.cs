
namespace DataStructures_CSharp.HashTable {
    public class HashTable<KeyType, ValueType> {
        private const int DefaultCapacity = 16;
        private const double DefaultLoadFactor = 0.75;
        
        private int _hashSize;
        private Func<KeyType, int, int> _hashFunction;
        private LinkedList<KeyValuePair<KeyType, ValueType>>[] _hashMassive;
        private int _countInsert;

        public HashTable() {
            _hashSize = DefaultCapacity;
            _countInsert = 0;

            _hashMassive = new LinkedList<KeyValuePair<KeyType, ValueType>>[DefaultCapacity];

            for (int i = 0; i < DefaultCapacity; i++) {
                _hashMassive[i] = new LinkedList<KeyValuePair<KeyType, ValueType>>();
            }

            _hashFunction = (key, size) => HashFunctions.Fnv1aHash(key, size);
        }

        public HashTable(int hashSize, Func<KeyType, int, int> hashFunction) {
            if (hashSize <= 0) throw new ArgumentException("hashSize<=0");
            _hashSize = hashSize;
            _hashFunction = hashFunction;
            _countInsert = 0;

            _hashMassive = new LinkedList<KeyValuePair<KeyType, ValueType>>[hashSize];

            for (int i = 0; i < hashSize; i++) {
                _hashMassive[i] = new LinkedList<KeyValuePair<KeyType, ValueType>>();
            }
        }

        public bool TryUpdate(KeyType key, ValueType value) {
            int index = _hashFunction(key, _hashSize);
            var current = _hashMassive[index].First;

            while(current != null) {
                if(current.Value.Key.Equals(key)) {
                    current.Value = new KeyValuePair<KeyType, ValueType>(key, value);
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public void ReHash() {
            if(GetLoadFactor() >= 0.75) {
                var hashMassiveNew = new LinkedList<KeyValuePair<KeyType, ValueType>>[_hashSize * 2];

                for(int i=0;i<hashMassiveNew.Length;i++) {
                    hashMassiveNew[i] = new LinkedList<KeyValuePair<KeyType, ValueType>>();
                }

                for(int i=0;i<_hashSize;i++) {
                    var current = _hashMassive[i].First;
                    while(current!=null) {
                        int newIndex = _hashFunction(current.Value.Key, _hashSize * 2);
                        hashMassiveNew[newIndex].AddLast(current.Value);
                        current = current.Next;
                    }
                }
                _hashMassive = hashMassiveNew;
                _hashSize = _hashSize * 2;
            }
        }

        public void Add(KeyType key, ValueType value) {
            if (!TryUpdate(key, value)) {
                int index = _hashFunction(key, _hashSize);
                _countInsert++;
                _hashMassive[index].AddLast(new KeyValuePair<KeyType, ValueType>(key, value));

                ReHash();
            }
        }

        public double GetLoadFactor() {
            int countFilledPlace = 0;
            for(int i=0;i<_hashSize;i++) {
                countFilledPlace += _hashMassive[i].Count;
            }
            return (double)countFilledPlace / _hashSize;
        }
    }
}
