using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeStar : IHeapItem<NodeStar>
{
    
    //public enum ColorPawn { White, Red, Blue};
    public ColorPawn color;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public NodeStar parent;
    int heapIndex;

    public List<NodeStar> neighbours = new List<NodeStar>();
    public List<string> cooordinatesList = new List<string>();



    public void setNeighBours()
    {
        int[,] joystick = { { 1, 0 }, { 0, -1 }, { -1, 1 }, { -1, 0 }, { 0, 1 },{ 1, -1 } };
        //int[,] joystick = { { 1, 0 }, { 0, -1 }, { -1, 1 }, { -1, 0 }, { 0, 1 } };
        for (int i = 0; i < joystick.GetLength(0); i++)
        {
            int xNeighbour = gridX + joystick[i, 0];
            int yNeighbour = gridY + joystick[i, 1];
            if( xNeighbour < 0)  continue;
            if (xNeighbour > 10) continue;

            if (yNeighbour < 0) continue;
            if (yNeighbour > 10) continue;
            string coordinates = xNeighbour + " " +yNeighbour ;
            NodeStar node = new NodeStar(xNeighbour, yNeighbour);
            neighbours.Add(node);
            cooordinatesList.Add(coordinates);
            
        }




    }


    public void printNeighbours()
    {
        string neighbourstring = "";
        foreach(NodeStar node in neighbours)
        {
            neighbourstring = neighbourstring+node.gridX + " " + node.gridY+"-";
        }
        Debug.Log(neighbourstring);
    }

    public void changeColor(ColorPawn color)
    {
        this.color = color;
    }
    public NodeStar( int _gridX, int _gridY)
    {
        this.color = ColorPawn.White;
        
        gridX = _gridX;
        gridY = _gridY;
        //setNeighBours();
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(NodeStar nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }


    public bool Target(NodeStar nodeStar)
    {
        if (gridX == nodeStar.gridX && gridY == nodeStar.gridY)
        {
            return true;
        }
        return false;
    }
}
public enum ColorPawn { White, Red, Blue };