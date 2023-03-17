using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform startPos;

    private void Awake()
    {
        startPos = GameObject.Find("Point").transform;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = startPos.transform.position;
        GunsInventory.instance.GetGun();
    }
}
