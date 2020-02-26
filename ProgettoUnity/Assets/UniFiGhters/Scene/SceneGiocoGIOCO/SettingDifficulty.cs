using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDifficulty : MonoBehaviour
{

    public GameObject easy;
    public GameObject medium;

    // Start is called before the first frame update
    void Start()
    {

        SetUp();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetUp()
    {
        if (MasterScene.difficulty == 0)
        {
            Destroy(medium);
        }
        else
        {
            Destroy(easy);
        }

    }
}
