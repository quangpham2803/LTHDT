using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval;
    private float counter = 0;
    public int numSpawn;
    public List<GameObject> enemies = new List<GameObject>();

    // Update is called once per frame
    private void Start()
    {
        interval = 200;
    }
    void FixedUpdate()
    {
        counter += 1;

        if (counter >= interval)
        {
            if(enemies.Count < numSpawn)
            {
               
                GameObject Enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                enemies.Add(Enemy);
             
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.Remove(enemies[i]);
                    counter = 0;
                }
            }
        
        }
    }
}