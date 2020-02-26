using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    public Rigidbody rBody;
    public Animator animator;
    private float inputH;
    private float inputV;
    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();
        rBody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            //animator.Play("Capoeira",-1,0f);
        }
        if (Input.GetMouseButton(0))
        {
            //animator.Play("DAMAGED00",-1,0f);
            animator.Play("Punching", -1, 0f);
        }

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        animator.SetFloat("inputH", inputH);
        animator.SetFloat("inputV", inputV);

        ///// movimento
        float moveX = inputH * 50 * Time.deltaTime;
        float moveZ = inputV * 50 * Time.deltaTime;
        rBody.velocity = new Vector3(moveX, 0f, moveZ);
    }
}
