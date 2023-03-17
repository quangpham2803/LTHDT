using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    
    }


    // Update is called once per frame
    private void OnLevelWasLoaded(int level)
    {
        //GameObject[] players = GameObject.FindGameObjectsWithTag("---------------- Player (1)");
        //if (players.Length > 0)
        //{
        //    Destroy(players[1]);
        //}
        
    }
 
}
