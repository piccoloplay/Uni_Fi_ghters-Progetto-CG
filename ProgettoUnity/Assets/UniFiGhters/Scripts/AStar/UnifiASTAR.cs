using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiASTAR 
{
    public List<NodeGrid> FindPath2(UnifiGrid graph, NodeGrid inputStart, NodeGrid inputTarget)
    {
        NodeGridStar startNode = inputStart.myStar;
        NodeGridStar targetNode = inputTarget.myStar;


        Heap<NodeGridStar> openSet = new Heap<NodeGridStar>(400);
        HashSet<NodeGridStar> closedSet = new HashSet<NodeGridStar>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            NodeGridStar currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);


            //if (currentNode.Target(targetNode))
            //{
               // RetracePath(startNode, targetNode);
              //  return null;
            //}

            foreach (string keysCoordinates in currentNode.neighbours)
            {
                NodeGridStar neighbour = graph.nodeSet[keysCoordinates];
                if (!(neighbour.isWalkable) || closedSet.Contains(neighbour))
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

        return RetracePath(startNode,targetNode);
    }

    int GetDistance(NodeGridStar nodeA, NodeGridStar nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    List<NodeGrid> RetracePath(NodeGridStar startNode, NodeGridStar endNode)
    {

        List<NodeGrid> path = new List<NodeGrid>();
        NodeGridStar currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.nodeGrid);
            currentNode = currentNode.parent;
        }
        path.Add(startNode.nodeGrid);
        path.Reverse();
        return path;
        //printPath(path);

    }

}
