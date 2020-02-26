using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseLighting : MonoBehaviour
{
    public Renderer rend;
    private bool turn = false;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    // Toggle the Object's visibility each second.
    void Update()
    {
        // Find out whether current second is odd or even
        //bool oddeven = Mathf.FloorToInt(Time.time) % 1 == 0;

        // Enable renderer accordingly
        //rend.enabled = oddeven;
        StartCoroutine(Light());
        StopCoroutine(Light());
    }


    IEnumerator Light()
    {
        if (turn)
        {
            rend.enabled = true;
            yield return new WaitForSeconds(1f);
            turn = false;
        }
        else
        {
            rend.enabled = false;
            yield return new WaitForSeconds(1f);
            turn = true;
        }
    }

}
