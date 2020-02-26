using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyRotation : MonoBehaviour
{


    public Transform playerOne;
    public Transform playerTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Swap()
    {
        Transform appo = playerOne;
        playerOne = playerTwo;
        playerTwo = appo;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerOne.position.x < playerTwo.position.x)
        {
            playerOne.Rotate(0, playerOne.rotation.y - 180, 0);
            Swap();


        }
        if (playerTwo.position.x > playerOne.position.x)
        {
            playerTwo.Rotate(0, playerTwo.rotation.y - 180, 0);
            Swap();
        }


        //if ((Mathf.Abs(playerOne.position.x) - Mathf.Abs(playerTwo.position.x) )<= 0.45f)
        //{
        //    playerOne.position = new Vector3(playerOne.position.x-0.2f,0,0);
        //    playerTwo.position = new Vector3(playerOne.position.x + 0.2f, 0, 0);
        //}



    }





}
