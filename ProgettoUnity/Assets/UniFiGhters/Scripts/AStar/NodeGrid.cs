using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public NodeGridStar myStar;
    public bool isWalkable=true;
    public List<string> neighbours;

    private string coordinates;

    public string Coordinates { get => coordinates; set => coordinates = value; }

    public void setNeighbours()
    {
        int[,] joystick = {{1,0 },{-1,0},{0,1},{0,-1},{1,1},{-1,-1},{1,-1},{-1,1}};
        for(int i=0;i< joystick.GetLength(0); i++)
        {
            int xNeighbour = (int)transform.position.x + 5*joystick[i, 0];
            int yNeighbour = (int)transform.position.z + 5*joystick[i, 1];
            if (xNeighbour < -50) continue;
            if (xNeighbour > 45) continue;

            if (yNeighbour < -50) continue;
            if (yNeighbour > 45) continue;
            
            string coordinates = xNeighbour + " " + yNeighbour;
          
            neighbours.Add(coordinates);
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        neighbours = new List<string>();
        myStar = new NodeGridStar((int)transform.position.x, (int)transform.position.z,neighbours,isWalkable,this);
        Coordinates = transform.position.x + " " + transform.position.z;
        setNeighbours();
    }

    
}
public class NodeGridStar : IHeapItem<NodeGridStar>
{
    public NodeGrid nodeGrid;
    public bool isWalkable;
    public List<string> neighbours;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public NodeGridStar parent;
    int heapIndex;



    public NodeGridStar(int _gridX, int _gridY, List<string>neighbours, bool isWalkable, NodeGrid nodeGrid)
    {
        this.nodeGrid = nodeGrid;
        this.isWalkable = isWalkable;
        this.neighbours = neighbours;
        gridX = _gridX;
        gridY = _gridY;
        
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

    public int CompareTo(NodeGridStar nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }


    public bool Target(NodeGridStar nodeStar)
    {
        if (gridX == nodeStar.gridX && gridY == nodeStar.gridY)
        {
            return true;
        }
        return false;
    }
}
