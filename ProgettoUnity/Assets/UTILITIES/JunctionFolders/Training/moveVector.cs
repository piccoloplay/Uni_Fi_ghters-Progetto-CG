using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveVector : MonoBehaviour
{
   
    
    public List<Transform> caselle;
   
    void Start()
    {
        StartCoroutine(pathTracerAnimation());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        

        
    }

    
    public IEnumerator pathTracerAnimation()
    {

        foreach(Transform casella in caselle)
        {
            StartCoroutine(SingleMovement(casella));
            yield return new WaitForSeconds(2.01f);
            StopCoroutine(SingleMovement(casella));
           
            
        }

    }
    public IEnumerator SingleMovement(Transform movePos)
    {

        while (transform.position != movePos.position) { 
            transform.position = Vector3.MoveTowards(transform.position, movePos.position, 0.1f);
            yield return null;
             }
        

    }
   

   

}
