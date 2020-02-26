using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MasterScene : MonoBehaviour
{

    public static int difficulty=0;
    public static int scene = 0;



    
    

    public void LoadIavsIa()
    {
        scene = 3;
        SceneManager.LoadScene(5);
    }
    public void LoadIavsKinect()
    {
        scene = 1;
        SceneManager.LoadScene(5);
    }


    public void AbbandonareLanave()
    {
        StartCoroutine(closeApplication());
    }


    public IEnumerator closeApplication()
    {
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
       
    }


    public void SetDifficultyEasy(bool diff)
    {
        difficulty = 0;
        Debug.Log(difficulty);
    }

    public void SetDifficultyMedium(bool diff)
    {
        difficulty = 1;
        Debug.Log(difficulty);
    }

}
