using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator LoadingTime()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(0);
    }
}
