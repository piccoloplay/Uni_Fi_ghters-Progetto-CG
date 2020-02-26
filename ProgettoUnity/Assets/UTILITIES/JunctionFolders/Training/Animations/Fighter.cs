using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 movement;
    public float verticalVelocity;


    private void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -0.01f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = 10;
            }
        }
        else
        {
            verticalVelocity -= 14*Time.deltaTime;
        }
        movement = Vector3.zero;
        movement.x = Input.GetAxis("Horizontal")*-5;
        movement.y = verticalVelocity;
        controller.Move(movement*Time.deltaTime);
    }
}
