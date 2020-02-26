using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UnifiGrid : MonoBehaviour
{

    public NodeGrid one;
    public NodeGrid two;
    public GameObject tree;
    public List<Transform> caselle=new List<Transform>();


    public GameObject playerIa;
    public GameObject target;
    public UnifiASTAR aStar= new UnifiASTAR();
    public Dictionary<string, NodeGridStar> nodeSet = new Dictionary<string, NodeGridStar>();
    void Start()
    {


        // crea il dizionario
        foreach (Transform child in transform)
        {
            NodeGrid nodeGrid=child.GetComponent<NodeGrid>();
            nodeSet.Add(nodeGrid.Coordinates, nodeGrid.myStar);
        }
        spawnObstacles();
        randomPositionIa();
        setCaselle();// fa Astar
        StartCoroutine(pathTracerAnimation());// sposta 
        

    }
    

    public void setCaselle()
    {
        List<NodeGrid> path = aStar.FindPath2(this,one,two);
        foreach(NodeGrid node in path)
        {
            caselle.Add(node.transform);
        }
    }


    public IEnumerator pathTracerAnimation()
    {

        foreach (Transform casella in caselle)
        {
            StartCoroutine(SingleMovement(casella));
            yield return new WaitForSeconds(2.01f);
            StopCoroutine(SingleMovement(casella));


        }
        SceneManager.LoadScene(2);
    }
    public IEnumerator SingleMovement(Transform movePos)
    {

        while (playerIa.transform.position != movePos.position)
        {
            playerIa.transform.position = Vector3.MoveTowards(playerIa.transform.position, movePos.position, 0.1f);
            yield return null;
        }


    }




    public void spawnObstacles()
    {
        for(int i = 0; i < 30; i++)
        {
            int[] coordinates = randomCoordinates();
            string coordinatesKey = coordinates[0] + " " + coordinates[1];
            NodeGrid node=nodeSet[coordinatesKey].nodeGrid;
            bool t = coordinates[0] == 0 && coordinates[1] == 0;
            if (node.isWalkable&&!t)
            {
                node.isWalkable = false;
                node.myStar.isWalkable = false;
                GameObject pippo=Instantiate(tree);
                pippo.transform.position = new Vector3(coordinates[0], 0.25f, coordinates[1]);
                
                
            }
            else
            {
                i --;
            }
        }
    }



    public void randomPositionIa()
    {
        
        
        int[] coordinates = randomCoordinates();
       
        string coordinatesKey = coordinates[0] + " " + coordinates[1];

        one = nodeSet[coordinatesKey].nodeGrid;
        /*
        while (!one.isWalkable)
        {
            coordinates = randomCoordinates();

            coordinatesKey = coordinates[0] + " " + coordinates[1];

            one = nodeSet[coordinatesKey].nodeGrid;
        }
        */
        
        if (one.isWalkable)
        {
            playerIa.transform.position = new Vector3(coordinates[0], 0, coordinates[1]);
            return;
        }
        else
        {
            randomPositionIa();
        }
        
        playerIa.transform.position = new Vector3(coordinates[0], 0, coordinates[1]);
    }

    public int[] randomCoordinates()
    {
        int[] result = new int[2];
        int[] numbers = new int[20];
        int start = -50;
        int index = 0;
        while (start <= 45)
        {

            numbers[index] = start;
            start = start + 5;
            index++;

        }

        result[0] = numbers[Random.Range(0, numbers.Length)];
        result[1] = numbers[Random.Range(0, numbers.Length)];

        return result;
    }

}
