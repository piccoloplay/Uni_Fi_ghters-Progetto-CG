using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public  AudioSource music;
    public TextMeshProUGUI loadingText;

    public void Awake()
    {
        DontDestroyOnLoad(music);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(LoadingTime());
        
        
    }

   

    public IEnumerator LoadingTime()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(MasterScene.scene+MasterScene.difficulty);
    }
   
}
