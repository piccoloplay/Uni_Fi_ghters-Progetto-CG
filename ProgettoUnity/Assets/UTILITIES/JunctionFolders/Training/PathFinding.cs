using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    
    public bool FindPath2(GraphStar graph,NodeStar startNode, NodeStar targetNode)
    {
        
        if (startNode.color != targetNode.color) return false;

        Heap<NodeStar> openSet = new Heap<NodeStar>(121);
        HashSet<NodeStar> closedSet = new HashSet<NodeStar>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            NodeStar currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);
            
            
            if (currentNode.Target(targetNode))
            {
                RetracePath(startNode, targetNode);
                return true;
            }

            foreach (string keysCoordinates in currentNode.cooordinatesList)
            {
                NodeStar neighbour = graph.nodeSet[keysCoordinates];
                if (neighbour.color != currentNode.color || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;
                    
                    
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else
                    {
                        //openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        
        return false;
    }

    int GetDistance(NodeStar nodeA, NodeStar nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    void RetracePath(NodeStar startNode, NodeStar endNode)
    {
        
        List<NodeStar> path = new List<NodeStar>();
        NodeStar currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Add(startNode);
        path.Reverse();

        //printPath(path);

    }
    
    public void printPath(List<NodeStar> nodes)
    {
        string path = "";
        foreach(NodeStar node in nodes)
        {
            path=path+node.gridX+" "+node.gridY+"-";
        }
        Debug.Log(path);
    }


}
