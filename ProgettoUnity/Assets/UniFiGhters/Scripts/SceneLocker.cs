using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLocker : MonoBehaviour
{
    public GameObject kineckt;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        kineckt.transform.position = new Vector3(kineckt.transform.position.x, 0, 0);
    }
}
