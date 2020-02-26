using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSP : MonoBehaviour
{
    public Transform enemy;
    public Transform Board;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemy.position = new Vector3(enemy.position.x, this.transform.position.y, this.transform.position.z);
        Board.position=new Vector3(Board.position.x, this.transform.position.y, this.transform.position.z);
        //transform.position = new Vector3(transform.position.x, 0, 0);
    }
}
