using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T> {

    T[] items;
    int currentItemCount;
	
	public Heap(int maxHeapSize)
    {items = new T[maxHeapSize];}

    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item; //put new item to end of array
        SortUp(item);
        currentItemCount++; //Icrement current item count by 1;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item) //If item piroirty needs to be changed (e.g. node in openset to be updated with lower fcost because newer path has been found with it)
    { SortUp(item); }

    public int Count //get the amount of items in heap
    {
        get
        { return currentItemCount; }
    }

    public bool Contains(T item) // Check if heap contains specfic item
    {
        return Equals(items[item.HeapIndex], item); //Compare if item in array with same index as item passed in is equal to actual item passed in
    }

    void SortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.HeapIndex * 2 + 1;
            int childIndexRight = item.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;

                //check if item also has another child on the right
                if (childIndexRight < currentItemCount)
                {
                    //check which child has the higher piority
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                //SwapIndex is now equal to child with higher piority, check if parent has lower fCost that child with highest fCost
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else //If parent is higher than both children then leave it in the same posistion as it was in array as it's in it's correct posistion
                {
                    return;
                }
            }
            else //if parent has no children, it is in correct posistion so leave alone
                return;
        }
    }

    void SortUp(T item) //
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0) // If higher piority than parent item, return 1, if same piroirty, return 0, if lower, return -1
                Swap(item, parentItem); //Swap parent item posistion in array with child item posistion
            else
                break;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}
