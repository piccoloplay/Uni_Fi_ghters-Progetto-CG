using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{

    public GameObject one;
    public GameObject two;
    public bool kinect=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (one.gameObject.GetComponent<CollisionDetection>().currentHp <= 0)
        {
            StartCoroutine(destroyOpponent(two, one));
            StopCoroutine(destroyOpponent(two, one));
            
        }

        if (two.gameObject.GetComponent<CollisionDetection>().currentHp <= 0)
        {
            if (kinect)
            {
                Destroy(two);
                one.GetComponent<Animator>().Play("win");
            }
            else
            {
                StartCoroutine(destroyOpponent(one,two));
                StopCoroutine(destroyOpponent(one,two));
            }
            
        }
    }
    // primo parametro esulta secondo paramentro viene distrutto
    IEnumerator destroyOpponent( GameObject two, GameObject one)
    {
       
        if (two.GetComponent<Animator>() != null) two.GetComponent<Animator>().Play("win");
        
        yield return new WaitForSeconds(1.5f);
        Destroy(one);
        ShutDownMusic();
        SceneManager.LoadSceneAsync(6);
    }




    public void ShutDownMusic()
    {
        GameObject ob = GameObject.Find("Star Fox (SNES) Corneria Music");
        if (ob != null)
        {
            Destroy(ob);
        }
    }


}
