using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool easyMode;
    public bool hardMode;
    public static GameManager instance;
    public GameObject[] enemys;
    void Start()
    {    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        hardMode = false; ;
    }

    // Update is called once per frame
    private void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        if (hardMode)
        {
            Debug.Log("HARD MODE");
       
        }
        else
        {
            Debug.Log("EASY MODE");
          
        }
      
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
       SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Update()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
