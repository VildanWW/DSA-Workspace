using DataStructures_CSharp.HashTable;

namespace DataStructures.Tests;

[TestClass]
public class HashTableTests {
    private HashTable<string, int> _table;

    [TestInitialize]
    public void Setup() {
        _table = new HashTable<string, int>(16, HashFunctions.Fnv1aHash);
    }

    [TestMethod]
    public void Constructor_Default_ShouldSetCapacity16() {
        var t = new HashTable<string, int>();
        Assert.AreEqual(16, t.HashSize);
        Assert.AreEqual(0, t.Count);
    }

    [TestMethod]
    public void Constructor_WithCapacity_ShouldSetCapacity() {
        var t = new HashTable<string, int>(100, HashFunctions.Fnv1aHash);
        Assert.AreEqual(100, t.HashSize);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Constructor_InvalidCapacity_ShouldThrow() {
        var t = new HashTable<string, int>(-5, HashFunctions.Fnv1aHash);
    }

    [TestMethod]
    public void Add_NewKey_ShouldIncreaseCount() {
        _table.Add("apple", 5);
        Assert.AreEqual(1, _table.Count);
    }

    [TestMethod]
    public void Add_MultipleKeys_ShouldIncreaseCount() {
        _table.Add("apple", 5);
        _table.Add("banana", 3);
        _table.Add("cherry", 7);
        Assert.AreEqual(3, _table.Count);
    }

    [TestMethod]
    public void Add_DuplicateKey_ShouldUpdateValue() {
        _table.Add("apple", 5);
        _table.Add("apple", 10);
        Assert.AreEqual(1, _table.Count);
        Assert.IsTrue(_table.TryGetValue("apple", out var value));
        Assert.AreEqual(10, value);
    }

    [TestMethod]
    public void Add_AfterRemove_ShouldAddNewNode() {
        _table.Add("apple", 5);
        _table.Remove("apple");
        _table.Add("apple", 10);
        Assert.AreEqual(1, _table.Count);
        Assert.AreEqual(10, _table["apple"]);
    }

    [TestMethod]
    public void Find_ExistingKey_ShouldReturnTrue() {
        _table.Add("apple", 5);
        Assert.IsTrue(_table.Find("apple"));
    }

    [TestMethod]
    public void Find_NonExistingKey_ShouldReturnFalse() {
        Assert.IsFalse(_table.Find("apple"));
    }

    [TestMethod]
    public void Find_AfterRemove_ShouldReturnFalse() {
        _table.Add("apple", 5);
        _table.Remove("apple");
        Assert.IsFalse(_table.Find("apple"));
    }

    [TestMethod]
    public void Remove_ExistingKey_ShouldReturnTrueAndDecreaseCount() {
        _table.Add("apple", 5);
        var result = _table.Remove("apple");
        Assert.IsTrue(result);
        Assert.AreEqual(0, _table.Count);
    }

    [TestMethod]
    public void Remove_NonExistingKey_ShouldReturnFalse() {
        var result = _table.Remove("apple");
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Remove_FirstInBucket_ShouldWork() {
        _table.Add("a", 1);
        _table.Add("b", 2);
        _table.Add("c", 3);
        _table.Remove("a");
        Assert.IsFalse(_table.Find("a"));
        Assert.IsTrue(_table.Find("b"));
        Assert.IsTrue(_table.Find("c"));
        Assert.AreEqual(2, _table.Count);
    }

    [TestMethod]
    public void Remove_MiddleInBucket_ShouldWork() {
        _table.Add("a", 1);
        _table.Add("b", 2);
        _table.Add("c", 3);
        _table.Remove("b");
        Assert.IsTrue(_table.Find("a"));
        Assert.IsFalse(_table.Find("b"));
        Assert.IsTrue(_table.Find("c"));
        Assert.AreEqual(2, _table.Count);
    }

    [TestMethod]
    public void Remove_LastInBucket_ShouldWork() {
        _table.Add("a", 1);
        _table.Add("b", 2);
        _table.Add("c", 3);
        _table.Remove("c");
        Assert.IsTrue(_table.Find("a"));
        Assert.IsTrue(_table.Find("b"));
        Assert.IsFalse(_table.Find("c"));
        Assert.AreEqual(2, _table.Count);
    }

    [TestMethod]
    public void Indexer_Get_ShouldReturnValue() {
        _table.Add("apple", 5);
        var value = _table["apple"];
        Assert.AreEqual(5, value);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void Indexer_Get_NotFound_ShouldThrow() {
        var value = _table["apple"];
    }

    [TestMethod]
    public void Indexer_Set_ShouldAdd() {
        _table["apple"] = 5;
        Assert.AreEqual(1, _table.Count);
        Assert.AreEqual(5, _table["apple"]);
    }

    [TestMethod]
    public void Indexer_Set_ShouldUpdate() {
        _table["apple"] = 5;
        _table["apple"] = 10;
        Assert.AreEqual(1, _table.Count);
        Assert.AreEqual(10, _table["apple"]);
    }

    [TestMethod]
    public void TryGetValue_ExistingKey_ShouldReturnTrue() {
        _table.Add("apple", 5);
        var result = _table.TryGetValue("apple", out var value);
        Assert.IsTrue(result);
        Assert.AreEqual(5, value);
    }

    [TestMethod]
    public void TryGetValue_NonExistingKey_ShouldReturnFalse() {
        var result = _table.TryGetValue("apple", out var value);
        Assert.IsFalse(result);
        Assert.AreEqual(0, value);
    }

    [TestMethod]
    public void Contains_ExistingPair_ShouldReturnTrue() {
        _table.Add("apple", 5);
        var pair = new KeyValuePair<string, int>("apple", 5);
        Assert.IsTrue(_table.Contains(pair));
    }

    [TestMethod]
    public void Contains_ExistingKeyWrongValue_ShouldReturnFalse() {
        _table.Add("apple", 5);
        var pair = new KeyValuePair<string, int>("apple", 10);
        Assert.IsFalse(_table.Contains(pair));
    }

    [TestMethod]
    public void Contains_NonExistingKey_ShouldReturnFalse() {
        var pair = new KeyValuePair<string, int>("apple", 5);
        Assert.IsFalse(_table.Contains(pair));
    }

    [TestMethod]
    public void RemovePair_ExistingPair_ShouldReturnTrue() {
        _table.Add("apple", 5);
        var pair = new KeyValuePair<string, int>("apple", 5);
        var result = _table.Remove(pair);
        Assert.IsTrue(result);
        Assert.AreEqual(0, _table.Count);
    }

    [TestMethod]
    public void RemovePair_WrongValue_ShouldReturnFalse() {
        _table.Add("apple", 5);
        var pair = new KeyValuePair<string, int>("apple", 10);
        var result = _table.Remove(pair);
        Assert.IsFalse(result);
        Assert.AreEqual(1, _table.Count);
    }

    [TestMethod]
    public void Clear_ShouldRemoveAllElements() {
        _table.Add("apple", 5);
        _table.Add("banana", 3);
        _table.Clear();
        Assert.AreEqual(0, _table.Count);
        Assert.IsFalse(_table.Find("apple"));
        Assert.IsFalse(_table.Find("banana"));
    }

    [TestMethod]
    public void Clear_EmptyTable_ShouldWork() {
        _table.Clear();
        Assert.AreEqual(0, _table.Count);
    }

    [TestMethod]
    public void GetLoadFactor_EmptyTable_ShouldReturnZero() {
        var loadFactor = _table.GetLoadFactor();
        Assert.AreEqual(0.0, loadFactor);
    }

    [TestMethod]
    public void GetLoadFactor_WithElements_ShouldReturnCorrectValue() {
        for (int i = 0; i < 8; i++) {
            _table.Add($"key{i}", i);
        }
        var loadFactor = _table.GetLoadFactor();
        Assert.AreEqual(8.0 / 16.0, loadFactor, 0.001);
    }

    [TestMethod]
    public void GetEmptyBuckets_ShouldReturnCorrectCount() {
        _table.Add("a", 1);
        _table.Add("b", 2);
        var empty = _table.GetEmptyBuckets();
        Assert.AreEqual(14, empty);
    }

    [TestMethod]
    public void ReHash_ShouldDoubleCapacity() {
        for (int i = 0; i < 13; i++) {
            _table.Add($"key{i}", i);
        }
       
        Assert.AreEqual(32, _table.HashSize);
    }

    [TestMethod]
    public void ReHash_ShouldPreserveAllElements() {
        for (int i = 0; i < 20; i++) {
            _table.Add($"key{i}", i);
        }
        for (int i = 0; i < 20; i++) {
            Assert.IsTrue(_table.Find($"key{i}"));
            Assert.AreEqual(i, _table[$"key{i}"]);
        }
        Assert.AreEqual(20, _table.Count);
    }

    [TestMethod]
    public void GetEnumerator_EmptyTable_ShouldReturnEmpty() {
        var count = 0;
        foreach (var pair in _table) {
            count++;
        }
        Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void GetEnumerator_ShouldIterateAllElements() {
        var expected = new Dictionary<string, int> {
            {"a", 1}, {"b", 2}, {"c", 3}
        };
        foreach (var pair in expected) {
            _table.Add(pair.Key, pair.Value);
        }
        var actual = new Dictionary<string, int>();
        foreach (var pair in _table) {
            actual[pair.Key] = pair.Value;
        }
        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void CopyTo_ShouldCopyAllElements() {
        _table.Add("a", 1);
        _table.Add("b", 2);
        var array = new KeyValuePair<string, int>[2];
        _table.CopyTo(array, 0);
        Assert.AreEqual(2, array.Length);
        Assert.IsTrue(array.Contains(new KeyValuePair<string, int>("a", 1)));
        Assert.IsTrue(array.Contains(new KeyValuePair<string, int>("b", 2)));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CopyTo_NullArray_ShouldThrow() {
        _table.CopyTo(null, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void CopyTo_NegativeIndex_ShouldThrow() {
        var array = new KeyValuePair<string, int>[1];
        _table.CopyTo(array, -1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CopyTo_InsufficientSpace_ShouldThrow() {
        _table.Add("a", 1);
        _table.Add("b", 2);
        var array = new KeyValuePair<string, int>[1];
        _table.CopyTo(array, 0);
    }

    [TestMethod]
    public void IsReadOnly_ShouldReturnFalse() {
        Assert.IsFalse(_table.IsReadOnly);
    }

    [TestMethod]
    public void HashSize_ShouldReturnCurrentCapacity() {
        Assert.AreEqual(16, _table.HashSize);
        for (int i = 0; i < 20; i++) {
            _table.Add($"key{i}", i);
        }
        Assert.AreEqual(32, _table.HashSize);
    }

    [TestMethod]
    public void Add_WithCollision_ShouldWork() {
        var table = new HashTable<int, string>(2, (key, size) => key % size);
        table.Add(0, "zero");
        table.Add(2, "two");
        table.Add(4, "four");
        Assert.AreEqual(3, table.Count);
        Assert.AreEqual("zero", table[0]);
        Assert.AreEqual("two", table[2]);
        Assert.AreEqual("four", table[4]);
    }

    [TestMethod]
    public void Remove_WithCollision_ShouldPreserveOthers() {
        var table = new HashTable<int, string>(2, (key, size) => key % size);
        table.Add(0, "zero");
        table.Add(2, "two");
        table.Add(4, "four");
        table.Remove(2);
        Assert.AreEqual(2, table.Count);
        Assert.IsTrue(table.Find(0));
        Assert.IsFalse(table.Find(2));
        Assert.IsTrue(table.Find(4));
    }
}