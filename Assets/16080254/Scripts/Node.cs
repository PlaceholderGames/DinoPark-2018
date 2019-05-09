using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int slowDown;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node(bool Wlkble, Vector3 wPos, int gX, int gY, int SD)
    {
        walkable = Wlkble;
        worldPosition = wPos;
        gridX = gX;
        gridY = gY;
        slowDown = SD;
    }

    public int fCost //get the fcost by adding the gcost and hcost togtether
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        { return heapIndex; }

        set
        { heapIndex = value; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) //if two fCosts are equal then do tiebreaker
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
