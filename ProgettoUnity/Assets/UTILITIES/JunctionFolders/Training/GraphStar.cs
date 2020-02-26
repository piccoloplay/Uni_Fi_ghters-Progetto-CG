using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphStar 
{
    private int problenDimension=11;
    public Dictionary<string, NodeStar> nodeSet = new Dictionary<string, NodeStar>();
    public List<NodeStar> nodeList = new List<NodeStar>();
    public GraphStar()
    {
        SetNodeSet();
    }
    
    public void SetNodeSet()
    {

        for(int i = 0; i < problenDimension; i++)
        {
            for(int j = 0; j < problenDimension; j++)
            {
                string coordinates = i + " " + j;
                NodeStar node = new NodeStar(i,j);
                node.setNeighBours();
                nodeList.Add(node);
                nodeSet.Add(coordinates, node);
            }
        }


    }

    

    
}
