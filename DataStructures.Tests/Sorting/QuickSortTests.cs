using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures_CSharp.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures.Tests.Sorting;

[TestClass]
public class QuickSortTests {

    [TestMethod]
    public void Sort_IntArray_Ascending_SortedCorrectly() {
        int[] arr = { 5, 2, 8, 1, 9, 3 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        int[] expected = { 1, 2, 3, 5, 8, 9 };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_IntArray_Descending_SortedCorrectly() {
        int[] arr = { 5, 2, 8, 1, 9, 3 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) > 0);
        int[] expected = { 9, 8, 5, 3, 2, 1 };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_EmptyArray_DoesNothing() {
        int[] arr = new int[0];
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        Assert.AreEqual(0, arr.Length);
    }

    [TestMethod]
    public void Sort_SingleElementArray_DoesNothing() {
        int[] arr = { 42 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        Assert.AreEqual(42, arr[0]);
    }

    [TestMethod]
    public void Sort_AlreadySortedArray_StaysSorted() {
        int[] arr = { 1, 2, 3, 4, 5 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        int[] expected = { 1, 2, 3, 4, 5 };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_ReverseSortedArray_SortedCorrectly() {
        int[] arr = { 9, 7, 5, 3, 1 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        int[] expected = { 1, 3, 5, 7, 9 };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_ArrayWithDuplicates_SortedCorrectly() {
        int[] arr = { 5, 2, 5, 1, 2, 3 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        int[] expected = { 1, 2, 2, 3, 5, 5 };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_StringArray_Ascending_SortedCorrectly() {
        string[] arr = { "banana", "apple", "cherry", "date" };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        string[] expected = { "apple", "banana", "cherry", "date" };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_DoubleArray_Ascending_SortedCorrectly() {
        double[] arr = { 3.14, 1.5, 2.7, 0.5 };
        QuickSort.Sort(arr, (a, b) => a.CompareTo(b) < 0);
        double[] expected = { 0.5, 1.5, 2.7, 3.14 };
        CollectionAssert.AreEqual(expected, arr);
    }

    [TestMethod]
    public void Sort_ListInt_Ascending_SortedCorrectly() {
        List<int> list = new List<int> { 5, 2, 8, 1, 9, 3 };
        QuickSort.Sort(list, (a, b) => a.CompareTo(b) < 0);
        int[] expected = { 1, 2, 3, 5, 8, 9 };
        CollectionAssert.AreEqual(expected, list);
    }
}