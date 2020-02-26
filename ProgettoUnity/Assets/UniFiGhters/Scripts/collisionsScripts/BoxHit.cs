using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHit : MonoBehaviour
{
    public float damage;
    public CollisionDetection collisionDetector;
    
    void Start()
    {
        
    }

    IEnumerator Trump()
    {
        yield return new WaitForSeconds(1);
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (!collisionDetector.DefenseOn)
        {
            if (collision.gameObject.tag == "Fist" || collision.gameObject.tag == "Kick")
            {

                if (collision.gameObject.GetComponent<BoxHitter>().collisionDetection.playerOne != collisionDetector.playerOne)
                //if (!(collision.gameObject.GetComponent<BoxHitter>().collisionDetection.playerOne && collisionDetector.playerOne))
                {
                    collisionDetector.UpdateHealthBar(damage);
                }
            }
        }
        StartCoroutine(Trump());
        StopCoroutine(Trump());

    }

   


    


}
