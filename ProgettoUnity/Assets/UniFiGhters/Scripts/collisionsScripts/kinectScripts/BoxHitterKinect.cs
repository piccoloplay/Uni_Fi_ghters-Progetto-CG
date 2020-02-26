using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHitterKinect : BoxHitter
{
    public bool kinect = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        BoxHitterKinect component = collision.gameObject.GetComponent<BoxHitterKinect>();

        if (component!=null)
        {
            collisionDetection.ActivateGuard();
        }
       
        
    }

    private void OnCollisionExit(Collision collision)
    {
        BoxHitterKinect component = collision.gameObject.GetComponent<BoxHitterKinect>();

        if (component != null)
        {
            collisionDetection.DeActivateGuard();
        }
    }
}
